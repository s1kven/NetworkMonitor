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
        private ConnectionTypeChecker typeChecker;

        private TelephonyInfo telephonyInfo;
        private WiFiInfo wifiInfo;
        private const long defaultBytes = 0;

        internal ConnectionType type;

        public event EventHandler<string> ConnectionTypeChanged;
        public event EventHandler TrafficChanged;

        public string ConnectionType { get; set; }

        public NetInfo()
        {
            Console.WriteLine("NetInfo");
            typeChecker = new ConnectionTypeChecker();
            typeChecker.ConnectionTypeChanged += CurrentConnectionTypeChanged;
            telephonyInfo = new TelephonyInfo();
            wifiInfo = new WiFiInfo();
        }

        private string GetConnectionType()
        {
            Console.WriteLine("NetInfo GetTypeName");
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
            Console.WriteLine("NetInfo ConType");
            ConnectionType = GetConnectionType();
            ConnectionTypeChanged?.Invoke(this, ConnectionType);
            TrafficChanged?.Invoke(this, null);
        }
        public void CheckConnectionType()
        {
            Console.WriteLine("NetInfo CheckType");
            typeChecker.CheckConnectionType();
        }
        public long GetReceivedBytes()
        {
            Console.WriteLine("NetInfo GetReceivedBytes");
            switch(type)
            {
                case Android.ConnectionType.Telephony:
                    Console.WriteLine(telephonyInfo.ReceivedBytes.ToString());
                    return telephonyInfo.ReceivedBytes;
                case Android.ConnectionType.WiFi:
                    Console.WriteLine(wifiInfo.ReceivedBytes.ToString());
                    return wifiInfo.ReceivedBytes;
                case Android.ConnectionType.offline:
                    Console.WriteLine(defaultBytes.ToString());
                    return defaultBytes;
                default:
                    throw new Exception();
            }
        }
        public long GetTransmittedBytes()
        {
            Console.WriteLine("NetInfo GetTransmittedBytes");
            switch (type)
            {
                case Android.ConnectionType.Telephony:
                    Console.WriteLine(telephonyInfo.TransmittedBytes.ToString());
                    return telephonyInfo.TransmittedBytes;
                case Android.ConnectionType.WiFi:
                    Console.WriteLine(wifiInfo.TransmittedBytes.ToString());
                    return wifiInfo.TransmittedBytes;
                case Android.ConnectionType.offline:
                    Console.WriteLine(defaultBytes.ToString());
                    return defaultBytes;
                default:
                    throw new Exception();
            }
        }

        public void Dispose()
        {
            typeChecker.ConnectionTypeChanged -= CurrentConnectionTypeChanged;
        }
    }
}