using FootyPunditsApp.Models;
using FootyPunditsApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace FootyPunditsApp.ViewModels
{
    class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel() : base()
        {
            FootyPunditsAPIProxy proxy = FootyPunditsAPIProxy.CreateProxy();
            Account = ((App)App.Current).CurrentUser;
            Email = Account.Email;
            Username = Account.Username;
            Password = Account.Upass;
            Name = Account.AccName;        
            ProfilePicture = $"{proxy.basePhotosUri}/{Account.ProfilePicture}";
        }

        public Command SaveCommand => new Command(() => Save());
        public async void Save()
        {
            if (ValidateForm())
            {
                Loading = true;
                FootyPunditsAPIProxy proxy = FootyPunditsAPIProxy.CreateProxy();
                Account = ((App)App.Current).CurrentUser;                
                bool updateProfileSuccess = await proxy.UpdateProfile(Username, Password);
                if (updateProfileSuccess)
                {
                    Account.Username = Username;
                    Account.Upass = Password;
                    ((App)App.Current).CurrentUser = Account;
                }

                Loading = false;
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private bool ValidateForm()
        {
            ValidateUsername();
            ValidatePassword();

            return !(ShowUsernameError || ShowPasswordError);
        }

        private UserAccount account;
        public UserAccount Account
        {
            get => account;
            set => SetValue(ref account, value);
        }

        private string profilePicture;
        public string ProfilePicture
        {
            get => profilePicture;
            set => SetValue(ref profilePicture, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetValue(ref email, value);
        }

        private bool loading;
        public bool Loading
        {
            get => loading;
            set => SetValue(ref loading, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetValue(ref name, value);
        }

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set => SetValue(ref showPasswordError, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetValue(ref password, value);
        }

        private string passwordError;
        public string PasswordError
        {
            get => passwordError;
            set => SetValue(ref passwordError, value);
        }

        private void ValidatePassword()
        {
            ShowPasswordError = true;
            if (Password.Length < 8)
               PasswordError = "The password must contain 8 or above symbols";          
            else
                ShowPasswordError = false;
        }
        #endregion

        #region Username
        private bool showUsernameError;

        public bool ShowUsernameError
        {
            get => showUsernameError;
            set => SetValue(ref showUsernameError, value);
        }

        private string username;
        public string Username
        {
            get => username;
            set => SetValue(ref username, value);
        }

        private string usernameError;

        public string UsernameError
        {
            get => usernameError;
            set => SetValue(ref usernameError, value);
        }

        private void ValidateUsername()
        {
            ShowUsernameError = string.IsNullOrEmpty(Username);
        }
        #endregion

        private FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;

    }
        
}


