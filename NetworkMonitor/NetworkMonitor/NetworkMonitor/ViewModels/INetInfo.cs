using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public interface INetInfo
    {
        //delegate void ConnectionTypeHandler();
        event EventHandler<string> ConnectionTypeChanged;

        event EventHandler TrafficChanged;
        string ConnectionType { get; set; }
        void CheckConnectionType();
        long GetReceivedBytes();
        long GetTransmittedBytes();
    }
}
