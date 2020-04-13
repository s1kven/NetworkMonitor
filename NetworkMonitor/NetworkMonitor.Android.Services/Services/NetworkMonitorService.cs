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
    public class NetworkMonitorService : Service
    {
        private NetInfo netInfo;
        private MidnightNotifier midNight;
        private Date date;
        private Connection connection;
        private Traffic traffic;
        //private List<Connection> connections;
        //private List<Traffic> traffics;
        private long ReceivedBytes { get; set; }
        private long TransmittedBytes { get; set; }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnCreate()
        {
            Console.WriteLine("OnCreateService");
            base.OnCreate();
            netInfo = new NetInfo();
            midNight = new MidnightNotifier();
            date = new Date();
            connection = new Connection();
            traffic = new Traffic();
            //connections = new List<Connection>();
            //traffics = new List<Traffic>();
            date.Connections = new List<Connection>();
            date.Traffics = new List<Traffic>();
            if (netInfo.type != ConnectionType.offline)
                connection.IP = netInfo.IP;
            ReceivedBytes = netInfo.GetTotalReceivedBytes();
            TransmittedBytes = netInfo.GetTotalTransmittedBytes();
            //netInfo.TrafficChanged += TrafficChanged;
            netInfo.ConnectionTypeChanged += ConnectionTypeChanged;
            midNight.DayChanged += WriteToDB;
            
        }
        private void WriteToDB(object sender, EventArgs e)
        {
            if (date.Connections != null)
            {
                Console.WriteLine("WriteToDB");
                AddInfoPastConnection();
                date.Traffics.Add(traffic);
                date.Connections.Add(connection);
                date.ConnectionDate = DateTime.Today.AddHours(-24);
                DBRepository.GetInstance().SaveDate(date);
                Console.WriteLine("SaveDate");
                foreach(Connection connection in date.Connections)
                {
                    Console.WriteLine("Save connection");
                    Console.WriteLine("connection.Id = " + connection.Id);
                    Console.WriteLine("connection.IdDate = " + connection.IdDate);
                    Console.WriteLine("connection.IP = " + connection.IP);
                    DBRepository.GetInstance().SaveConnection(connection);
                }
                foreach (Traffic traffic in date.Traffics)
                {
                    Console.WriteLine("Save traffic");
                    Console.WriteLine("traffic.Id = " + traffic.Id);
                    //Console.WriteLine("traffic.IdConnection = " + traffic.IdConnection);
                    Console.WriteLine("traffic.IdDate = " + traffic.IdDate);
                    Console.WriteLine("traffic.ReceivedBytes = " + traffic.ReceivedBytes);
                    Console.WriteLine("traffic.TransmittedBytes = " + traffic.TransmittedBytes);
                    DBRepository.GetInstance().SaveTraffic(traffic);
                }
                DBRepository.GetInstance().UpdateWithChildren(date);
                Console.WriteLine("date.Id = " + date.Id);
                foreach (Connection connection in DBRepository.GetInstance().GetConnections(date.Id))
                {
                    Console.WriteLine("Get connection");
                    Console.WriteLine("connection.Id = " + connection.Id);
                    Console.WriteLine("connection.IdDate = " + connection.IdDate);
                    Console.WriteLine("connection.IP = " + connection.IP);
                }
                foreach (Traffic traffic in DBRepository.GetInstance().GetTraffics(date.Id))
                {
                    Console.WriteLine("Get traffic");
                    Console.WriteLine("traffic.Id = " + traffic.Id);
                    //Console.WriteLine("traffic.IdConnection = " + traffic.IdConnection);
                    Console.WriteLine("traffic.IdDate = " + traffic.IdDate);
                    Console.WriteLine("traffic.ReceivedBytes = " + traffic.ReceivedBytes);
                    Console.WriteLine("traffic.TransmittedBytes = " + traffic.TransmittedBytes);
                }
            }
            //date = new Date();
            //traffic = new Traffic();
            //connection = new Connection();
            //connections = new List<Connection>();
            //traffics = new List<Traffic>();
        }
        private void ConnectionTypeChanged(object sender, string e)
        {
            Console.WriteLine("Service ConnectionTypeChanged");
            if (e != ConnectionType.offline.ToString())
            {
                AddInfoPastConnection();
                date.Traffics.Add(traffic);
                date.Connections.Add(connection);
                traffic = new Traffic();
                connection = new Connection();
                connection.IP = netInfo.IP;
            }
        }
        private void AddInfoPastConnection()
        {
            traffic.IdDate = date.Id;
            traffic.ReceivedBytes = netInfo.GetTotalReceivedBytes() - ReceivedBytes;
            Console.WriteLine("traffic.ReceivedBytes = " + traffic.ReceivedBytes);
            traffic.TransmittedBytes = netInfo.GetTotalTransmittedBytes() - TransmittedBytes;
            Console.WriteLine("traffic.TransmittedBytes = " + traffic.TransmittedBytes);
            ReceivedBytes = netInfo.GetTotalReceivedBytes();
            TransmittedBytes = netInfo.GetTotalTransmittedBytes();
            connection.IdDate = date.Id;
        }
        //public override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Console.WriteLine("OnDestroyService");
        //    netInfo.ConnectionTypeChanged -= ConnectionTypeChanged;
        //    netInfo.TrafficChanged -= TrafficChanged;
        //    midNight.DayChanged -= WriteToDB;
        //}
    }
}