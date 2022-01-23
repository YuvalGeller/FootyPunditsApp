using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using FootyPunditsApp.Services;
using FootyPunditsApp.Models;
using FootyPunditsApp.Views;
using System.Threading.Tasks;

namespace FootyPunditsApp.ViewModels
{
    class TabControlViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public TabControlViewModel(int startIndex)
        {
            SelectedViewModelIndex = startIndex;
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

        public event Action<Page> Push;
    }
}