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
    public class ConnectionHistoryViewModel
    {
        private DataMapper mapper;
        private ConnectionHistory history;
        public ConnectionHistoryViewModel()
        {
            mapper = new DataMapper();
            history = new ConnectionHistory();
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
        }
    }
}
