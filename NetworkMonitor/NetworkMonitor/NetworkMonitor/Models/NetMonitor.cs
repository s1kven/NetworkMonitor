using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Models
{
    public class NetMonitor
    {
        public string ConnectionType { get; set; }
        public string ReceivedSpeed { get; set; }
        public string TransmittedSpeed { get; set; }
        public string MaxReceivedSpeed { get; set; }
        public string MaxTransmittedSpeed { get; set; }
        public string ReceivedBytes { get; set; }
        public string TransmittedBytes { get; set; }
        public string ConnectionTimer { get; set; }
    }
}
