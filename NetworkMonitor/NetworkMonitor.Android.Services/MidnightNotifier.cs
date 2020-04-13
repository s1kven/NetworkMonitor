using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NetworkMonitor.Services
{
    internal class MidnightNotifier
    {

        public event EventHandler DayChanged;
        private readonly Timer timer;

        internal MidnightNotifier()
        {
            timer = new Timer(GetSleepTime());
            timer.Elapsed += (s, e) =>
            {
                DayChanged?.Invoke(null, null);
                timer.Interval = GetSleepTime();
            };
            timer.Start();
        }

        private static double GetSleepTime()
        {
            var midnightTonight = DateTime.Today.AddDays(1);
            var differenceInMilliseconds = (midnightTonight - DateTime.Now).TotalMilliseconds;
            return differenceInMilliseconds;
        }
    }
}