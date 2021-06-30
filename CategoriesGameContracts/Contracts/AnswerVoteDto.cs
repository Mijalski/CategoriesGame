namespace CategoriesGameContracts.Contracts
{
    public record AnswerVoteDto(string Category, bool IsPositive, string AnsweringPlayerUserName);
}
