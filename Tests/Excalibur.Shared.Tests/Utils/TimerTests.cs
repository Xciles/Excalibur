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
        public void SingleImediateTriggerTest()
        {
            var mre = new ManualResetEvent(false);

            var wasHit = false;
            var timer = new Excalibur.Shared.Utils.Timer(state =>
            {
                wasHit = true;
                mre.Set();
            }, null, -1, -1);

            Assert.IsNotNull(timer);
            mre.WaitOne();

            Assert.IsTrue(wasHit);
        }
    }
}
