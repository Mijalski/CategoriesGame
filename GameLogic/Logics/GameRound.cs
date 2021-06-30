using GameLogic.Logics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoriesGameServer.Logics
{
    public class GameRound
    {
        public GameRound(string letter)
        {
            Letter = letter;
            IsFinishedAnswering = false;
            IsFinishedVoting = false;
            PlayerAnswerCount = 0;
            PlayerVoteCount = 0;
        }

        public string Letter { get; }
        public bool IsFinishedAnswering { get; private set; }
        public bool IsFinishedVoting { get; private set; }
        public IEnumerable<RoundAnswer> Answers { get; private set; }
        public int PlayerAnswerCount { get; private set; }
        public int PlayerVoteCount { get; private set; }

        public void AddAnswersRange(IEnumerable<RoundAnswer> answers)
        {
            Answers = Answers.Concat(answers);
            PlayerAnswerCount++;
            if(PlayerAnswerCount == _playerCount)
            {
                IsFinishedAnswering = true;
            }
        }

        public void AddVotesRange(IEnumerable<AnswerVote> votes)
        {
            foreach (var vote in votes)
            {
                var matchingAnswer = Answers.Single(_ => _.PlayerUserName == vote.AnsweringPlayerUserName
                                                        && _.Category == vote.Category);
                matchingAnswer.AddVote(vote);
            }

            PlayerVoteCount++;
            if (PlayerVoteCount == _playerCount)
            {
                IsFinishedVoting = true;
            }
        }

        public void MarkAsClosed()
        {
            IsFinishedAnswering = true;
            IsFinishedVoting = true;
        }

        private int _playerCount = 0;
        public void SetPlayerCount(int playerCount)
        {
            _playerCount = playerCount;
        }
    }
}
