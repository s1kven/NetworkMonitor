﻿using NetworkMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetworkMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionHistoryPage : ContentPage
    {
        public ConnectionHistoryPage()
        {
            InitializeComponent();
            FlexLayout IpLabel = new FlexLayout();
            this.BindingContext = new ConnectionHistoryViewModel();
        }
    }
}