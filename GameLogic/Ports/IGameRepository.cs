using CategoriesGameServer.Logics;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public interface IGameRepository
    {
        ValueTask CreateGameAsync(string gameCode, GameState state,
            CancellationToken cancellationToken = default);
        ValueTask UpdateGameAsync(string gameCode, GameState state, 
            CancellationToken cancellationToken = default);
        ValueTask AddPlayerToGameAsync(string gameCode, GamePlayer player, 
            CancellationToken cancellationToken = default);
    }
}