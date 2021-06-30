using GameLogic.Dtos;
using System.Collections.Generic;

namespace CategoriesGameServer.Logics
{
    public class VoteForAnswersAction
    {
        public IEnumerable<AnswerVoteDto> Votes { get; set; }
        public string PlayerId { get; set; }
    }
}
