using System;
using System.Text;
using System.Threading.Tasks;
using Excalibur.Base.Configuration;
using Excalibur.Base.State;
using Excalibur.Base.Storage;
using Excalibur.Providers.FileStorage;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Plugin.File;
using Newtonsoft.Json;
using PCLCrypto;

namespace Excalibur.Providers.EncryptedFileStorage
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IExCrypto, ExCrypto>();
            Mvx.IoCProvider.RegisterType<IEncryptedConfigurationManager, EncryptedConfigurationManager>();
            Mvx.IoCProvider.RegisterType<IConfigurationManager, EncryptedConfigurationManager>();
            Mvx.IoCProvider.RegisterType<IExEncryptedStorageService, ExEncryptedStorageService>();
            Mvx.IoCProvider.RegisterType<IStorageService, ExEncryptedStorageService>();
            Mvx.IoCProvider.RegisterType<IEncryptedConfigurationManager, EncryptedConfigurationManager>();
            Mvx.IoCProvider.RegisterType<IConfigurationManager, EncryptedConfigurationManager>();
        }
    }

    public interface IEncryptedConfigurationManager : IConfigurationManager
    {
        Task<TConfigObject> LoadAsync<TConfigObject>(string password, byte[] salt) where TConfigObject : new();
        Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject, string password, byte[] salt) where TConfigObject : new();
    }

    /// <inheritdoc />
    public class EncryptedConfigurationManager : ConfigurationManager, IEncryptedConfigurationManager
    {
        private readonly IExEncryptedStorageService _storageService;

        /// <summary>
        /// Initializes a ConfigurationManager using the <see cref="IStorageService"/> as storage provider
        /// </summary>
        public EncryptedConfigurationManager(IExEncryptedStorageService storageService) : base(storageService)
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

    public interface IEncryptedBaseState : IBaseState
    {
        Task InitAndLoadAsync(string password, byte[] salt);
        Task SaveAsync(string password, byte[] salt);
    }

    /// <summary>
    /// Base class for managing state. 
    /// This class will initialize a <see cref="ConfigurationManager"/> and will implement certain methods from <see cref="IBaseState"/> to contain
    /// a default implementation.
    /// </summary>
    /// <typeparam name="TConfig">A config class that contains the configuration that contains persistable state</typeparam>
    public abstract class EncryptedBaseState<TConfig> : BaseState<TConfig>, IEncryptedBaseState
        where TConfig : new()
    {
        /// <summary>
        /// The configuration manager that manages the Config
        /// </summary>
        protected new IEncryptedConfigurationManager ConfigurationManager { get; set; }

        /// <summary>
        /// Initializes the BaseState.
        /// </summary>
        protected EncryptedBaseState(IEncryptedConfigurationManager configurationManager) : base(configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        /// <summary>
        /// Initialize and load the state
        /// </summary>
        /// <returns>An await-able task</returns>
        public virtual async Task InitAndLoadAsync(string password, byte[] salt)
        {
            Config = await ConfigurationManager.LoadAsync<TConfig>(password, salt).ConfigureAwait(false);

            await Initialize().ConfigureAwait(false);
        }

        /// <summary>
        /// Save the state.
        /// </summary>
        /// <returns>An await-able task</returns>
        public virtual async Task SaveAsync(string password, byte[] salt)
        {
            await ConfigurationManager.SaveAsync(Config, password, salt).ConfigureAwait(false);
        }
    }

    public interface IExEncryptedStorageService : IStorageService
    {
        Task<string> StoreAsync(string folder, string fullName, string contentAsString, string password, byte[] salt);
        Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes, string password, byte[] salt);
        Task<string> ReadAsTextAsync(string folder, string fullName, string password, byte[] salt);
        Task<byte[]> ReadAsBinaryAsync(string folder, string fullName, string password, byte[] salt);
    }

    public class ExEncryptedStorageService : ExStorageService, IExEncryptedStorageService
    {
        private readonly IExCrypto _exCrypto;

        public ExEncryptedStorageService(IMvxFileStore fileStore, IMvxFileStoreAsync fileStoreAsync, IExCrypto exCrypto) : base(fileStore, fileStoreAsync)
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
            var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName);
            return _exCrypto.DecryptAes(encryptedBytes, password, salt);
        }

        public async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName, string password, byte[] salt)
        {
            var encryptedBytes = await base.ReadAsBinaryAsync(folder, fullName);
            return _exCrypto.DecryptAesToBytes(encryptedBytes, password, salt);
        }
    }

    /// <summary>    
    /// Cryptographic helper inspired by
    /// http://www.c-sharpcorner.com/UploadFile/4088a7/using-cryptography-in-portable-xamarin-formswindows-phone/   
    /// </summary>    
    public interface IExCrypto
    {
        /// <summary>    
        /// Creates Salt with given length in bytes.    
        /// </summary>    
        /// <param name="lengthInBytes">No. of bytes</param>    
        /// <returns></returns>    
        byte[] CreateSalt(int lengthInBytes);

        /// <summary>    
        /// Creates a derived key from a combination     
        /// </summary>    
        /// <param name="password"></param>    
        /// <param name="salt"></param>    
        /// <param name="keyLengthInBytes"></param>    
        /// <param name="iterations"></param>    
        /// <returns></returns>    
        byte[] CreateDerivedKey(string password, byte[] salt, int keyLengthInBytes = 32, int iterations = 1000);

        /// <summary>    
        /// Encrypts given data using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Encrypted bytes</returns>    
        byte[] EncryptAes(string data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Encrypts given data using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Encrypted bytes</returns>    
        byte[] EncryptAesFromBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Decrypts given bytes using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <param name="password">Password used for encryption</param>    
        /// <param name="salt">Salt used for encryption</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Decrypted data as string</returns>    
        string DecryptAes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Decrypts given bytes using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <param name="password">Password used for encryption</param>    
        /// <param name="salt">Salt used for encryption</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Decrypted data as byte[]</returns>    
        byte[] DecryptAesToBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);
    }

    /// <inheritdoc cref="IExCrypto"/>
    public class ExCrypto : IExCrypto
    {
        /// <inheritdoc />
        public byte[] CreateSalt(int lengthInBytes = 15) => WinRTCrypto.CryptographicBuffer.GenerateRandom(lengthInBytes);

        /// <inheritdoc />
        public byte[] CreateDerivedKey(string password, byte[] salt, int keyLengthInBytes = 32, int iterations = 5000)
        {
            return NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
        }
 
        /// <inheritdoc />
        public byte[] EncryptAes(string data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            return EncryptAesFromBytes(Encoding.UTF8.GetBytes(data), password, salt, algorithm);
        }

        /// <inheritdoc />
        public byte[] EncryptAesFromBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            byte[] key = CreateDerivedKey(password, salt);

            var aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var symmetricKey = aes.CreateSymmetricKey(key);
            return WinRTCrypto.CryptographicEngine.Encrypt(symmetricKey, data);
        }

        /// <inheritdoc />
        public string DecryptAes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            var bytes = DecryptAesToBytes(data, password, salt, algorithm);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        /// <inheritdoc />
        public byte[] DecryptAesToBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            byte[] key = CreateDerivedKey(password, salt);

            var aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var symmetricKey = aes.CreateSymmetricKey(key);
            return WinRTCrypto.CryptographicEngine.Decrypt(symmetricKey, data);
        }
    }
}