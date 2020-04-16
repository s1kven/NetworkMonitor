using NetworkMonitor.Models;
using NetworkMonitor.ViewModels.Styles;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public class NetMonitorViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private NetMonitor netMonitor;
        private INetInfo netInfo;

        public NetMonitorViewModel()
        {
            netMonitor = new NetMonitor();
            netInfo = Xamarin.Forms.DependencyService.Get<INetInfo>();
            netInfo.ConnectionTypeChanged += ConnectionTypeChanged;
            netInfo.CurrentStatsChanged += CurrentStatsChanged;
            netInfo.CheckConnectionType();
        }
        public string ConnectionType
        {
            get { return netMonitor.ConnectionType; }
            set
            {
                if (netMonitor.ConnectionType != value)
                {
                    netMonitor.ConnectionType = value;
                    OnPropertyChanged(nameof(ConnectionType));
                }
            }
        }
        public string ReceivedSpeed
        {
            get { return netMonitor.ReceivedSpeed; }
            set
            {
                string speed = DataStyle.SpeedToString(value);
                if (netMonitor.ReceivedSpeed != speed)
                {
                    netMonitor.ReceivedSpeed = speed;
                    OnPropertyChanged(nameof(ReceivedSpeed));
                }
            }
        }
        public string TransmittedSpeed
        {
            get { return netMonitor.TransmittedSpeed; }
            set
            {
                string speed = DataStyle.SpeedToString(value);
                if (netMonitor.TransmittedSpeed != speed)
                {
                    netMonitor.TransmittedSpeed = speed;
                    OnPropertyChanged(nameof(TransmittedSpeed));
                }
            }
        }
        public string MaxReceivedSpeed
        {
            get { return netMonitor.MaxReceivedSpeed; }
            set
            {
                string speed = DataStyle.SpeedToString(value);
                if (netMonitor.MaxReceivedSpeed != speed)
                {
                    netMonitor.MaxReceivedSpeed = speed;
                    OnPropertyChanged(nameof(MaxReceivedSpeed));
                }
            }
        }
        public string MaxTransmittedSpeed
        {
            get { return netMonitor.MaxTransmittedSpeed; }
            set
            {
                string speed = DataStyle.SpeedToString(value);
                if (netMonitor.MaxTransmittedSpeed != speed)
                {
                    netMonitor.MaxTransmittedSpeed = speed;
                    OnPropertyChanged(nameof(MaxTransmittedSpeed));
                }
            }
        }
        public string ReceivedBytes
        {
            get { return netMonitor.ReceivedBytes; }
            set
            {
                string bytes = DataStyle.TrafficToString(value);
                if (netMonitor.ReceivedBytes != bytes)
                {
                    netMonitor.ReceivedBytes = bytes;
                    OnPropertyChanged(nameof(ReceivedBytes));
                }
            }
        }
        public string TransmittedBytes
        {
            get { return netMonitor.TransmittedBytes; }
            set
            {
                string bytes = DataStyle.TrafficToString(value);
                if (netMonitor.TransmittedBytes != bytes)
                {
                    netMonitor.TransmittedBytes = bytes;
                    OnPropertyChanged(nameof(TransmittedBytes));
                }
            }
        }
        public string ConnectionTimer
        {
            get { return netMonitor.ConnectionTimer; } 
            set
            {
                if(netMonitor.ConnectionTimer != value)
                {
                    netMonitor.ConnectionTimer = value;
                    OnPropertyChanged(nameof(ConnectionTimer));
                }
            }
        }
        private void ConnectionTypeChanged(object sender, string e)
        {
            ConnectionType = netInfo.ConnectionType;
        }
        private void CurrentStatsChanged(object sender, EventArgs e)
        {
            ReceivedSpeed = netInfo.GetReceivedSpeed().ToString();
            TransmittedSpeed = netInfo.GetTransmittedSpeed().ToString();
            MaxReceivedSpeed = netInfo.GetMaxReceivedSpeed().ToString();
            MaxTransmittedSpeed = netInfo.GetMaxTransmittedSpeed().ToString();
            ReceivedBytes = netInfo.GetReceivedBytes().ToString();
            TransmittedBytes = netInfo.GetTransmittedBytes().ToString();
            ConnectionTimer = netInfo.GetConnectionDuration().ToString();
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void Dispose()
        {
            netInfo.ConnectionTypeChanged -= ConnectionTypeChanged;
            netInfo.CurrentStatsChanged -= CurrentStatsChanged;
        }
    }
}
