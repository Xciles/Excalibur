using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Excalibur.Shared.Tests.Utils
{
    [TestClass]
    public class TimerTests
    {
        [TestMethod]
        public void SingleTriggerTest()
        {
            var mre = new ManualResetEvent(false);

            var wasHit = false;
            var timer = new Excalibur.Shared.Utils.Timer(state =>
            {
                wasHit = true;
                mre.Set();
            }, null, 1, -1);

            Assert.IsNotNull(timer);
            mre.WaitOne();

            Assert.IsTrue(wasHit);
        }

        [TestMethod]
        public void SingleAfterTriggerTest()
        {
            var mre = new ManualResetEvent(false);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var wasHit = false;
            var timer = new Excalibur.Shared.Utils.Timer(state =>
            {
                wasHit = true;
                stopwatch.Stop();
                mre.Set();
            }, null, 1000, -1);

            Assert.IsNotNull(timer);
            mre.WaitOne();

            Assert.IsTrue(stopwatch.Elapsed.Seconds == 1);

            Assert.IsTrue(wasHit);
        }

        [TestMethod]
        public void MultipleAfterTriggerTest()
        {
            var mre = new ManualResetEvent(false);
            var numberOfHits = 0;
            var timer = new Excalibur.Shared.Utils.Timer(state =>
            {
                numberOfHits++;
                if (numberOfHits == 4)
                {
                    mre.Set();
                }
            }, null, 500, 100);

            Assert.IsNotNull(timer);
            mre.WaitOne();

            Assert.IsTrue(numberOfHits == 4);
        }

        [TestMethod]
        [ExpectedException((typeof(ArgumentOutOfRangeException)))]
        public void FailDelayTriggerTest()
        {
            var timer = new Excalibur.Shared.Utils.Timer(state =>
            {
                // We do nothing
            }, null, -2, -1);
        }
    }
}
