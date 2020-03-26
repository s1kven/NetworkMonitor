using NetworkMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public class NetMonitorViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private NetMonitor netMonitor;

        public NetMonitorViewModel()
        {
            netMonitor = new NetMonitor();
        }
        public string NetworkTitle
        {
            get { return netMonitor.NetworkTitle; }
            set
            {
                if (netMonitor.NetworkTitle != value)
                {
                    netMonitor.NetworkTitle = value;
                    OnPropertyChanged("NetworkTitle");
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
