using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public interface INetInfo
    {
        //delegate void ConnectionTypeHandler();
        event EventHandler<string> ConnectionTypeChanged;
        string ConnectionType { get; set; }
        string IP { get; }
        void CheckConnectionType();
        long GetReceivedBytes();
        long GetTransmittedBytes();
    }
}
