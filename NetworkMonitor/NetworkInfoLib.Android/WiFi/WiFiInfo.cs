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

namespace NetworkInfoLib.Android.WiFi
{
    public class WiFiInfo
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
    }
}