using System;
using CustomTimer.Interfaces;

namespace CustomTimer.Implementation
{
    /// <inheritdoc/>
    public class CountDownNotifier : ICountDownNotifier
    {
        private readonly Timer timer;

        public CountDownNotifier(Timer timer)
        {
            this.timer = timer;
        }

        /// <inheritdoc/>
        public void Init(EventHandler<TimerEventArgs> startHandler, EventHandler<TimerEventArgs> stopHandler, EventHandler<TimerEventArgs> tickHandler)
        {
            this.timer.Started += startHandler;
            this.timer.Tick += tickHandler;
            this.timer.Stopped += stopHandler;
        }

        /// <inheritdoc/>
        public void Run()
        {
            this.timer.Start();
        }
    }
}
