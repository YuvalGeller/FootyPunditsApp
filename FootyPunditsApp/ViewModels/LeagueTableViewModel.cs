using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using FootyPunditsApp.Services;
using FootyPunditsApp.Models;
using System.Linq;
using Xamarin.Essentials;
using FootyPunditsApp.Views;
using FootballDataApi.Models;

namespace FootyPunditsApp.ViewModels
{
    public class LeagueTableViewModel : BaseViewModel
    {
        private int competitionId;
        public LeagueTableViewModel(int compId)
        {
            competitionId = compId;
            GetTableByCompetition();
        }

        private async void GetTableByCompetition()
        {
            FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();

            var standings = await proxy.Standing.GetStandingOfCompetition(competitionId);
            Table = standings.Standings.First().Table.ToList();
        }

        private List<Ranking> table;
        public List<Ranking> Table
        {
            get => table;
            set
            {
                table = value;
                OnPropertyChanged("Table");
            }
        }

    }

}
