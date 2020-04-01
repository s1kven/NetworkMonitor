using NetworkMonitor.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public class NetMonitorViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private NetMonitor netMonitor;
        private INetInfo netInfo;

        public NetMonitorViewModel()
        {
            Console.WriteLine("NetMonitorViewModel");
            netMonitor = new NetMonitor();
            netInfo = Xamarin.Forms.DependencyService.Get<INetInfo>();
            netInfo.ConnectionTypeChanged += ConnectionTypeChanged;
            netInfo.TrafficChanged += TrafficChanged;
            netInfo.CheckConnectionType();
        }

        private void ConnectionTypeChanged(object sender, string e)
        {
            Console.WriteLine("TypeNameChanged");
            ConnectionType = netInfo.ConnectionType;
        }
        private void TrafficChanged(object sender, EventArgs e)
        {
            Console.WriteLine("TrafficChanged");
            ReceivedBytes = netInfo.GetReceivedBytes().ToString();
            TransmittedBytes = netInfo.GetTransmittedBytes().ToString();
        }
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
                string bytes = TrafficToString(value);
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
                string bytes = TrafficToString(value);
                if (netMonitor.TransmittedBytes != bytes)
                {
                    netMonitor.TransmittedBytes = bytes;
                    OnPropertyChanged("TransmittedBytes");
                }
            }
        }
        private string TrafficToString(string bytes)
        {
            const int kB = 1000;
            double traffic = Convert.ToDouble(bytes);
            if (traffic < Math.Pow(kB, 1))
            { 
                return traffic.ToString() + " B";
            }
            else if (traffic >= Math.Pow(kB, 1) && traffic < Math.Pow(kB, 2))
            {
                traffic = traffic / Math.Pow(kB, 1);
                return traffic.ToString() + " kB";
            }
            else if (traffic >= Math.Pow(kB, 2) && traffic < Math.Pow(kB, 3))
            {
                traffic = traffic / Math.Pow(kB, 2);
                return traffic.ToString() + " MB";
            }
            else if (traffic >= Math.Pow(kB, 3) && traffic < Math.Pow(kB, 4))
            {
                traffic = traffic / Math.Pow(kB, 3);
                return traffic.ToString() + " GB";
            }
            else
            {
                traffic = traffic / Math.Pow(kB, 4);
                return traffic.ToString() + " TB";
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
