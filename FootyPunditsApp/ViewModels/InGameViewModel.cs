using System;
using System.Collections.Generic;
using System.Text;
using FootballDataApi.Models;
using FootyPunditsApp.Services;

namespace FootyPunditsApp.ViewModels
{
    public class InGameViewModel : BaseViewModel
    {
        public InGameViewModel(Match m)
        {
            Match = m;
            SelectedIndex = 0;

            FootballDataAPIProxy proxy = FootballDataAPIProxy.CreateProxy();
            LeagueTableViewModel = new LeagueTableViewModel(m.Competition.Id);
        }

        #region Selected Tab Index
        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        #endregion

        private Match match;
        public Match Match
        {
            get => match;
            set
            {
                match = value;
                OnPropertyChanged("Match");
            }
        }

        public LeagueTableViewModel LeagueTableViewModel { get; set; }
    }
}
