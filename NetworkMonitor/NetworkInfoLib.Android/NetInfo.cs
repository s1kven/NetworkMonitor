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
using NetworkInfoLib.Android.Telephony;
using NetworkInfoLib.Android.WiFi;
using NetworkMonitor.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(NetworkInfoLib.Android.NetInfo))]
namespace NetworkInfoLib.Android
{
    public class NetInfo : INetInfo, IDisposable
    {
        private DhcpInfo dhcp;
        private ConnectionTypeChecker typeChecker;

        private TelephonyInfo telephonyInfo;
        private WiFiInfo wifiInfo;
        private const long defaultBytes = 0;

        public ConnectionType type;

        public event EventHandler<string> ConnectionTypeChanged;
        //public event EventHandler TrafficChanged;

        public string ConnectionType { get; set; }
        public string IP { get; private set; }

        public NetInfo()
        {
            dhcp = new DhcpInfo();
            typeChecker = new ConnectionTypeChecker();
            typeChecker.ConnectionTypeChanged += CurrentConnectionTypeChanged;
            telephonyInfo = new TelephonyInfo();
            wifiInfo = new WiFiInfo();
        }

        private string GetConnectionType()
        {
            if (typeChecker.IsTelephony)
            {
                type = Android.ConnectionType.Telephony;
                return Android.ConnectionType.Telephony.ToString();
            }
            else if (typeChecker.IsWiFi)
            {
                type = Android.ConnectionType.WiFi;
                return Android.ConnectionType.WiFi.ToString();
            }
            else
            {
                type = Android.ConnectionType.offline;
                return Android.ConnectionType.offline.ToString();
            }
        }
        private void CurrentConnectionTypeChanged()
        {
            ConnectionType = GetConnectionType();
            IP = dhcp.ServerAddress.ToString();
            //TrafficChanged?.Invoke(this, null);
            ConnectionTypeChanged?.Invoke(this, ConnectionType);
        }
        public void CheckConnectionType()
        {
            typeChecker.CheckConnectionType();
        }
        public long GetReceivedBytes()
        {
            switch(type)
            {
                case Android.ConnectionType.Telephony:
                    return telephonyInfo.ReceivedBytes;
                case Android.ConnectionType.WiFi:
                    return wifiInfo.ReceivedBytes;
                case Android.ConnectionType.offline:
                    return defaultBytes;
                default:
                    throw new Exception();
            }
        }
        public long GetTransmittedBytes()
        {
            switch (type)
            {
                case Android.ConnectionType.Telephony:
                    return telephonyInfo.TransmittedBytes;
                case Android.ConnectionType.WiFi:
                    return wifiInfo.TransmittedBytes;
                case Android.ConnectionType.offline:
                    return defaultBytes;
                default:
                    throw new Exception();
            }
        }
        public long GetTotalReceivedBytes()
        {
            return TrafficStats.TotalRxBytes;
        }
        public long GetTotalTransmittedBytes()
        {
            return TrafficStats.TotalTxBytes;
        }
        public void Dispose()
        {
            typeChecker.ConnectionTypeChanged -= CurrentConnectionTypeChanged;
        }
    }
}