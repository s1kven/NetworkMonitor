using NetworkMonitor.DB;
using NetworkMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NetworkMonitor.ViewModels
{
    public class ConnectionHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataMapper mapper;
        private ConnectionHistory history;
        private Date date;
        private Connection connection;
        public ConnectionHistoryViewModel()
        {
            mapper = new DataMapper();
            history = new ConnectionHistory();
            date = new Date();
            connection = new Connection();
            history.Dates = new List<Date>();
            GetHistory(mapper);
        }
        private void GetHistory(DataMapper mapper)
        {
            foreach (Date date in mapper.GetDates())
            {
                history.Dates.Add(date);
            }
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
