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
            //netInfo.TrafficChanged += TrafficChanged;
            netInfo.CheckConnectionType();
        }

        private void ConnectionTypeChanged(object sender, string e)
        {
            ConnectionType = netInfo.ConnectionType;
            ReceivedBytes = netInfo.GetReceivedBytes().ToString();
            TransmittedBytes = netInfo.GetTransmittedBytes().ToString();
        }
        //private void TrafficChanged(object sender, EventArgs e)
        //{
        //    Console.WriteLine("TrafficChanged");
        //    ReceivedBytes = netInfo.GetReceivedBytes().ToString();
        //    TransmittedBytes = netInfo.GetTransmittedBytes().ToString();
        //}
        public string ConnectionType
        {
            get { return netMonitor.ConnectionType; }
            set
            {
                if (netMonitor.ConnectionType != value)
                {
                    netMonitor.ConnectionType = value;
                    OnPropertyChanged("ConnectionType");
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
                    OnPropertyChanged("ReceivedBytes");
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
                    OnPropertyChanged("TransmittedBytes");
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void Dispose()
        {
            netInfo.ConnectionTypeChanged -= ConnectionTypeChanged;
        }
    }
}
