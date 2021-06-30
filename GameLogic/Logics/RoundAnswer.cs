using System.Collections.Generic;
using System.Linq;

namespace GameLogic.Logics
{
    public class RoundAnswer
    {
        public RoundAnswer(string category, string answer, string playerUserName)
        {
            Category = category;
            Answer = answer;
            PlayerUserName = playerUserName;
            Votes = new List<AnswerVote>();
        }

        public string Category { get; }
        public string Answer { get; }
        public string PlayerUserName { get; }
        public IEnumerable<AnswerVote> Votes { get; private set; }

        public void AddVote(AnswerVote vote)
        {
            Votes = Votes.Append(vote);
        }
    }
}
