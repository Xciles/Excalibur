using System;
using System.Threading.Tasks;
using Excalibur.Base.Configuration;
using Excalibur.Base.Storage;
using Newtonsoft.Json;

namespace Excalibur.Providers.FileStorage
{
    /// <inheritdoc />
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IStorageService _storageService;

        /// <summary>
        /// Initializes a ConfigurationManager using the <see cref="IStorageService"/> as storage provider
        /// </summary>
        public ConfigurationManager(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc />
        public virtual async Task<TConfigObject> LoadAsync<TConfigObject>() where TConfigObject : new()
        {
            var result = new TConfigObject();
            if (HasConfigurationFor<TConfigObject>())
            {
                var configAsString = await _storageService.ReadAsTextAsync("", $"{typeof(TConfigObject).Name}.json").ConfigureAwait(false);
                if (!String.IsNullOrWhiteSpace(configAsString))
                {
                    result = JsonConvert.DeserializeObject<TConfigObject>(configAsString);
                }
            }

            return result;
        }

        /// <inheritdoc />
        public virtual async Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject) where TConfigObject : new()
        {
            var configAsString = JsonConvert.SerializeObject(configObject);
            var configName = typeof(TConfigObject).Name;

            if (_storageService.Exists("", $"{configName}.json"))
            {
                _storageService.DeleteFile("", $"{configName}.json");
            }

            await _storageService.StoreAsync("", $"{configName}.json", configAsString).ConfigureAwait(false);

            return true;
        }

        public bool HasConfigurationFor<TConfigObject>() where TConfigObject : new()
        {
            var configName = typeof(TConfigObject).Name;
            return _storageService.Exists("", $"{configName}.json");
        }
    }
}