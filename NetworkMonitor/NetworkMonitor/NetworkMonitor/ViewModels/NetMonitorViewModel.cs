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

        public NetMonitorViewModel(INetInfo netInfo)
        {
            netMonitor = new NetMonitor();
            this.netInfo = netInfo;
            this.netInfo.TypeName += TypeNameChanged;
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
        public string MobileRxBytes
        {
            get { return netMonitor.MobileRxBytes; }
            set
            {
                string bytes = TrafficToString(value);
                if (netMonitor.MobileRxBytes != bytes)
                {
                    netMonitor.MobileRxBytes = bytes;
                    OnPropertyChanged("MobileRxBytes");
                }
            }
        }
        public string MobileTxBytes
        {
            get { return netMonitor.MobileTxBytes; }
            set
            {
                string bytes = TrafficToString(value);
                if (netMonitor.MobileTxBytes != bytes)
                {
                    netMonitor.MobileTxBytes = bytes;
                    OnPropertyChanged("MobileRxBytes");
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
        private void TypeNameChanged()
        {
            ConnectionType = netInfo.GetTypeName();
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
