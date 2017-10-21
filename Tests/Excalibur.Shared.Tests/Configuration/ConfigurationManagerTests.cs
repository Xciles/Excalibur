using System.Threading.Tasks;
using Excalibur.Shared.Configuration;
using Excalibur.Shared.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using XLabs.Ioc;

namespace Excalibur.Shared.Tests.Configuration
{
    [TestClass]
    public class ConfigurationManagerTests
    {
        public class ConfigTest
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private Mock<IStorageService> _mockedStorageService;


        [TestInitialize]
        public void Initialize()
        {
            var container = new SimpleContainer();

            _mockedStorageService = new Mock<IStorageService>();
            container.Register<IStorageService>(_mockedStorageService.Object);

            Resolver.SetResolver(container.GetResolver());
        }

        [TestCleanup]
        public void Cleanup()
        {
            Resolver.ResetResolver();
        }

        [TestMethod]
        public void CreationTest()
        {
            var manager = new ConfigurationManager();
            Assert.IsNotNull(manager);
        }

        [TestMethod]
        public async Task LoadConfigurationAsync()
        {
            var config = new ConfigTest()
            {
                Description = "Test",
                Name = "Test"
            };

            _mockedStorageService.Setup(x => x.ReadAsTextAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(() => Task.FromResult(JsonConvert.SerializeObject(config)));

            var manager = new ConfigurationManager();
            var loadedConfig = await manager.LoadAsync<ConfigTest>();

            Assert.IsNotNull(loadedConfig);
            Assert.AreEqual(config.Description, loadedConfig.Description);
            Assert.AreEqual(config.Name, loadedConfig.Name);

            _mockedStorageService.Verify(x => x.ReadAsTextAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public async Task LoadNullConfigurationAsync()
        {
            var manager = new ConfigurationManager();
            var loadedConfig = await manager.LoadAsync<ConfigTest>();

            Assert.IsNotNull(loadedConfig);
            Assert.IsTrue(string.IsNullOrEmpty(loadedConfig.Description));
            Assert.IsTrue(string.IsNullOrEmpty(loadedConfig.Name));

            _mockedStorageService.Verify(x => x.ReadAsTextAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        [ExpectedException((typeof(JsonReaderException)))]
        public async Task LoadIncorrectJsonConfigurationAsync()
        {
            _mockedStorageService.Setup(x => x.ReadAsTextAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult("{[[]}"));

            var manager = new ConfigurationManager();
            var loadedConfig = await manager.LoadAsync<ConfigTest>();
        }

        [TestMethod]
        public async Task LoadEmptyJsonConfigurationAsync()
        {
            _mockedStorageService.Setup(x => x.ReadAsTextAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult("{}"));

            var manager = new ConfigurationManager();
            var loadedConfig = await manager.LoadAsync<ConfigTest>();

            Assert.IsNotNull(loadedConfig);
            Assert.IsTrue(string.IsNullOrEmpty(loadedConfig.Description));
            Assert.IsTrue(string.IsNullOrEmpty(loadedConfig.Name));

            _mockedStorageService.Verify(x => x.ReadAsTextAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public async Task SaveNotExistsConfigurationAsync()
        {
            var config = new ConfigTest()
            {
                Description = "Test",
                Name = "Test"
            };

            _mockedStorageService.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => false);
            _mockedStorageService.Setup(x => x.StoreAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult("NoPath"));

            var manager = new ConfigurationManager();
            var result = await manager.SaveAsync(config);

            Assert.AreEqual(result, true);

            _mockedStorageService.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<string>()));
            _mockedStorageService.Verify(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockedStorageService.Verify(x => x.StoreAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public async Task SaveExistsConfigurationAsync()
        {
            var config = new ConfigTest()
            {
                Description = "Test",
                Name = "Test"
            };

            _mockedStorageService.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => true);
            _mockedStorageService.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
            _mockedStorageService.Setup(x => x.StoreAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult("NoPath"));

            var manager = new ConfigurationManager();
            var result = await manager.SaveAsync(config);

            Assert.AreEqual(result, true);

            _mockedStorageService.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<string>()));
            _mockedStorageService.Verify(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
            _mockedStorageService.Verify(x => x.StoreAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
