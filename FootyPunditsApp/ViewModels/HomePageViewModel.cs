using FootballDataApi.Models;
using FootyPunditsApp.DataTests;
using FootyPunditsApp.Services;
using FootyPunditsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using FootyPunditsApp.Views;

namespace FootyPunditsApp.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            Competitions = new ObservableCollection<CompetitionsHelper>();
            MatchDate = "2022-05-22"; //= DateTime.Now.Date;
            GetMatchesByDate(MatchDate, MatchDate);
        }

        private async void GetMatchesByDate(string dateFrom, string dateTo)
        {
            FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();

            var matches = await proxy.Match.GetAllMatches("dateFrom", dateFrom, "dateTo", dateTo);

            foreach (Match match in matches)
            {
                Competition competition = match.Competition;

                CompetitionsHelper compHelper = Competitions.FirstOrDefault(c => c.Id == competition.Id);
                if (compHelper == null)
                {
                    compHelper = new CompetitionsHelper()
                    {
                        CompetitionName = competition.Name,
                        Id = competition.Id,
                        Matches = new ObservableCollection<Match>()
                    };
                    compHelper.Matches.Add(match);
                    Competitions.Add(compHelper);
                }
                else
                {
                    compHelper.Matches.Add(match);
                }
            }
        }

        public ICommand MatchClickedCommand => new Command<Match>((m) => MatchClicked(m));
        private void MatchClicked(Match m)
        {
            ((App)App.Current).MainPage.Navigation.PushAsync(new InGameView(m));
        }

        private ObservableCollection<CompetitionsHelper> competitions;
        public ObservableCollection<CompetitionsHelper> Competitions
        {
            get => competitions;
            set
            {
                competitions = value;
                OnPropertyChanged("Competitions");
            }
        }

        private string matchDate;
        public string MatchDate
        {
            get => matchDate;
            set
            {
                matchDate = value;
                OnPropertyChanged("MatchDate");
            }
        }
    }
}
