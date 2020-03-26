using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Models
{
    public class NetMonitor
    {
        public string NetworkTitle { get; set; }
        public double DownloadSpeed { get; set; }
        public double UploadSpeed { get; set; }
        public double MaxDownloadSpeed { get; set; }
        public double MaxUploadSpeed { get; set; }
        public double DownloadTraffic { get; set; }
        public double UploadTraffic { get; set; }
        public double NetworkTimer { get; set; }
    }
}
