using System.Collections.Generic;

namespace GameLogic.Dtos
{
    public record SettingsDto(int RoundCount, IEnumerable<string> Categories, int MaxRoundTime, int TimeToVote, int? RoundTimeAfterFirstAnswer);
}
