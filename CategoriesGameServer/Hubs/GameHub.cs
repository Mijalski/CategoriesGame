using CategoriesGameServer.Logics;
using GameLogic.Dtos;
using Infrastucture.Generators;
using Infrastucture.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
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

            await Groups.AddToGroupAsync(Context.ConnectionId, action.Code, cancellationToken);
            await Clients.Group(action.Code).SendAsync("PlayerJoined", action.UserName, cancellationToken);
        }
    }
}
