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
                    Console.WriteLine("connection.ConnectionType = " + connection.ConnectionType);
                    Console.WriteLine("connection.IP = " + connection.IP);
                    DBRepository.GetInstance().SaveConnection(connection);

                    //DBRepository.GetInstance().UpdateWithChildren(DBRepository.GetInstance().GetConnection(connection.IP));
                }
                foreach (Traffic traffic in date.Traffics)
                {
                    //if(IsExistIpDB(connection))
                    //{
                    //    traffic.IdConnection = DBRepository.GetInstance().GetConnection(connection.IP).Id;
                    //}
                    Console.WriteLine("Save traffic");
                    Console.WriteLine("traffic.Id = " + traffic.Id);
                    Console.WriteLine("traffic.IdConnection = " + traffic.IdConnection);
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
                    Console.WriteLine("connection.ConnectionType = " + connection.ConnectionType);
                    Console.WriteLine("connection.IP = " + connection.IP);
                }
                foreach (Traffic traffic in DBRepository.GetInstance().GetTraffics(date.Id))
                {
                    Console.WriteLine("Get traffic");
                    Console.WriteLine("traffic.Id = " + traffic.Id);
                    Console.WriteLine("traffic.IdConnection = " + traffic.IdConnection);
                    Console.WriteLine("traffic.IdDate = " + traffic.IdDate);
                    Console.WriteLine("traffic.ReceivedBytes = " + traffic.ReceivedBytes);
                    Console.WriteLine("traffic.TransmittedBytes = " + traffic.TransmittedBytes);
                }
            }
            //date = new Date();
            //traffic = new Traffic();
            //connection = new Connection();
        }
        //private bool IsExistIpDB(Connection connection)
        //{
        //    if (DBRepository.GetInstance().GetConnection(connection.IP) == null)
        //        return false;
        //    else
        //        return true;
        //}
        //private bool IsExistIpService(Connection connection, List<Connection> connections)
        //{
        //    bool isExistIP = false;
        //    foreach(Connection con in connections)
        //    {
        //        if (connection.IP == con.IP)
        //            isExistIP = true;
        //    }
        //    return isExistIP;
        //}
        private void ConnectionTypeChanged(object sender, string type)
        {
            Console.WriteLine("Service ConnectionTypeChanged");
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
        //    Console.WriteLine("OnDestroyService");
        //    netInfo.ConnectionTypeChanged -= ConnectionTypeChanged;
        //    midNight.DayChanged -= WriteToDB;
        //}
    }
}