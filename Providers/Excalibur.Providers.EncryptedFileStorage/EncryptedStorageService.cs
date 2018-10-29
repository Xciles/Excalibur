using System;
using System.Threading.Tasks;
using Excalibur.Base.Storage;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public class EncryptedStorageService : ExStorageService, IStorageService
    {
        private const string ErrorString = "Did you forget to initialize the IEncryptedProviderConfig?";
        private readonly IEncryptedProviderConfig _config;
        private readonly IExCrypto _exCrypto;

        public EncryptedStorageService(IMvxFileStore fileStore, IEncryptedProviderConfig config, IMvxFileStoreAsync fileStoreAsync, IExCrypto exCrypto) : base(fileStore, fileStoreAsync)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config), ErrorString);
            _exCrypto = exCrypto;
        }

        public override async Task<string> StoreAsync(string folder, string fullName, string contentAsString)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            var encryptedBytes = _exCrypto.Encrypt(contentAsString, await _config.Key(), await _config.Salt());
            return await base.StoreAsync(folder, fullName, encryptedBytes);
        }

        public override async Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            var encryptedBytes = _exCrypto.EncryptFromBytes(contentAsBytes, await _config.Key(), await _config.Salt());
            return await base.StoreAsync(folder, fullName, encryptedBytes);
        }

        public override async Task<string> ReadAsTextAsync(string folder, string fullName)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            if (Exists(folder, fullName))
            {
                var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName).ConfigureAwait(false);
                return _exCrypto.Decrypt(encryptedBytes, await _config.Key(), await _config.Salt());
            }

            return string.Empty;
        }

        public override async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            if (Exists(folder, fullName))
            {
                var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName).ConfigureAwait(false);
                return _exCrypto.DecryptToBytes(encryptedBytes, await _config.Key(), await _config.Salt());
            }
            return new byte[]{};
        }
    }
}