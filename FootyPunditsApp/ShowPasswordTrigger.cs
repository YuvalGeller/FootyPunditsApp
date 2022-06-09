using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FootyPunditsApp
{
    public class ShowPasswordTrigger : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public string showIcon { get; set; }
        public string hideIcon { get; set; }
        bool _hidePassword = true;
        public bool hidePassword
        {
            get => _hidePassword;
            set
            {
                if (_hidePassword != value)
                {
                    _hidePassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(hidePassword)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = hidePassword ? showIcon : hideIcon;
            hidePassword = !hidePassword;
        }
    }
}
