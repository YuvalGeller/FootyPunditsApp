﻿using System;
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
    public partial class HomePageView : ContentView
    {
        public HomePageView()
        {
            HomePageViewModel context = new HomePageViewModel();
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}