using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;
using Xamarin.Forms;
using FootyPunditsApp.Models;

namespace FootyPunditsApp.Services
{
    public class ChatService : IChatService
    {
        private readonly HubConnection hubConnection;
        public ChatService()
        {
            string baseUri = FootyPunditsAPIProxy.CreateProxy().baseUriClean;
            hubConnection = new HubConnectionBuilder().WithUrl($"{baseUri}/chathub").Build();
        }

        public async Task Connect(string[] gameIds)
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("OnConnect", gameIds);
        }

        public async Task Disconnect(string[] gameIds)
        {
            await hubConnection.InvokeAsync("OnDisconnect", gameIds);
            await hubConnection.StopAsync();
        }

        public async Task SendMessageToGroup(AccMessageDTO message, string gameId)
        {
            await hubConnection.InvokeAsync("SendMessageToGroup", message, gameId);
        }

        public void RegisterToReceiveMessageFromGroup(Action<int, string, string, int> GetMessageAndUserFromGroup)
        {
            hubConnection.On("ReceiveMessageFromGroup", GetMessageAndUserFromGroup);
        }

    }
}
