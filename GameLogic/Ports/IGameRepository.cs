using CategoriesGameServer.Logics;
using GameLogic.Logics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public interface IGameRepository
    {
        ValueTask<GameState> GetGameStateAsync(string gameCode, 
            CancellationToken cancellationToken = default);
        ValueTask CreateGameAsync(string gameCode, GameState state,
            CancellationToken cancellationToken = default);
        ValueTask UpdateGameAsync(string gameCode, GameState state, 
            CancellationToken cancellationToken = default);
        ValueTask AddPlayerToGameAsync(string gameCode, GamePlayer player, 
            CancellationToken cancellationToken = default);
        ValueTask AddRoundToGameAsync(string gameCode, GameRound round,
            CancellationToken cancellationToken = default);
        ValueTask AddAnswersRangeToRoundAsync(string gameCode, IEnumerable<RoundAnswer> answers,
            CancellationToken cancellationToken = default);
        ValueTask AddVotesToAnswersAsync(string gameCode, IEnumerable<AnswerVote> votes,
            CancellationToken cancellationToken = default);
    }
}