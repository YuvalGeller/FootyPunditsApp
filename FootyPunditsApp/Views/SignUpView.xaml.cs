using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootyPunditsApp.ViewModels;
using FootballDataApi.Models;

namespace FootyPunditsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        private SignUpViewModel context;
        public SignUpView()
        {
            context = new SignUpViewModel();
            this.BindingContext = context;
            context.Push += (p) => Navigation.PushAsync(p);
            InitializeComponent();
        }

        private void SfAutoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            if (e.Value is Competition)
            {
                Competition c = (Competition)e.Value;
                context.ChosenCompetition = c;
                context.GetTeamsInCompetition();
                teamsAutoComplete.DataSource = context.Teams;
                teamsAutoComplete.ItemsSource = context.Teams;
            }
             
        }
    }
}