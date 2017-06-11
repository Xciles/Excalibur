using System;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using Newtonsoft.Json;
using XLabs.Ioc;

namespace Excalibur.Shared.Configuration
{
    /// <summary>
    /// ConfigurationManager that will manage the Configuration. 
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IStorageService _storageService;

        /// <summary>
        /// Initializes a ConfigurationManager using the <see cref="IStorageService"/> as storage provider
        /// </summary>
        public ConfigurationManager()
        {
            _storageService = Resolver.Resolve<IStorageService>();
        }

        /// <summary>
        /// Loads the configuration using <see cref="TConfigObject"/> as storage entity
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <returns>An await able Task with the configuration as result</returns>
        public async Task<TConfigObject> LoadAsync<TConfigObject>() where TConfigObject : new()
        {
            var result = new TConfigObject();

            var configAsString = await _storageService.ReadAsTextAsync("", $"{typeof(TConfigObject).Name}.json").ConfigureAwait(false);
            if (!String.IsNullOrWhiteSpace(configAsString))
            {
                result = JsonConvert.DeserializeObject<TConfigObject>(configAsString);
            }

            return result;
        }

        /// <summary>
        /// Saves the configuration using the <see cref="IStorageService"/> as storage provider
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <param name="configObject">The object that contains the configuration</param>
        /// <returns>An await able Task with the success as result</returns>
        public async Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject) where TConfigObject : new()
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
    }
}