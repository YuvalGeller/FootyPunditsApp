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
    public partial class EditProfileView : ContentPage
    {
        public EditProfileView()
        {
            this.BindingContext = new EditProfileViewModel();
            InitializeComponent();
        }
    }
}