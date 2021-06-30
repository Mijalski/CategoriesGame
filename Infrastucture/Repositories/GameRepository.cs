using CategoriesGameServer.Logics;
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
    }
}
