﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootyPunditsApp.Models;
using FootyPunditsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FootyPunditsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ContentView
    {
        private ChatViewModel context;
        public ChatView()
        {
            InitializeComponent();
            if (this.BindingContext != null)
            {
                context = (ChatViewModel)this.BindingContext;
                context.MessagesLoaded += ScrollToBottom;
            }
        }

        public void ScrollToBottom()
        {
            AccMessage lastMessage = context.Messages.FirstOrDefault();
            if (lastMessage != null)
                messages.ScrollTo(lastMessage, ScrollToPosition.End);
        }
    }
}