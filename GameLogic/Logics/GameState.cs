using System;
using System.Collections.Generic;

namespace CategoriesGameServer.Logics
{
    public class GameState
    {
        public string Code { get; set; }
        public GameSettings Settings { get; set; }
        public IEnumerable<GameRound> Rounds { get; set; }
        public IEnumerable<GamePlayer> Players { get; set; }
    }
}
