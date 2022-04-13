using FootyPunditsApp.Models;
using FootyPunditsApp.Services;
using FootyPunditsApp.Views;
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
    class ProfilePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action<Page> Push;
        private FootyPunditsAPIProxy proxy;
        private FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private UserAccount user;
        public UserAccount User
        {
            get
            {
                return this.user;
            }
            set
            {
                if (this.user != value)
                {
                    this.user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        private string profileImage;
        public string ProfileImage
        {
            get
            {
                return this.profileImage;
            }
            set
            {
                if (this.profileImage != value)
                {
                    this.profileImage = value;
                    OnPropertyChanged(nameof(ProfileImage));
                }
            }
        }

        private DateTime joinedAt;
        public DateTime JoinedAt
        {
            get
            {
                return this.joinedAt;
            }
            set
            {
                if (this.joinedAt != value)
                {
                    this.joinedAt = value;
                    OnPropertyChanged(nameof(JoinedAt));
                }
            }
        }

        public ProfilePageViewModel()
        {
            proxy = FootyPunditsAPIProxy.CreateProxy();
            LoadProfile();
        }

        public void LoadProfile()
        {
            User = ((App)App.Current).CurrentUser;

            Name = $"{User.AccName}";
            JoinedAt = User.SignUpDate.Date;
            //ProfileImage = $"{proxy.baseUri}/imgs/{User.ProfileImage}";

        }

        public ICommand LogOutCommand => new Command(LogOut);
        private async void LogOut()
        {
            bool success = await proxy.Logout();
            if (success)
            {
                ((App)App.Current).CurrentUser = null;
                Push?.Invoke(new LogInView());
            }
        }

        public Command ChangePfpCommand => new Command(() => ChangePfp());
        public async void ChangePfp()
        {
            if (MediaPicker.IsCaptureSupported)
            {
                FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
                {
                    Title = "Pick a profile picture"
                });

                if (result != null)
                {
                    this.imageFileResult = result;

                    var stream = await result.OpenReadAsync();
                    ImageSource imgSource = ImageSource.FromStream(() => stream);
                    if (SetImageSourceEvent != null)
                        SetImageSourceEvent(imgSource);
                    bool uploadImageSuccess = await proxy.UploadImage(imageFileResult.FullPath, $"a{User.AccountId}.jpg");
                    if (uploadImageSuccess)
                        User.ProfilePicture = $"a{User.AccountId}.jpg";
                }
            }
            else
            {
                // add error popup
            }
        }
    }
}
