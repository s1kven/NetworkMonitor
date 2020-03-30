using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.ViewModels
{
    public interface INetInfo
    {
        delegate void TypeNameHandler();
        event TypeNameHandler TypeName { add { } remove { } }
        string GetTypeName();
        long GetMobileRxBytes();
        long GetMobileTxBytes();
    }
}
