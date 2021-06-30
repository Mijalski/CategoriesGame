namespace GameLogic.Logics
{
    public class AnswerVote
    {
        public AnswerVote(string category, bool isPositive, string answeringPlayerUserName, string votingPlayerUserName)
        {
            Category = category;
            IsPositive = isPositive;
            AnsweringPlayerUserName = answeringPlayerUserName; 
            VotingPlayerUserName = votingPlayerUserName;
        }

        public string Category { get; }
        public bool IsPositive { get; }
        public string AnsweringPlayerUserName { get; }
        public string VotingPlayerUserName { get; }
    }
}
