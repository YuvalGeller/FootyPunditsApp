using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballDataApi.Models;
using FootyPunditsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FootyPunditsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InGameView : ContentPage
    {
        public InGameView(Match m)
        {
            this.BindingContext = new InGameViewModel(m);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            chatTab.ScrollToBottom();
            base.OnAppearing();
        }
    }
}