using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Models
{
    public class NetMonitor
    {
        public string ConnectionType { get; set; }
        public double DownloadSpeed { get; set; }
        public double UploadSpeed { get; set; }
        public double MaxDownloadSpeed { get; set; }
        public double MaxUploadSpeed { get; set; }
        public string MobileRxBytes { get; set; }
        public string MobileTxBytes { get; set; }
        public double NetworkTimer { get; set; }
    }
}
