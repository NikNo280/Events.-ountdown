using System;
using CustomTimer.Factories;
using NUnit.Framework;

#pragma warning disable CA1707

namespace CustomTimer.Tests
{
    public class CountDownNotifierTests
    {
        private CountDownNotifierFactory countDownNotifierFactory;
        private TimerFactory timerFactory;

        [SetUp]
        public void Setup()
        {
            this.countDownNotifierFactory = new CountDownNotifierFactory();
            this.timerFactory = new TimerFactory();
        }

        [TestCase("pie", 10)]
        [TestCase("cookies", 5)]
        [TestCase("pizza", 1)]
        public void Run_ValidTimer_AllEventsWorkAsExpected(string name, int totalTicks)
        {
            var timer = this.timerFactory.CreateTimer(name, totalTicks);
            var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

            void TimerStarted(object sender, TimerEventArgs e)
            {
                Assert.AreEqual(name, e.TimerName);
                Assert.AreEqual(totalTicks, e.Ticks);
                Console.WriteLine($"Start timer '{e.TimerName}', total {e.Ticks} ticks");
            }

            void TimerStopped(object sender, TimerEventArgs e)
            {
                Assert.AreEqual(name, e.TimerName);
                Console.WriteLine($"Stop timer '{e.TimerName}'");
            }

            var remainsTicks = totalTicks;

            void TimerTick(object sender, TimerEventArgs e)
            {
                remainsTicks -= 1;
                Assert.AreEqual(name, e.TimerName);
                Assert.AreEqual(remainsTicks, e.Ticks);
                Console.WriteLine($"Timer '{e.TimerName}', remains {e.Ticks} ticks");
            }

            notifier.Init(TimerStarted, TimerStopped, TimerTick);
            notifier.Run();

            Assert.AreEqual(0, remainsTicks);
        }


        [TestCase("pie", 10)]
        [TestCase("cookies", 5)]
        [TestCase("pizza", 1)]
        public void Run_NullDelegates_TimerIsWorking(string name, int totalTicks)
        {
            var timer = this.timerFactory.CreateTimer(name, totalTicks);
            var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

            Assert.DoesNotThrow(() =>
            {
                notifier.Init(null, null, null);
                notifier.Run();
            });
        }

        [Test]
        public void Ctor_TimerIsNull_ThrowsArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => this.countDownNotifierFactory.CreateNotifierForTimer(null));
    }
}
