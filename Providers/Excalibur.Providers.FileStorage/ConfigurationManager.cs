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
        public virtual async Task<TConfigObject> Load<TConfigObject>() where TConfigObject : new()
        {
            var result = new TConfigObject();
            if (HasConfigurationFor<TConfigObject>())
            {
                var configAsString = await _storageService.ReadAsText("", $"{typeof(TConfigObject).Name}.json").ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(configAsString))
                {
                    result = JsonConvert.DeserializeObject<TConfigObject>(configAsString);
                }
            }

            return result;
        }

        /// <inheritdoc />
        public virtual async Task<bool> Save<TConfigObject>(TConfigObject configObject) where TConfigObject : new()
        {
            var configAsString = JsonConvert.SerializeObject(configObject);
            var configName = typeof(TConfigObject).Name;

            if (_storageService.Exists("", $"{configName}.json"))
            {
                _storageService.DeleteFile("", $"{configName}.json");
            }

            await _storageService.Store("", $"{configName}.json", configAsString).ConfigureAwait(false);

            return true;
        }

        /// <inheritdoc />
        public void Reset<TConfigObject>() where TConfigObject : new()
        {
            var configName = typeof(TConfigObject).Name;
            _storageService.DeleteFile("", $"{configName}.json");
        }

        /// <inheritdoc />
        public bool HasConfigurationFor<TConfigObject>() where TConfigObject : new()
        {
            var configName = typeof(TConfigObject).Name;
            return _storageService.Exists("", $"{configName}.json");
        }
    }
}