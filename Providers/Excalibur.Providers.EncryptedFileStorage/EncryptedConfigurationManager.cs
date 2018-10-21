using System;
using System.Threading.Tasks;
using Excalibur.Base.Storage;
using Excalibur.Providers.FileStorage;
using Newtonsoft.Json;

namespace Excalibur.Providers.EncryptedFileStorage
{
    /// <inheritdoc cref="IEncryptedConfigurationManager"/>
    public class EncryptedConfigurationManager : ConfigurationManager, IEncryptedConfigurationManager
    {
        private readonly IEncryptedStorageService _storageService;

        /// <summary>
        /// Initializes a ConfigurationManager using the <see cref="IStorageService"/> as storage provider
        /// </summary>
        public EncryptedConfigurationManager(IEncryptedStorageService storageService) : base(storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc />
        public async Task<TConfigObject> LoadAsync<TConfigObject>(string password, byte[] salt) where TConfigObject : new()
        {
            var result = new TConfigObject();

            var configAsString = await _storageService.ReadAsTextAsync("", $"{typeof(TConfigObject).Name}.json", password, salt).ConfigureAwait(false);
            if (!String.IsNullOrWhiteSpace(configAsString))
            {
                result = JsonConvert.DeserializeObject<TConfigObject>(configAsString);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject, string password, byte[] salt) where TConfigObject : new()
        {
            var configAsString = JsonConvert.SerializeObject(configObject);
            var configName = typeof(TConfigObject).Name;

            if (_storageService.Exists("", $"{configName}.json"))
            {
                _storageService.DeleteFile("", $"{configName}.json");
            }

            await _storageService.StoreAsync("", $"{configName}.json", configAsString, password, salt).ConfigureAwait(false);

            return true;
        }
    }
}