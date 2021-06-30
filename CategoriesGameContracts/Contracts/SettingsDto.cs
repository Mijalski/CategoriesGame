using System.Collections.Generic;

namespace CategoriesGameContracts.Contracts
{
    public record SettingsDto(int RoundCount, IEnumerable<string> Categories, int MaxRoundTime, int TimeToVote, int? RoundTimeAfterFirstAnswer);
}
