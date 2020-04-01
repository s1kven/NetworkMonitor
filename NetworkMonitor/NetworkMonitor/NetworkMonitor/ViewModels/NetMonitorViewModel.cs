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
            //ConnectionType = netInfo.ConnectionType;
            //ReceivedBytes = netInfo.GetReceivedBytes().ToString();
            //TransmittedBytes = netInfo.GetTransmittedBytes().ToString();
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
            Console.WriteLine("NetMonitorViewModel Received " + ReceivedBytes);
            TransmittedBytes = netInfo.GetTransmittedBytes().ToString();
            Console.WriteLine("NetMonitorViewModel Transmitted " + TransmittedBytes);
        }
        public string ConnectionType
        {
            get { return netMonitor.ConnectionType; }
            set
            {
                Console.WriteLine("NetMonitorViewModel  ConnectionType");
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
            double variable = Convert.ToDouble(bytes);
            if (variable < Math.Pow(kB, 1))
            { 
                return variable.ToString() + " B";
            }
            else if (variable >= Math.Pow(kB, 1) && variable < Math.Pow(kB, 2))
            {
                variable = variable / Math.Pow(kB, 1);
                return variable.ToString() + " kB";
            }
            else if (variable >= Math.Pow(kB, 2) && variable < Math.Pow(kB, 3))
            {
                variable = variable / Math.Pow(kB, 2);
                return variable.ToString() + " MB";
            }
            else if (variable >= Math.Pow(kB, 3) && variable < Math.Pow(kB, 4))
            {
                variable = variable / Math.Pow(kB, 3);
                return variable.ToString() + " GB";
            }
            else
            {
                variable = variable / Math.Pow(kB, 4);
                return variable.ToString() + " TB";
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
