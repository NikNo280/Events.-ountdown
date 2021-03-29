using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTimer
{
    public class TimerEventArgs : EventArgs
    {
        public TimerEventArgs(string timerName, int ticks)
        {
            this.TimerName = timerName;
            this.Ticks = ticks;
        }

        public string TimerName { get; set; }

        public int Ticks { get; set; }
    }
}
