using System;
using System.Collections.Generic;
using System.Linq;


namespace FootyPunditsApp.Models
{
    public partial class AccMessage
    {
        public AccMessage()
        {
            VotesHistories = new List<VotesHistory>();
        }

        public static int LoggedInAccountId { get => ((App)App.Current).CurrentUser.AccountId; }
        public int MessageId { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int ChatGameId { get; set; }
        public bool IsLiked { get; set; }

        public virtual UserAccount Account { get; set; }
        public virtual List<VotesHistory> VotesHistories { get; set; }
    }
}
