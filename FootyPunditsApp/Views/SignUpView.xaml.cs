using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootyPunditsApp.ViewModels;

namespace FootyPunditsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        public SignUpView()
        {
            SignUpViewModel context = new SignUpViewModel();
            this.BindingContext = context;
            context.Push += (p) => Navigation.PushAsync(p);
            InitializeComponent();
        }
    }
}