using System.Threading.Tasks;
using Excalibur.Base.Providers;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public interface IEncryptedProviderConfig : IProviderConfig
    {
        string ProtectedStoreKeyIdentifier { get; set; }
        string ProtectedStoreSaltIdentifier { get; set; }
        bool HasBeenInitialized { get; }

        Task InitializeFirstTimeAndGenerate(string password);
        Task<bool> InitializeAndTryDecrypt(string password);

        string DeviceKey();
        Task<string> Key();
        Task<byte[]> Salt();
        void Clear();
        Task Reset();
    }
}