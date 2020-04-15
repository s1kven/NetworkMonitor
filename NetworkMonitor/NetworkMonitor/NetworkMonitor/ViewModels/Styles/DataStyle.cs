using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.ViewModels.Styles
{
    public static class DataStyle
    {
        public static string TrafficToString(string bytes, byte signsNumber = 2)
        {
            const int KB = 1000;
            double traffic = Convert.ToDouble(bytes);
            if (traffic < Math.Pow(KB, 1))
            {
                return RoundNumber(traffic, signsNumber).ToString() + " B";
            }
            else if (traffic >= Math.Pow(KB, 1) && traffic < Math.Pow(KB, 2))
            {
                traffic = traffic / Math.Pow(KB, 1);
                return RoundNumber(traffic, signsNumber).ToString() + " KB";
            }
            else if (traffic >= Math.Pow(KB, 2) && traffic < Math.Pow(KB, 3))
            {
                traffic = traffic / Math.Pow(KB, 2);
                return RoundNumber(traffic, signsNumber).ToString() + " MB";
            }
            else if (traffic >= Math.Pow(KB, 3) && traffic < Math.Pow(KB, 4))
            {
                traffic = traffic / Math.Pow(KB, 3);
                return RoundNumber(traffic, signsNumber).ToString() + " GB";
            }
            else
            {
                traffic = traffic / Math.Pow(KB, 4);
                return RoundNumber(traffic, signsNumber).ToString() + " TB";
            }
        }
        public static string SpeedToString(string bytes, byte signsNumber = 2)
        {
            const int KB = 1000;
            double traffic = Convert.ToDouble(bytes);
            if (traffic < Math.Pow(KB, 1))
            {
                return RoundNumber(traffic, signsNumber).ToString() + " B/S";
            }
            else if (traffic >= Math.Pow(KB, 1) && traffic < Math.Pow(KB, 2))
            {
                traffic = traffic / Math.Pow(KB, 1);
                return RoundNumber(traffic, signsNumber).ToString() + " KB/S";
            }
            else if (traffic >= Math.Pow(KB, 2) && traffic < Math.Pow(KB, 3))
            {
                traffic = traffic / Math.Pow(KB, 2);
                return RoundNumber(traffic, signsNumber).ToString() + " MB/S";
            }
            else if (traffic >= Math.Pow(KB, 3) && traffic < Math.Pow(KB, 4))
            {
                traffic = traffic / Math.Pow(KB, 3);
                return RoundNumber(traffic, signsNumber).ToString() + " GB/S";
            }
            else
            {
                traffic = traffic / Math.Pow(KB, 4);
                return RoundNumber(traffic, signsNumber).ToString() + " TB/S";
            }
        }
        public static double RoundNumber(double number, byte signsNumber = 2)
        {
            return Math.Round(number, signsNumber);
        }
        public static string RoundNumber(string number, byte signsNumber = 2)
        {
            double num = Convert.ToDouble(number);
            string signs = "N" + signsNumber.ToString();
            return num.ToString(signs);
        }
    }
}
