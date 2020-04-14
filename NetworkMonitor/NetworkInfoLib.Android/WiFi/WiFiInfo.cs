using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace NetworkInfoLib.Android.WiFi
{
    public class WiFiInfo : IConnectionInfo
    {
        public long ReceivedBytes
        {
            get
            {
                return (TrafficStats.TotalRxBytes - TrafficStats.MobileRxBytes);
            }
            set
            {
                ReceivedBytes = value;
            }
        }
        public long TransmittedBytes
        {
            get
            {
                return (TrafficStats.TotalTxBytes - TrafficStats.MobileTxBytes);
            }
            set
            {
                TransmittedBytes = value;
            }
        }
        public string GetIPAddress()
        {
            var AllNetworkInterfaces = Collections.List(Java.Net.NetworkInterface.NetworkInterfaces);
            var IPAddress = "";
            foreach (var interfaces in AllNetworkInterfaces)
            {
                var AddressInterface = (interfaces as Java.Net.NetworkInterface).InterfaceAddresses;
                foreach (var AInterface in AddressInterface)
                {
                    if (AInterface.Broadcast != null)
                        IPAddress = AInterface.Address.HostAddress;
                }
            }
            return IPAddress;
        }
    }
}