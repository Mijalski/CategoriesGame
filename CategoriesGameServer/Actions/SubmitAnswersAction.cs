using CategoriesGameContracts.Contracts;
using System.Collections.Generic;

namespace CategoriesGameServer.Actions
{
    public class SubmitAnswersAction
    {
        public IEnumerable<CategoryWithAnswerDto> Answers { get; set; }
        public string PlayerUserName { get; set; }
        public string Code { get; set; }
    }
}
