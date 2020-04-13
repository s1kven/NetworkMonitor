using NetworkMonitor.DB;
using NetworkMonitor.DB.Tables;
using NetworkMonitor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    internal class DataMapper
    {
        internal List<Models.Date> GetDates()
        {
            List<Models.Date> dates = new List<Models.Date>();
            foreach (DB.Tables.Date dateDB in DBRepository.GetInstance().GetDates())
            {
                Models.Date date = new Models.Date();
                date.DateString = dateDB.ConnectionDate.ToString();
                date.Connections = GetConnections(dateDB.Id);
                dates.Add(date);
            }
            return dates;
        }
        private List<Models.Connection> GetConnections(int idDate)
        {
            List<Models.Connection> connections = new List<Models.Connection>();
            foreach (DB.Tables.Connection connectionDB in DBRepository.GetInstance().GetConnections(idDate))
            {
                Models.Connection connection = new Models.Connection();
                //Traffic traffic = new Traffic();
                Traffic traffic = DBRepository.GetInstance().GetTraffic(connectionDB.Id);
                connection.IP = connectionDB.IP;
                connection.ReceivedBytes = traffic.ReceivedBytes;
                connection.TransmittedBytes = traffic.TransmittedBytes;
                connections.Add(connection);
            }
            return connections;
        }
    }
}
