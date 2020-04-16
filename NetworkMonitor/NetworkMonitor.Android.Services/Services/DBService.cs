using NetworkMonitor.DB;
using NetworkInfoLib.Android;
using NetworkMonitor.DB.Tables;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NetworkInfoLib.Android.Telephony;
using NetworkInfoLib.Android.WiFi;

namespace NetworkMonitor.Services.Services
{
    [Service]
    public class DBService : Service
    {
        private NetInfo netInfo;
        private MidnightNotifier midNight;
        private Date date;
        private Connection connection;
        private Traffic traffic;
        private long ReceivedBytes { get; set; }
        private long TransmittedBytes { get; set; }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnCreate()
        {
            base.OnCreate();
            netInfo = new NetInfo();
            midNight = new MidnightNotifier();
            date = new Date();
            connection = new Connection();
            traffic = new Traffic();
            date.Connections = new List<Connection>();
            date.Traffics = new List<Traffic>();
            SetInitConnection();
            ReceivedBytes = netInfo.GetTotalReceivedBytes();
            TransmittedBytes = netInfo.GetTotalTransmittedBytes();
            netInfo.ConnectionTypeChanged += ConnectionTypeChanged;
            midNight.DayChanged += WriteToDB;
            
        }
        private void SetInitConnection()
        {
            if (netInfo.ConnectionType != ConnectionType.offline.ToString())
            {
                connection.ConnectionType = netInfo.ConnectionType;
                if(netInfo.ConnectionType != ConnectionType.Telephony.ToString())
                    connection.IP = netInfo.IP;
            }
        }
        private void WriteToDB(object sender, EventArgs e)
        {
            if (date.Connections != null)
            {
                AddInfoPastConnection();
                date.Traffics.Add(traffic);
                date.Connections.Add(connection);
                date.ConnectionDate = DateTime.Today.AddHours(-24);
                DBRepository.GetInstance().SaveDate(date);
                foreach(Connection connection in date.Connections)
                {
                    DBRepository.GetInstance().SaveConnection(connection);
                }
                foreach (Traffic traffic in date.Traffics)
                {
                    DBRepository.GetInstance().SaveTraffic(traffic);
                }
                DBRepository.GetInstance().UpdateWithChildren(date);
            }
        }
        private void ConnectionTypeChanged(object sender, string type)
        {
            if (type != ConnectionType.offline.ToString())
            {
                AddInfoPastConnection();
                date.Traffics.Add(traffic);
                date.Connections.Add(connection);
                traffic = new Traffic();
                connection = new Connection();
                connection.ConnectionType = type;
                if (type != ConnectionType.Telephony.ToString())
                    connection.IP = netInfo.IP;
            }
        }
        private void AddInfoPastConnection()
        {
            traffic.IdDate = date.Id;
            traffic.IdConnection = connection.Id;
            traffic.ReceivedBytes = netInfo.GetTotalReceivedBytes() - ReceivedBytes;
            traffic.TransmittedBytes = netInfo.GetTotalTransmittedBytes() - TransmittedBytes;
            ReceivedBytes = netInfo.GetTotalReceivedBytes();
            TransmittedBytes = netInfo.GetTotalTransmittedBytes();
            connection.IdDate = date.Id;
        }
        //public override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    netInfo.ConnectionTypeChanged -= ConnectionTypeChanged;
        //    midNight.DayChanged -= WriteToDB;
        //}
    }
}