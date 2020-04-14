using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Models
{
    public class Connection
    {
        public string ConnectionType { get; set; }
        public string IP { get; set; }
        public string ReceivedBytes { get; set; }
        public string TransmittedBytes { get; set; }
    }
}
