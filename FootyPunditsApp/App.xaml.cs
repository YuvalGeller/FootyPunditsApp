using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FootyPunditsApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv = true;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
