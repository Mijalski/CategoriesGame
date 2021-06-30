using CategoriesGameContracts.Contracts;
using System.Collections.Generic;

namespace CategoriesGameServer.Actions
{
    public class VoteForAnswersAction
    {
        public IEnumerable<AnswerVoteDto> Votes { get; set; }
        public string Code { get; set; }
        public string PlayerUserName { get; set; }
    }
}
