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
    public partial class ConnectionInfo : ContentPage
    {
        public ConnectionInfo()
        {
            InitializeComponent();
            ChangeEvenRowCollor(TelephonyGrid, Color.LightBlue);
            ChangeEvenRowCollor(WifiGrid, Color.LightBlue);
        }

        private void ChangeEvenRowCollor(Grid grid, Color color)
        {
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
                if (i % 2 == 0)
                    for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                        grid.Children.ElementAt(i * grid.ColumnDefinitions.Count + j).BackgroundColor = color;
        }
    }
}