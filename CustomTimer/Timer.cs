using System;
using System.Threading;

namespace CustomTimer
{
    /// <summary>
    /// A custom class for simulating a countdown clock, which implements the ability to send a messages and additional
    /// information about the Started, Tick and Stopped events to any types that are subscribing the specified events.
    /// 
    /// - When creating a CustomTimer object, it must be assigned:
    ///     - name (not null or empty string, otherwise ArgumentException will be thrown);
    ///     - the number of ticks (the number must be greater than 0 otherwise an exception will throw an ArgumentException).
    /// 
    /// - After the timer has been created, it should fire the Started event, the event should contain information about
    /// the name of the timer and the number of ticks to start.
    /// 
    /// - After starting the timer, it fires Tick events, which contain information about the name of the timer and
    /// the number of ticks left for triggering, there should be delays between Tick events, delays are modeled by Thread.Sleep.
    /// 
    /// - After all Tick events are triggered, the timer should start the Stopped event, the event should contain information about
    /// the name of the timer.
    /// </summary>
    public class Timer
    {
        private readonly string timerName;
        private readonly int ticks;

        public Timer(string timerName, int ticks)
        {
            if (string.IsNullOrEmpty(timerName))
            {
                throw new ArgumentException($"{nameof(timerName)} is null or empty");
            }

            if (ticks <= 0)
            {
                throw new ArgumentException($"{nameof(ticks)} has to be greater than 0");
            }

            this.timerName = timerName;
            this.ticks = ticks;
        }

        public event EventHandler<TimerEventArgs> Started;

        public event EventHandler<TimerEventArgs> Tick;

        public event EventHandler<TimerEventArgs> Stopped;

        public void Start()
        {
            this.Started?.Invoke(this, new TimerEventArgs(this.timerName, this.ticks));
            this.Ticks();
        }

        public void Ticks()
        {
            for (int i = this.ticks - 1; i >= 0; i--)
            {
                this.Tick?.Invoke(this, new TimerEventArgs(this.timerName, i));
                Thread.Sleep(1000);
            }

            this.End();
        }

        public void End()
        {
            this.Stopped?.Invoke(this, new TimerEventArgs(this.timerName, this.ticks));
        }
    }
}
