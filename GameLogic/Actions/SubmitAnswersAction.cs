using GameLogic.Dtos;
using System.Collections.Generic;

namespace CategoriesGameServer.Logics
{
    public class SubmitAnswersAction
    {
        public IEnumerable<CategoryWithAnswerDto> Answers { get; set; }
        public string PlayerId { get; set; }
    }
}
