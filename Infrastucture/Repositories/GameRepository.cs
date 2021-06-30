using CategoriesGameServer.Logics;
using GameLogic.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly Dictionary<string, GameState> _games;

        public GameRepository()
        {
            _games = new();
        }

        public ValueTask<GameState> GetGameStateAsync(string gameCode, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult(_games[gameCode]);
        }


        public ValueTask CreateGameAsync(string gameCode, GameState state,
            CancellationToken cancellationToken = default)
        {
            _games.Add(gameCode, state);

            return ValueTask.CompletedTask;
        }

        public ValueTask UpdateGameAsync(string gameCode, GameState state, CancellationToken cancellationToken = default)
        {
            _games[gameCode] = state;

            return ValueTask.CompletedTask;
        }

        public ValueTask AddPlayerToGameAsync(string gameCode, GamePlayer player, CancellationToken cancellationToken = default)
        {
            _games[gameCode].Players = _games[gameCode].Players.Append(player);

            return ValueTask.CompletedTask;
        }

        public ValueTask AddRoundToGameAsync(string gameCode, GameRound round, CancellationToken cancellationToken = default)
        {
            var state = _games[gameCode];
            round.SetPlayerCount(state.Players.Count());
            state.Rounds = state.Rounds.Append(round);

            return ValueTask.CompletedTask;
        }

        public ValueTask AddAnswersRangeToRoundAsync(string gameCode, IEnumerable<RoundAnswer> answers, 
            CancellationToken cancellationToken = default)
        {
            var round = _games[gameCode].Rounds.Single(_ => _.IsFinishedAnswering);
            round.AddAnswersRange(answers);

            return ValueTask.CompletedTask;
        }

        public ValueTask AddVotesToAnswersAsync(string gameCode, IEnumerable<AnswerVote> votes,
            CancellationToken cancellationToken = default)
        {
            var round = _games[gameCode].Rounds.Single(_ => _.IsFinishedAnswering);
            round.AddVotesRange(votes);

            return ValueTask.CompletedTask;
        }
    }
}
