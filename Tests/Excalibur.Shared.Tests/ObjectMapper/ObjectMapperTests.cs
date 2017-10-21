using Excalibur.Shared.ObjectConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Excalibur.Shared.Tests.ObjectMapper
{
    [TestClass]
    public class ObjectMapperTests
    {
        #region InnerClasses

        private class ObjectA
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
        private class ObjectB
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private class ObjectObjectMapper : BaseObjectMapper<ObjectA, ObjectB>
        {
            public override ObjectB Map(ObjectA source)
            {
                var destination = base.Map(source);

                destination.Name = source.Name;
                destination.Description = source.Description;

                return destination;
            }

            public override void UpdateDestination(ObjectA source, ObjectB destination)
            {
                destination.Name = source.Name;
                destination.Description = source.Description;
            }

            public override void UpdateSource(ObjectB destination, ObjectA source)
            {
                source.Name = destination.Name;
                source.Description = destination.Description;
            }
        }

        #endregion

        [ClassInitialize]
        public static void ClassSetup(TestContext a)
        {
            
            ObjectMapperRegistrar.RegisterBinding<IObjectMapper<ObjectA, ObjectB>>(() => new ObjectObjectMapper());
        }

        [TestMethod]
        public void TestObjectMap()
        {
            var objectA = new ObjectA()
            {
                Name = "Object",
                Description = "Object description"
            };

            IObjectMapper<ObjectA, ObjectB> mapper = new ObjectObjectMapper();
            var objectB = mapper.Map(objectA);

            Assert.AreEqual(objectA.Name, objectB.Name);
            Assert.AreEqual(objectA.Description, objectB.Description);
        }

        [TestMethod]
        public void TestObjectUpdateDestination()
        {
            var objectA = new ObjectA()
            {
                Name = "ObjectA",
                Description = "ObjectA description"
            };

            var objectB = new ObjectB()
            {
                Name = "ObjectB",
                Description = "ObjectB description"
            };

            IObjectMapper<ObjectA, ObjectB> mapper = new ObjectObjectMapper();
            mapper.UpdateDestination(objectA, objectB);

            Assert.AreEqual(objectA.Name, objectB.Name);
            Assert.AreEqual(objectA.Name, "ObjectA");
            Assert.AreEqual(objectA.Description, objectB.Description);
            Assert.AreEqual(objectA.Description, "ObjectA description");
        }

        [TestMethod]
        public void TestObjectUpdateSource()
        {
            var objectA = new ObjectA()
            {
                Name = "ObjectA",
                Description = "ObjectA description"
            };

            var objectB = new ObjectB()
            {
                Name = "ObjectB",
                Description = "ObjectB description"
            };

            IObjectMapper<ObjectA, ObjectB> mapper = new ObjectObjectMapper();
            mapper.UpdateSource(objectB, objectA);

            Assert.AreEqual(objectA.Name, objectB.Name);
            Assert.AreEqual(objectA.Name, "ObjectB");
            Assert.AreEqual(objectA.Description, objectB.Description);
            Assert.AreEqual(objectA.Description, "ObjectB description");
        }
    }
}
