using System;
using System.Text;
using System.Threading.Tasks;
using Excalibur.Base.Configuration;
using Excalibur.Base.Storage;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.Providers.FileStorage;
using Newtonsoft.Json;

namespace Excalibur.Providers.EncryptedFileStorage
{
    /// <inheritdoc cref="ConfigurationManager"/>
    public class EncryptedConfigurationManager : ConfigurationManager, IConfigurationManager
    {
        private readonly IStorageService _storageService;
        private readonly IEncryptedProviderConfig _config;
        private readonly IProtectedStore _protectedStore;

        /// <summary>
        /// Initializes a ConfigurationManager using the <see cref="IStorageService"/> as storage provider
        /// </summary>
        public EncryptedConfigurationManager(IStorageService storageService, IEncryptedProviderConfig config, IProtectedStore protectedStore) : base(storageService)
        {
            _storageService = storageService;
            _config = config;
            _protectedStore = protectedStore;
        }

        /// <inheritdoc />
        public override async Task<TConfigObject> LoadAsync<TConfigObject>()
        {
            var result = new TConfigObject();

            var configAsString = await _storageService.ReadAsTextAsync("", $"{typeof(TConfigObject).Name}.json").ConfigureAwait(false);
            if (!String.IsNullOrWhiteSpace(configAsString))
            {
                result = JsonConvert.DeserializeObject<TConfigObject>(configAsString);
            }

            return result;
        }

        /// <inheritdoc />
        public override async Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject)
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