using System;

namespace CustomTimer.Interfaces
{
    /// <summary>
    /// Represents subscriber classes for the Started, Tick, and Stopped events of the CustomTimer class.
    /// </summary>
    public interface ICountDownNotifier
    {
        /// <summary>
        /// Initializes event handlers.
        /// </summary>
        /// <param name="startHandler">Represents a Start event handler. Can be null.</param>
        /// <param name="stopHandler">Represents a Stop event handler. Can be null.</param>
        /// <param name="tickHandler">Represents a Tick event handler. Can be null.</param>
        void Init(EventHandler<TimerEventArgs> startHandler, EventHandler<TimerEventArgs> stopHandler, EventHandler<TimerEventArgs> tickHandler);

        /// <summary>
        /// Starts timer.
        /// </summary>
        void Run();
    }
}
