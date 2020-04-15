using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public interface INetInfo
    {
        event EventHandler<string> ConnectionTypeChanged;
        event EventHandler CurrentStatsChanged;
        string ConnectionType { get; set; }
        string IP { get; }
        void CheckConnectionType();
        long GetReceivedBytes();
        long GetTransmittedBytes();
        long GetReceivedSpeed();
        long GetTransmittedSpeed();
        long GetMaxReceivedSpeed();
        long GetMaxTransmittedSpeed();
        TimeSpan GetConnectionDuration();
    }
}
