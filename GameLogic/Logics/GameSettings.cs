using System;
using System.Collections.Generic;

namespace CategoriesGameServer.Logics
{
    public class GameSettings
    {

        public GameSettings(int roundCount, IEnumerable<string> categories, 
            int maxRoundTime, int timeToVote, int? roundTimeAfterFirstAnswer)
        {
            RoundCount = roundCount;
            Categories = categories;
            MaxRoundTime = maxRoundTime;
            TimeToVote = timeToVote;
            RoundTimeAfterFirstAnswer = roundTimeAfterFirstAnswer;
        }
        public int RoundCount { get; }

        public int MaxRoundTime { get; }
        public int TimeToVote { get; } 
        public int? RoundTimeAfterFirstAnswer { get; }
        public IEnumerable<string> Categories { get; }
    }
}
