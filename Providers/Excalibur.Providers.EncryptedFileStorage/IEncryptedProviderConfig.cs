using Excalibur.Base.Providers;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public interface IEncryptedProviderConfig : IProviderConfig
    {
        string ProtectedStoreKeyIdentifier { get; set; }
        string ProtectedStoreSaltIdentifier { get; set; }
    }
}