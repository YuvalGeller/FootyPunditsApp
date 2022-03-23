using FootballDataApi.Models;
using FootyPunditsApp.DataTests;
using FootyPunditsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;

namespace FootyPunditsApp.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            GetMatchesByDate();
        }



        private async void GetCompetitions()
        {
            FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();
            try
            {
                var competions = await proxy.Competition.GetAvailableCompetition();


                if (competitions != null)
                {
                    this.Competitions.Clear();
                    foreach (Competition c in competions)
                    {
                        Competitions.Add(c);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async void GetMatchesByDate()
        {
            FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();
            //string str = TestMatch.GetMatches();
            //IEnumerable<Object> matches = JsonConvert.DeserializeObject<IEnumerable<Object>>(str);

            var matches = await proxy.Match.GetAllMatchOfCompetition(2021);
        }

        private ObservableCollection<Competition> competitions;
        public ObservableCollection<Competition> Competitions
        {
            get => competitions;
            set
            {
                competitions = value;
                OnPropertyChanged("Competitions");
            }
        }
    }
}
