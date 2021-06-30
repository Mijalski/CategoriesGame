using CategoriesGameContracts.Contracts;
using CategoriesGameServer.Actions;
using CategoriesGameServer.Logics;
using GameLogic.Logics;
using Infrastucture.Generators;
using Infrastucture.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CategoriesGameServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICodeGenerator _codeGenerator;

        public GameHub(IGameRepository gameRepository, 
            ICodeGenerator codeGenerator)
        {
            _gameRepository = gameRepository ?? throw new System.ArgumentNullException(nameof(gameRepository));
            _codeGenerator = codeGenerator ?? throw new System.ArgumentNullException(nameof(codeGenerator));
        }

        public async Task StartGameAsync(StartGameAction action, CancellationToken cancellationToken = default)
        {
            var code = _codeGenerator.GetCode(8);
            var settings = new GameSettings(action.Settings.RoundCount, action.Settings.Categories, 
                        action.Settings.MaxRoundTime, action.Settings.TimeToVote, action.Settings.RoundTimeAfterFirstAnswer);

            await _gameRepository.CreateGameAsync(code, new GameState
            {
                Settings = settings,
                Code = code,
                Players = new List<GamePlayer>() { new GamePlayer(action.UserName) } 
            }, cancellationToken);

            await Groups.AddToGroupAsync(Context.ConnectionId, code, cancellationToken);
            await Clients.Group(code).SendAsync("GameStarted", code);
        }

        public async Task JoinGameAsync(JoinGameAction action, CancellationToken cancellationToken = default)
        {
            await _gameRepository.AddPlayerToGameAsync(action.Code, new GamePlayer(action.UserName), cancellationToken);
            var state = await _gameRepository.GetGameStateAsync(action.Code, cancellationToken);

            await Groups.AddToGroupAsync(Context.ConnectionId, action.Code, cancellationToken);
            await Clients.Group(action.Code).SendAsync("PlayerJoined", action.UserName, cancellationToken);
            await Clients.Caller.SendAsync("GetSettings", 
                new SettingsDto(state.Settings.RoundCount, state.Settings.Categories, state.Settings.MaxRoundTime,
                                state.Settings.TimeToVote, state.Settings.RoundTimeAfterFirstAnswer),
                cancellationToken);
        }

        public async Task StartRoundAsync(StartRoundAction action, CancellationToken cancellationToken = default)
        {
            var round = new GameRound("A");
            await _gameRepository.AddRoundToGameAsync(action.Code, round, cancellationToken);

            await Clients.Group(action.Code).SendAsync("RoundStarted", round.Letter, cancellationToken);
        }

        public async Task SubmitAnswersAsync(SubmitAnswersAction action, CancellationToken cancellationToken = default)
        {
            var answers = action.Answers.Select(x => new RoundAnswer(x.Category, x.Answer, action.PlayerUserName));
            await _gameRepository.AddAnswersRangeToRoundAsync(action.Code, answers, cancellationToken);

            await Clients.Group(action.Code).SendAsync("AnswersSubmitted", action.PlayerUserName, cancellationToken);

            var state = await _gameRepository.GetGameStateAsync(action.Code, cancellationToken);
            var finishedRound = state.Rounds.SingleOrDefault(_ => _.IsFinishedAnswering && !_.IsFinishedVoting);
            if (finishedRound is not null)
            {
                await Clients.Group(action.Code).SendAsync("RoundFinished",
                    finishedRound.Answers.Select(x => new PlayerAnswerDto(x.Category, x.Answer, x.PlayerUserName)), 
                    cancellationToken);
            }
        }

        public async Task VoteForAnswersAsync(VoteForAnswersAction action, CancellationToken cancellationToken = default)
        {
            var votes = action.Votes.Select(x => new AnswerVote(
                x.Category, x.IsPositive, x.AnsweringPlayerUserName, action.PlayerUserName));
            await _gameRepository.AddVotesToAnswersAsync(action.Code, votes, cancellationToken);

            await Clients.Group(action.Code).SendAsync("VotesSubmitted", action.PlayerUserName, cancellationToken);
        }
    }
}
