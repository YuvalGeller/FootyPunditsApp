using System;
using System.Collections.Generic;
using System.Text;

namespace FootyPunditsApp.Models
{
    public class AccMessageDTO
    {
        public AccMessageDTO(AccMessage msg)
        {
            this.AccountId = msg.AccountId;
            this.GameId = msg.ChatGameId;
            this.Content = msg.Content;
            this.SentDate = msg.SentDate;
            this.AccountName = msg.Account.AccName;
            this.ProfilePath = msg.Account.ProfilePicture;
        }

        public int AccountId { get; set; }
        public int GameId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public string AccountName { get; set; }
        public string ProfilePath { get; set; }

    }
}
