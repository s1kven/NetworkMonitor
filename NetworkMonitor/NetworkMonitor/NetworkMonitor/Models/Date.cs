using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Models
{
    public class Date
    {
        public string DateString { get; set; }
        public List<Connection> Connections { get; set; }
    }
}
