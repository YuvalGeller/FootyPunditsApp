using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootyPunditsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FootyPunditsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInView : ContentPage
    {
        public LogInView()
        {         
            LoginViewModel context = new LoginViewModel();
            this.BindingContext = context;
            InitializeComponent();
            context.Push += (p) => Navigation.PushAsync(p);


        }
    }
}