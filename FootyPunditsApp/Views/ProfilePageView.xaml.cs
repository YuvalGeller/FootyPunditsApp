﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FootyPunditsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePageView : ContentView
    {
        public ProfilePageView()
        {
            InitializeComponent();
        }

        public static implicit operator Page(ProfilePageView v)
        {
            throw new NotImplementedException();
        }
    }
}