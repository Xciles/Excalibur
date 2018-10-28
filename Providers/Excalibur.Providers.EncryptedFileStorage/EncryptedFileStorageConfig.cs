using System.Text;
using System.Threading.Tasks;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using MvvmCross;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public class EncryptedFileStorageConfig : FileStorageConfig, IEncryptedProviderConfig
    {
        public string ProtectedStoreKeyIdentifier { get; set; } = "ex.ps.key";
        public string ProtectedStoreSaltIdentifier { get; set; } = "ex.ps.salt";

        public bool HasBeenInitialized { get; private set; }

        public async Task Init(string password)
        {
            var crypto = Mvx.IoCProvider.Resolve<IExCrypto>();
            var keySalt = crypto.GenerateRandom(27);
            var key = crypto.CreateDerivedKey(password, keySalt);

            var store = Mvx.IoCProvider.Resolve<IProtectedStore>();
            await store.Save(ProtectedStoreKeyIdentifier, Encoding.UTF8.GetString(key));
            var storeSalt = crypto.GenerateRandom(32);
            await store.Save(ProtectedStoreSaltIdentifier, Encoding.UTF8.GetString(storeSalt));

            HasBeenInitialized = true;
        }
    }
}