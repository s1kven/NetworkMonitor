using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
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
        private ConnectionTypeChecker typeChecker;

        private TelephonyInfo telephonyInfo;
        private WiFiInfo wifiInfo;

        public ConnectionType type;

        public event EventHandler<string> ConnectionTypeChanged;
        public event EventHandler CurrentStatsChanged;

        public string ConnectionType { get; set; }
        public string IP { get; private set; }

        public NetInfo()
        {
            typeChecker = new ConnectionTypeChecker();
            typeChecker.ConnectionTypeChanged += CurrentConnectionTypeChanged;
            telephonyInfo = new TelephonyInfo();
            wifiInfo = new WiFiInfo();
            ConnectionType = GetConnectionType();
            SetIP();
            CurrentStats.ComponentChanged += CurrentStatsComponentChanged;
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
        private void SetIP()
        {
            if (type == Android.ConnectionType.WiFi)
                IP = wifiInfo.GetIPAddress();
        }
        private void CurrentConnectionTypeChanged()
        {
            ConnectionType = GetConnectionType();
            SetIP();
            ConnectionTypeChanged?.Invoke(this, ConnectionType);
        }
        public void CheckConnectionType()
        {
            typeChecker.CheckConnectionType();
        }
        public long GetTotalReceivedBytes()
        {
            return TrafficStats.TotalRxBytes;
        }
        public long GetTotalTransmittedBytes()
        {
            return TrafficStats.TotalTxBytes;
        }
        private void CurrentStatsComponentChanged(object sender, EventArgs e)
        {
            CurrentStatsChanged?.Invoke(null, null);
        }
        public long GetReceivedSpeed()
        {
            return CurrentStats.ReceivedSpeed;
        }
        public long GetTransmittedSpeed()
        {
            return CurrentStats.TransmittedSpeed;
        }
        public long GetMaxReceivedSpeed()
        {
            return CurrentStats.MaxReceivedSpeed;
        }
        public long GetMaxTransmittedSpeed()
        {
            return CurrentStats.MaxTransmittedSpeed;
        }
        public long GetReceivedBytes()
        {
            return CurrentStats.ReceivedBytes;
        }
        public long GetTransmittedBytes()
        {
            return CurrentStats.TransmittedBytes;
        }
        public TimeSpan GetConnectionDuration()
        {
            return CurrentStats.ConnectionDuration;
        }
        public void Dispose()
        {
            typeChecker.ConnectionTypeChanged -= CurrentConnectionTypeChanged;
            CurrentStats.ComponentChanged -= CurrentStatsComponentChanged;
        }
    }
}