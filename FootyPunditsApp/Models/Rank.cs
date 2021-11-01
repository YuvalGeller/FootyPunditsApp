using System;
using System.Collections.Generic;



namespace FootyPunditsApp.Models
{
    public partial class Rank
    {
        public Rank()
        {
            UserAccounts = new List<UserAccount>();
        }

        public int RankId { get; set; }
        public int MinUpvotes { get; set; }
        public string RankName { get; set; }
        public string RankLogo { get; set; }

        public virtual List<UserAccount> UserAccounts { get; set; }
    }
}
