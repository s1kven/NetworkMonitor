using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetworkMonitor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetMonitor : ContentPage
    {
        public NetMonitor()
        {
            InitializeComponent();
        }
    }
}