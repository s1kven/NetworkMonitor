using NetworkMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace NetworkMonitor.ViewModels
{
    public class ConnectionHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ConnectionHistory history;

        Date date;
        Connection connection;
        public ConnectionHistoryViewModel()
        {
            history = new ConnectionHistory();
            date = new Date();
            connection = new Connection();
            connection.IP = "123.54.74.23";
            connection.ReceivedBytes = 12345323;
            connection.TransmittedBytes = 74564564;
            date.DateString = "01.01.2020";
            date.Connections = new List<Connection>();
            date.Connections.Add(connection);
            history.Dates = new List<Date>();
            history.Dates.Add(date);
            //history.Dates = new List<Date>()
            //{
            //    new Date()
            //    { DateString = "01.01.2020", Connections = new List<Connection>()
            //        {
            //            new Connection() {IP = "123.54.74.23", ReceivedBytes = 12345323, TransmittedBytes = 74564564},
            //            new Connection() {IP = "153.76.34.123", ReceivedBytes = 345323, TransmittedBytes = 745564},
            //        }
            //    }
            //};
        }
        public List<Date> Dates 
        {
            get { return history.Dates; }
            set
            {
                if (history.Dates != value)
                {
                    history.Dates = value;
                    OnPropertyChanged("Dates");
                }
            }
        }
        public List<Connection> Connections
        {
            get { return date.Connections; }
            set
            {
                if(date.Connections != value)
                {
                    date.Connections = value;
                    OnPropertyChanged("Connections");
                }
            }
        }
        public string Date
        {
            get { return date.DateString; }
            set
            {
                if (date.DateString != value)
                {
                    date.DateString = value;
                    OnPropertyChanged("DateString");
                }
            }
        }
        public string IP 
        { 
            get { return connection.IP; }
            set
            {
                if (connection.IP != value)
                {
                    connection.IP = value;
                    OnPropertyChanged("IP");
                }
            }
        }
        public long ReceivedBytes
        {
            get { return connection.ReceivedBytes; }
            set
            {
                if (connection.ReceivedBytes != value)
                {
                    connection.ReceivedBytes = value;
                    OnPropertyChanged("ReceivedBytes");
                }
            }
        }
        public long TransmittedBytes
        {
            get { return connection.TransmittedBytes; }
            set
            {
                if (connection.TransmittedBytes != value)
                {
                    connection.TransmittedBytes = value;
                    OnPropertyChanged("TransmittedBytes");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
