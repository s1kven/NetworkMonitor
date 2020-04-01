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
    internal class ConnectionTypeChecker : IDisposable
    {
        internal bool IsWiFi { get; private set; }
        internal bool IsTelephony { get; private set; }
        internal delegate void ConnectionTypeHandler();
        internal event ConnectionTypeHandler ConnectionTypeChanged;
        internal ConnectionTypeChecker()
        {
            Console.WriteLine("ConnectionTypeChecker");
            CrossConnectivity.Current.ConnectivityChanged += CurrentConnectivityChanged;
        }
        internal void CheckConnectionType()
        {
            Console.WriteLine("ConnectionTypeChecker CheckType");
            if (CrossConnectivity.Current != null &&
                CrossConnectivity.Current.ConnectionTypes != null &&
                CrossConnectivity.Current.IsConnected == true)
            {
                var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                if (connectionType == Plugin.Connectivity.Abstractions.ConnectionType.Cellular)
                    IsTelephony = true;
                else if (connectionType == Plugin.Connectivity.Abstractions.ConnectionType.WiFi)
                    IsWiFi = true;
            }
            else
            {
                IsTelephony = false;
                IsWiFi = false;
            }
            ConnectionTypeChanged?.Invoke();
        }
        private void CurrentConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Console.WriteLine("ConnectionTypeChecker CurrentConnectivityChanged");
            CheckConnectionType();
        }

        public void Dispose()
        {
            CrossConnectivity.Current.ConnectivityChanged -= CurrentConnectivityChanged;
        }
    }
}