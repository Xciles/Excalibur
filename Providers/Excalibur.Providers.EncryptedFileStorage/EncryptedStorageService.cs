using System.Text;
using System.Threading.Tasks;
using Excalibur.Base.Storage;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public class EncryptedStorageService : ExStorageService, IStorageService
    {
        private readonly IEncryptedProviderConfig _config;
        private readonly IProtectedStore _protectedStore;
        private readonly IExCrypto _exCrypto;

        public EncryptedStorageService(IMvxFileStore fileStore, IEncryptedProviderConfig config, IProtectedStore protectedStore, IMvxFileStoreAsync fileStoreAsync, IExCrypto exCrypto) : base(fileStore, fileStoreAsync)
        {
            _config = config;
            _protectedStore = protectedStore;
            _exCrypto = exCrypto;
        }

        public override Task<string> StoreAsync(string folder, string fullName, string contentAsString)
        {
            var encryptedBytes = _exCrypto.Encrypt(contentAsString, _protectedStore.GetStringForIdentifier(_config.ProtectedStoreKeyIdentifier), Encoding.UTF8.GetBytes(_protectedStore.GetStringForIdentifier(_config.ProtectedStoreSaltIdentifier)));
            return base.StoreAsync(folder, fullName, encryptedBytes);
        }

        public override Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes)
        {
            var encryptedBytes = _exCrypto.EncryptFromBytes(contentAsBytes, _protectedStore.GetStringForIdentifier(_config.ProtectedStoreKeyIdentifier), Encoding.UTF8.GetBytes(_protectedStore.GetStringForIdentifier(_config.ProtectedStoreSaltIdentifier)));
            return base.StoreAsync(folder, fullName, encryptedBytes);
        }

        public override async Task<string> ReadAsTextAsync(string folder, string fullName)
        {
            var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName).ConfigureAwait(false);
            return _exCrypto.Decrypt(encryptedBytes, _protectedStore.GetStringForIdentifier(_config.ProtectedStoreKeyIdentifier), Encoding.UTF8.GetBytes(_protectedStore.GetStringForIdentifier(_config.ProtectedStoreSaltIdentifier)));
        }

        public override async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName)
        {
            var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName).ConfigureAwait(false);
            return _exCrypto.DecryptToBytes(encryptedBytes, _protectedStore.GetStringForIdentifier(_config.ProtectedStoreKeyIdentifier), Encoding.UTF8.GetBytes(_protectedStore.GetStringForIdentifier(_config.ProtectedStoreSaltIdentifier)));
        }
    }
}