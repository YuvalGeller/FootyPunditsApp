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

namespace FootyPunditsApp.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            Competitions = new ObservableCollection<CompetitionsHelper>();
            GetMatchesByDate("2022-05-22", "2022-05-22");
        }



        //private async void GetCompetitions()
        //{
        //    FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();
        //    try
        //    {
        //        var competions = await proxy.Competition.GetAvailableCompetition();


        //        if (competitions != null)
        //        {
        //            this.Competitions.Clear();
        //            foreach (Competition c in competions)
        //            {
        //                Competitions.Add(c);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

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
    }
}
