using System;
using System.Collections.Generic;
using System.Text;
using FootyPunditsApp.Services;
using Xamarin.Forms;
using FootyPunditsApp.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FootyPunditsApp.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private int gameId;
        private ChatService chatService;
        private FootyPunditsAPIProxy proxy;

        public ChatViewModel(int gameId)
        {
            this.gameId = gameId;
            this.chatService = chatService;
            this.proxy = FootyPunditsAPIProxy.CreateProxy();

            CurrentUser = ((App)App.Current).CurrentUser;
            chatService = ((App)App.Current).chatService;
            Messages = new ObservableCollection<AccMessage>();
            InitializeHub();
            LoadChat();
        }

        public ICommand LikeMovieCommand => new Command<int>(async (id) => 
        {
            VotesHistory vh = await proxy.LikeMessage(id);
            if (vh != null)
            {
                CurrentUser.VotesHistories.Add(vh);
                AccMessage accMessage = CurrentUser.AccMessages.FirstOrDefault(m => m.MessageId == vh.MessageId);
                if (accMessage != null)
                {
                    accMessage.IsLiked = true;
                }
            }
        });

        public ICommand UnlikeMovieCommand => new Command<int>(async (id) =>
        {
            VotesHistory vh = await proxy.UnlikeMessage(id);
            if (vh != null)
            {
                CurrentUser.VotesHistories.Remove(vh);
                AccMessage accMessage = CurrentUser.AccMessages.FirstOrDefault(m => m.MessageId == vh.MessageId);
                if (accMessage != null)
                {
                    accMessage.IsLiked = false;
                }
            }
        });

        public async void LoadChat()
        {
            List<AccMessage> messages = await proxy.GetMessagesById(gameId);

            var accounts = messages.GroupBy(m => m.Account).Select(x => x.First()).ToList();
            foreach (AccMessage msg in accounts)
            {
                msg.Account.ProfilePicture = $"{proxy.basePhotosUri}/{msg.Account.ProfilePicture}";
            }
            foreach (AccMessage msg in messages)
            {
                if (!CurrentUser.AccMessages.Any(m => m.MessageId == msg.MessageId))
                {
                    CurrentUser.AccMessages.Add(msg);
                }
                msg.IsLiked = ((App)App.Current).CurrentUser.VotesHistories.Any(v => v.MessageId == msg.MessageId);
            }

            Messages = new ObservableCollection<AccMessage>(messages);
        }

        public async void InitializeHub()
        {
            try
            {
                chatService.RegisterToReceiveMessageFromGroup(ReceiveMessageFromGroup);
                 await chatService.Connect(new string[] { gameId.ToString() });
            }
            catch (System.Exception exp)
            {

            }
        }

        public Command SendMsgCommand => new Command(SendMsg);
        private async void SendMsg()
        {
            string text = Message;
            if (text != null && text != "")
            {
                Message = "";
                AccMessage message = new AccMessage()
                {
                    AccountId = CurrentUser.AccountId,
                    ChatGameId = gameId,
                    Content = text,
                    SentDate = DateTime.Now,
                    Account = CurrentUser
                };

                await chatService.SendMessageToGroup(new AccMessageDTO(message), gameId.ToString());
            }
        }

        private async void ReceiveMessageFromGroup(int accountId, string message, string groupId, int messageId)
        {
            UserAccount a = await proxy.GetAccountById(accountId);
            AccMessage msg = new AccMessage()
            {
                AccountId = accountId,
                ChatGameId = int.Parse(groupId),
                Content = message,
                SentDate = DateTime.Now,
                Account = a,
                MessageId = messageId
            };
            AddMessage(msg);
        }

        private void AddMessage(AccMessage message)
        {
            Messages.Add(message);
            CurrentUser.AccMessages.Add(message);
            message.Account.ProfilePicture = $"{proxy.basePhotosUri}/{message.Account.ProfilePicture}";
            MessagesLoaded?.Invoke();
        }

        private ObservableCollection<AccMessage> messages;
        public ObservableCollection<AccMessage> Messages
        {
            get => messages;
            set => SetValue(ref messages, value);
        }

        private UserAccount currentUser;
        public UserAccount CurrentUser
        {
            get => currentUser;
            set => SetValue(ref currentUser, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetValue(ref message, value);
        }

        public Action MessagesLoaded;
    }
}
