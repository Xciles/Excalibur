using System.Threading.Tasks;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public class EncryptedStorageService : ExStorageService, IEncryptedStorageService
    {
        private readonly IExCrypto _exCrypto;

        public EncryptedStorageService(IMvxFileStore fileStore, IMvxFileStoreAsync fileStoreAsync, IExCrypto exCrypto) : base(fileStore, fileStoreAsync)
        {
            _exCrypto = exCrypto;
        }

        public Task<string> StoreAsync(string folder, string fullName, string contentAsString, string password, byte[] salt)
        {
            var encryptedBytes = _exCrypto.EncryptAes(contentAsString, password, salt);
            return base.StoreAsync(folder, fullName, encryptedBytes);
        }

        public Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes, string password, byte[] salt)
        {
            var encryptedBytes = _exCrypto.EncryptAesFromBytes(contentAsBytes, password, salt);
            return base.StoreAsync(folder, fullName, encryptedBytes);
        }

        public async Task<string> ReadAsTextAsync(string folder, string fullName, string password, byte[] salt)
        {
            var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName).ConfigureAwait(false);
            return _exCrypto.DecryptAes(encryptedBytes, password, salt);
        }

        public async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName, string password, byte[] salt)
        {
            var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName).ConfigureAwait(false);
            return _exCrypto.DecryptAesToBytes(encryptedBytes, password, salt);
        }
    }
}