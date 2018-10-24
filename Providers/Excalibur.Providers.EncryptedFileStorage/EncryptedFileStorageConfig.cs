using Excalibur.Providers.FileStorage;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public class EncryptedFileStorageConfig : FileStorageConfig, IEncryptedProviderConfig
    {
        public string ProtectedStoreKeyIdentifier { get; set; } = "ex.ps.key";
        public string ProtectedStoreSaltIdentifier { get; set; } = "ex.ps.salt";
    }
}