using System;
using System.Collections.Generic;
using System.Text;
using FootyPunditsApp.Services;
using FootyPunditsApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FootyPunditsApp.ViewModels
{
    public class LeaderboardsViewModel : BaseViewModel
    {
        private FootyPunditsAPIProxy proxy;
        public LeaderboardsViewModel()
        {
            proxy = FootyPunditsAPIProxy.CreateProxy();

            LoadLeaderboard();
        }
        
        public async void LoadLeaderboard()
        {
            Dictionary<int, int> accsKeyValue = await proxy.GetLeaderboard();
            Accounts = new ObservableCollection<UserAccount>();
            int rank = 1;
            if (accsKeyValue != null)
            {
                foreach (KeyValuePair<int, int> acc in accsKeyValue)
                {
                    UserAccount account = await proxy.GetAccountById(acc.Key);
                    account.TotalUpvotes = acc.Value;
                    account.ProfilePicture = $"{proxy.basePhotosUri}/{account.ProfilePicture}";
                    account.LeaderboardPosition = rank;
                    rank++;
                    Accounts.Add(account);
                }
            }
        }

        public Command RefreshCommand => new Command(() => {
            IsRefreshing = true;
            LoadLeaderboard();
            IsRefreshing = false;
        });

        private ObservableCollection<UserAccount> accounts;
        public ObservableCollection<UserAccount> Accounts
        {
            get => accounts;
            set => SetValue(ref accounts, value);
        }


        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }
    }
}
