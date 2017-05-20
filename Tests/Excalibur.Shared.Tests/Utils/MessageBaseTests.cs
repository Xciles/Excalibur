using Excalibur.Shared.Business;
using Excalibur.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Excalibur.Shared.Tests.Utils
{
    [TestClass]
    public class MessageBaseTests
    {
        private class MesObject
        {
            public string Name { get; set; }
        }

        [TestMethod]
        public void MessageBaseObjectTest()
        {
            var obj = new MesObject
            {
                Name = "Test"
            };

            var messageBase = new MessageBase<MesObject>(obj);

            Assert.AreSame(messageBase.Object, obj);
            Assert.AreEqual(messageBase.State, EDomainState.Updated);
        }

        [TestMethod]
        public void MessageBaseObjectCreatedTest()
        {
            var obj = new MesObject
            {
                Name = "Test"
            };

            var messageBase = new MessageBase<MesObject>(obj, EDomainState.Created);

            Assert.AreSame(messageBase.Object, obj);
            Assert.AreEqual(messageBase.State, EDomainState.Created);
        }

        [TestMethod]
        public void MessageBaseObjectUpdatedTest()
        {
            var obj = new MesObject
            {
                Name = "Test"
            };

            var messageBase = new MessageBase<MesObject>(obj, EDomainState.Updated);

            Assert.AreSame(messageBase.Object, obj);
            Assert.AreEqual(messageBase.State, EDomainState.Updated);
        }

        [TestMethod]
        public void MessageBaseObjectDeletedTest()
        {
            var obj = new MesObject
            {
                Name = "Test"
            };

            var messageBase = new MessageBase<MesObject>(obj, EDomainState.Deleted);

            Assert.AreSame(messageBase.Object, obj);
            Assert.AreEqual(messageBase.State, EDomainState.Deleted);
        }

        [TestMethod]
        public void MessageBaseStringTest()
        {
            var test = "TestString";
            var messageBase = new MessageBase<string>(test);

            Assert.AreEqual(messageBase.Object, test);
            Assert.AreEqual(messageBase.State, EDomainState.Updated);
        }
    }
}
