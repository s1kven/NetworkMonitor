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
using NetworkMonitor.ViewModels;

namespace NetworkInfoLib.Android
{
    public class NetInfo : INetInfo
    {
        private ConnectionTypeChecker typeChecker;
        public delegate void TypeNameHandler();
        event TypeNameHandler TypeName;

        public NetInfo()
        {
            typeChecker = new ConnectionTypeChecker();
            typeChecker.CheckType();
            //typeChecker.ConnectionType += func;
            TypeName?.Invoke();
        }

        //public void func(bool connection)
        //{
        //    GetTypeName();
        //}
        public string GetTypeName()
        {
            if (typeChecker.IsTelephony == true)
                return "Telephony";
            else if (typeChecker.IsWiFi == true)
                return "WiFI";
            else
                return "offline";
        }
        public long GetMobileRxBytes()
        {
            return TrafficStats.MobileRxBytes;
        }
        public long GetMobileTxBytes()
        {
            return TrafficStats.MobileTxBytes;
        }
    }
}