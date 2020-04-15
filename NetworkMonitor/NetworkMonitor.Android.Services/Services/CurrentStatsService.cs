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
using NetworkInfoLib.Android;

namespace NetworkMonitor.Services.Services
{
    [Service]
    public class CurrentStatsService : Service
    {
        private NetInfo netInfo;
        private const int defaultValue = 0;
        private const int interval = 1000;
        private DateTime timeStartConnection;
        private static Timer timer = new Timer();
        private long startReceivedBytes;
        private long startTransmittedBytes;
        private long TotalReceivedBytes { get; set; }
        private long TotalTransmittedBytes { get; set; }
        private long ReceivedSpeed { get; set; }
        private long TransmittedSpeed { get; set; }
        private long MaxReceivedSpeed { get; set; }
        private long MaxTransmittedSpeed { get; set; }
        private long ConnectionReceivedBytes { get; set; }
        private long ConnectionTransmittedBytes { get; set; }

        public override void OnCreate()
        {
            base.OnCreate();
            netInfo = new NetInfo();
            netInfo.ConnectionTypeChanged += ConnectionTypeChanged;
            startReceivedBytes = netInfo.GetTotalReceivedBytes();
            startTransmittedBytes = netInfo.GetTotalTransmittedBytes();
            TotalReceivedBytes = netInfo.GetTotalReceivedBytes();
            TotalTransmittedBytes = netInfo.GetTotalTransmittedBytes();
            if (netInfo.type != ConnectionType.offline)
            {
                InitTimer();
                timeStartConnection = DateTime.Now;
            }
        }

        private void ConnectionTypeChanged(object sender, string e)
        {
            if (netInfo.type != ConnectionType.offline)
            {
                timer.Enabled = true;
                startReceivedBytes = netInfo.GetTotalReceivedBytes();
                startTransmittedBytes = netInfo.GetTotalTransmittedBytes();
                TotalReceivedBytes = netInfo.GetTotalReceivedBytes();
                TotalTransmittedBytes = netInfo.GetTotalTransmittedBytes();
                timeStartConnection = DateTime.Now;
            }
            else
            {
                SetReceivedSpeed(defaultValue);
                SetMaxReceivedSpeed(defaultValue);
                SetConnectionReceivedBytes(defaultValue);
                SetTransmittedSpeed(defaultValue);
                SetMaxTransmittedSpeed(defaultValue);
                SetConnectionTransmittedBytes(defaultValue);
                SetConnectionDuration(defaultValue);
                timer.Enabled = false;
            }
        }

        private void InitTimer()
        {
            timer.Interval = interval;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetReceivedSpeed();
            SetMaxReceivedSpeed();
            SetConnectionReceivedBytes();
            SetReceivedBytes();
            SetTransmittedSpeed();
            SetMaxTransmittedSpeed();
            SetConnectionTransmittedBytes();
            SetTransmittedBytes();
            SetConnectionDuration();
        }

        private void SetReceivedSpeed()
        {
            ReceivedSpeed = netInfo.GetTotalReceivedBytes() - TotalReceivedBytes;
            CurrentStats.ReceivedSpeed = ReceivedSpeed;
        }
        private void SetReceivedSpeed(int defaultValue)
        {
            ReceivedSpeed = defaultValue;
            CurrentStats.ReceivedSpeed = ReceivedSpeed;
        }
        private void SetTransmittedSpeed()
        {
            TransmittedSpeed = netInfo.GetTotalTransmittedBytes() - TotalTransmittedBytes;
            CurrentStats.TransmittedSpeed = TransmittedSpeed;
        }
        private void SetTransmittedSpeed(int defaultValue)
        {
            TransmittedSpeed = defaultValue;
            CurrentStats.TransmittedSpeed = TransmittedSpeed;
        }
        private void SetMaxReceivedSpeed()
        {
            if (MaxReceivedSpeed < ReceivedSpeed)
            {
                MaxReceivedSpeed = ReceivedSpeed;
                CurrentStats.MaxReceivedSpeed = MaxReceivedSpeed;
            }
        }
        private void SetMaxReceivedSpeed(int defaultValue)
        {
            MaxReceivedSpeed = defaultValue;
            CurrentStats.MaxReceivedSpeed = MaxReceivedSpeed;
        }
        private void SetMaxTransmittedSpeed()
        {
            if (MaxTransmittedSpeed < TransmittedSpeed)
            {
                MaxTransmittedSpeed = TransmittedSpeed;
                CurrentStats.MaxTransmittedSpeed = MaxTransmittedSpeed;
            }
        }
        private void SetMaxTransmittedSpeed(int defaultValue)
        {
            MaxTransmittedSpeed = defaultValue;
            CurrentStats.MaxTransmittedSpeed = MaxTransmittedSpeed;
        }
        private void SetConnectionReceivedBytes()
        {
            ConnectionReceivedBytes = netInfo.GetTotalReceivedBytes() - startReceivedBytes;
            CurrentStats.ReceivedBytes = ConnectionReceivedBytes;
        }
        private void SetConnectionReceivedBytes(int defaultValue)
        {
            ConnectionReceivedBytes = defaultValue;
            CurrentStats.ReceivedBytes = ConnectionReceivedBytes;
        }
        private void SetConnectionTransmittedBytes()
        {
            ConnectionTransmittedBytes = netInfo.GetTotalTransmittedBytes() - startTransmittedBytes;
            CurrentStats.TransmittedBytes = ConnectionTransmittedBytes;
        }
        private void SetConnectionTransmittedBytes(int defaultValue)
        {
            ConnectionTransmittedBytes = defaultValue;
            CurrentStats.TransmittedBytes = ConnectionTransmittedBytes;
        }
        private void SetConnectionDuration()
        {
            CurrentStats.ConnectionDuration = DateTime.Now - timeStartConnection;
        }
        private void SetConnectionDuration(int defaultValue)
        {
            CurrentStats.ConnectionDuration = new TimeSpan();
        }
        private void SetReceivedBytes()
        {
            TotalReceivedBytes = netInfo.GetTotalReceivedBytes();
        }
        private void SetTransmittedBytes()
        {
            TotalTransmittedBytes = netInfo.GetTotalTransmittedBytes();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}