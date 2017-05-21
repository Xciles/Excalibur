using System;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using Newtonsoft.Json;
using XLabs.Ioc;

namespace Excalibur.Shared.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly string _configName;
        private readonly IStorageService _storageService;

        public ConfigurationManager(string configName)
        {
            _configName = configName;
            _storageService = Resolver.Resolve<IStorageService>();
        }

        public async Task<TConfigObject> LoadAsync<TConfigObject>() where TConfigObject : new()
        {
            var result = new TConfigObject();

            var configAsString = await _storageService.ReadAsTextAsync("", _configName).ConfigureAwait(false);
            if (!String.IsNullOrWhiteSpace(configAsString))
            {
                result = JsonConvert.DeserializeObject<TConfigObject>(configAsString);
            }

            return result;
        }

        public async Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject) where TConfigObject : new()
        {
            var configAsString = JsonConvert.SerializeObject(configObject);

            if (_storageService.Exists("", _configName))
            {
                _storageService.DeleteFile("", _configName);
            }

            await _storageService.StoreAsync("", _configName, configAsString).ConfigureAwait(false);

            return true;
        }
    }
}