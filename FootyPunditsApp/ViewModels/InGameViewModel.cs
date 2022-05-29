using System;
using System.Collections.Generic;
using System.Text;
using FootballDataApi.Models;

namespace FootyPunditsApp.ViewModels
{
    public class InGameViewModel : BaseViewModel
    {
        public InGameViewModel(Match m)
        {
            Match = m;
            SelectedViewModelIndex = 0;
        }

        #region Selected Tab Index
        private int selectedViewModelIndex;
        public int SelectedViewModelIndex
        {
            get => selectedViewModelIndex;
            set
            {
                selectedViewModelIndex = value;
                OnPropertyChanged("SelectedViewModelIndex");
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
    }
}
