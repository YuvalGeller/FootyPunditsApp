using System;
using System.Collections.Generic;



namespace FootyPunditsApp.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            AccMessages = new List<AccMessage>();
            PlayerRatings = new List<PlayerRating>();
            VotesHistories = new List<VotesHistory>();
        }

        public int AccountId { get; set; }
        public string AccName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Upass { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsAdmin { get; set; }
        public int FavoriteTeam { get; set; }
        public DateTime SignUpDate { get; set; }
        public int RankId { get; set; }
        // added for leaderboards
        public int TotalUpvotes { get; set; }
        public int LeaderboardPosition { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual List<AccMessage> AccMessages { get; set; }
        public virtual List<PlayerRating> PlayerRatings { get; set; }
        public virtual List<VotesHistory> VotesHistories { get; set; }
    }
}
