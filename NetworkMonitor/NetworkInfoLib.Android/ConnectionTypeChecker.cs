using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Connectivity;

namespace NetworkInfoLib.Android
{
    public class ConnectionTypeChecker
    {
        public bool IsWiFi { get; private set; }
        public bool IsTelephony { get; private set; }
        //public delegate void ConnectionTypeHandler(bool connection);
        //public event ConnectionTypeHandler ConnectionType;
        public ConnectionTypeChecker()
        {
            CrossConnectivity.Current.ConnectivityChanged += CurrentConnectivityChanged;
        }
        internal void CheckType()
        {
            if (CrossConnectivity.Current != null &&
                CrossConnectivity.Current.ConnectionTypes != null &&
                CrossConnectivity.Current.IsConnected == true)
            {
                var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                if (connectionType == Plugin.Connectivity.Abstractions.ConnectionType.Cellular)
                {
                    IsTelephony = true;
                    //ConnectionType?.Invoke(IsTelephony);
                }
                else if (connectionType == Plugin.Connectivity.Abstractions.ConnectionType.WiFi)
                {
                    IsWiFi = true;
                    //ConnectionType?.Invoke(IsWiFi);
                }
            }
            else
            {
                IsTelephony = false;
                IsWiFi = false;
                //ConnectionType?.Invoke(IsTelephony);
                //ConnectionType?.Invoke(IsWiFi);
            }
        }
        private void CurrentConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            CheckType();
        }
    }
}