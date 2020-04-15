using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NetworkInfoLib.Android
{
    public static class CurrentStats
    {
        public static event EventHandler ComponentChanged;
        private static long receivedSpeed;
        public static long ReceivedSpeed 
        {
            get { return receivedSpeed; }
            set
            {
                receivedSpeed = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
        private static long transmittedSpeed;
        public static long TransmittedSpeed 
        {
            get { return transmittedSpeed; }
            set
            {
                transmittedSpeed = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
        private static long maxReceivedSpeed;
        public static long MaxReceivedSpeed
        {
            get { return maxReceivedSpeed; }
            set
            {
                maxReceivedSpeed = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
        private static long maxTransmittedSpeed;
        public static long MaxTransmittedSpeed
        {
            get { return maxTransmittedSpeed; }
            set
            {
                maxTransmittedSpeed = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
        private static long receivedBytes;
        public static long ReceivedBytes
        {
            get { return receivedBytes; }
            set
            {
                receivedBytes = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
        private static long transmittedBytes;
        public static long TransmittedBytes
        {
            get { return transmittedBytes; }
            set
            {
                transmittedBytes = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
        private static TimeSpan connectionDuration;
        public static TimeSpan ConnectionDuration
        {
            get { return connectionDuration; }
            set
            {
                connectionDuration = value;
                ComponentChanged?.Invoke(null, null);
            }
        }
    }
}