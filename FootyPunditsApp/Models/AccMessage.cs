using System;
using System.Collections.Generic;



namespace FootyPunditsApp.Models
{
    public partial class AccMessage
    {
        public AccMessage()
        {
            VotesHistories = new List<VotesHistory>();
        }

        public int MessageId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int ChatGameId { get; set; }
        public int ChatPlayerId { get; set; }
        public int ChatTeamId { get; set; }
        public int ChatLeagueId { get; set; }

        public virtual UserAccount Account { get; set; }
        public virtual List<VotesHistory> VotesHistories { get; set; }
    }
}
