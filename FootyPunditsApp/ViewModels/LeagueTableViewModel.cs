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

namespace FootyPunditsApp.ViewModels
{
    class LeagueTableViewModel : BaseViewModel
    {
        public LeagueTableViewModel()
        {
            GetTableByCompetition();
        }

        private async void GetTableByCompetition()
        {
            FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();

            //var table = await proxy.Standing.GetStandingOfCompetition();
        }
    }
}
