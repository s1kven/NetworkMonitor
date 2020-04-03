using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Models
{
    public class Connection
    {
        public string IP { get; set; }
        public long ReceivedBytes { get; set; }
        public long TransmittedBytes { get; set; }
    }
}
