using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootyPunditsApp.Views;

namespace FootyPunditsApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv = true;

        public App()
        {
            InitializeComponent();

            MainPage = new LogInView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
