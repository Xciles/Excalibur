using System.Threading.Tasks;
using Excalibur.Base.Providers;

namespace Excalibur.Providers.EncryptedFileStorage
{
    /// <summary>
    /// Encrypted provider configuration that should be used when you want to use encryption within an application.
    /// This provides you with a way to manage and retrieve various encryption requirements that's needed for making encryption possible
    ///     in a secure way.
    /// </summary>
    public interface IEncryptedProviderConfig : IProviderConfig
    {
        /// <summary>
        /// The Identifier that is used to identify the Key for the encryption
        /// </summary>
        string ProtectedStoreKeyIdentifier { get; set; }

        /// <summary>
        /// The Identifier that is used to identify the Salt for the Key for the encryption
        /// </summary>
        string ProtectedStoreSaltIdentifier { get; set; }

        /// <summary>
        /// The Identifier that is used to identify the Salt for the device for the encryption
        /// </summary>
        string ProtectedStoreDeviceSaltIdentifier { get; set; }

        /// <summary>
        /// Returns if the encrypted configuration has been initialized
        /// </summary>
        bool HasBeenInitialized { get; }

        /// <summary>
        /// This method should be called just the once for the application and will initialize the underlying dependencies.
        /// Password should, usually, be a thing the user provides.
        /// It generates a Key and 2 Salts for the application. The password in combination with the deviceId will be used to encrypt the key 
        ///     that will be used for encryption in the application.
        /// This method also adds a test value to the store, which will be attempted to be decrypted when a user tries to login.
        ///
        /// The underlying storage service will use the <see cref="ProtectedStoreKeyIdentifier"/> and <see cref="ProtectedStoreSaltIdentifier"/>
        ///     to encrypt and decrypt files with.
        /// </summary>
        /// <param name="password">The password that you want to use for deriving keys from</param>
        /// <param name="protectedStoreFileName">The store name that will be used for storing secrets</param>
        Task InitializeFirstTimeAndGenerate(string password, string protectedStoreFileName = "Excalibur.Store");

        /// <summary>
        /// This method should be called every time a user returns to the application to make sure that the dependencies are initialized properly.
        /// It will try to decrypt data, including the test value, that have been created with the <see cref="InitializeFirstTimeAndGenerate"/>.
        /// If it's successful it will return true, if not, it <see cref="Clear"/>s itself and will return false.
        /// </summary>
        /// <param name="password">The password that you want to use for deriving keys from</param>
        /// <param name="protectedStoreFileName">The store name that will be used for storing secrets</param>
        /// <returns></returns>
        Task<bool> InitializeAndTryDecrypt(string password, string protectedStoreFileName = "Excalibur.Store");

        /// <summary>
        /// A derived key fom the user his/her password and the deviceId.
        /// This key is used to encrypt the KeyStore on Android with as well as encrypting the actual encryption key for storage.
        /// </summary>
        /// <returns>The password deviceId derived key</returns>
        string DeviceKey();

        /// <summary>
        /// The actual encryption key that is used by the underlying encrypted storage provider.
        /// </summary>
        /// <returns>The encryption key</returns>
        Task<string> Key();

        /// <summary>
        /// The salt for the encryption key that is used by the underlying encrypted storage provider.
        /// </summary>
        /// <returns>The encryption salt</returns>
        Task<byte[]> Salt();

        /// <summary>
        /// This method clears the current state, terminates the protected store and clears stored information by the configuration.
        /// You should call this method when the application for example unloads, suspends, etc.
        /// </summary>
        void Clear();

        /// <summary>
        /// All save information will be reset to the initial state and will no longer be available.
        /// You should call this method if you want to reset your application state or force remove information for a user.
        /// This will also remove the stores if needed and <see cref="Clear"/> the current state.
        /// </summary>
        Task Reset();
    }
}