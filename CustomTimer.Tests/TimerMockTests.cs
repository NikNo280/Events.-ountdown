using System;
using CustomTimer.Implementation;
using Moq;
using NUnit.Framework;

namespace CustomTimer.Tests
{
    public class TimerMockTests
    {
        [TestCase("alarm", 1)]
        [TestCase("alarm", 15)]
        public void EventMethodsCallCheck(string timerName, int ticks)
        {
            var timer = new Timer(timerName, ticks);
            var notifierMock = new Mock<CountDownNotifier>(timer);
            var notifier = notifierMock.Object;

            var onStartMock = new Mock<EventHandler<TimerEventArgs>>();
            var onStopMock = new Mock<EventHandler<TimerEventArgs>>();
            var onTickMock = new Mock<EventHandler<TimerEventArgs>>();

            notifier.Init(onStartMock.Object, onStopMock.Object, onTickMock.Object);
            notifier.Run();

            onStartMock.Verify(n => n(It.IsAny<object>(), It.IsAny<TimerEventArgs>()), Times.Once);
            onStopMock.Verify(n => n(It.IsAny<object>(), It.IsAny<TimerEventArgs>()), Times.Once);
            onTickMock.Verify(n => n(It.IsAny<object>(), It.IsAny<TimerEventArgs>()), Times.Exactly(ticks));
        }
    }
}
