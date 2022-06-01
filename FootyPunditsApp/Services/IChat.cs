using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FootyPunditsApp.Models;

namespace FootyPunditsApp.Services
{
    public interface IChatService
    {
        Task Connect(string[] gameIds);
        Task Disconnect(string[] gameIds);
        //Task SendMessage(string userId, string message);
        Task SendMessageToGroup(AccMessageDTO message, string groupName);
        //void RegisterToReceiveMessage(Action<string, string> GetMessageAndUser);
        void RegisterToReceiveMessageFromGroup(Action<int, string, string> GetMessageAndUserFromGroup);
    }
}
