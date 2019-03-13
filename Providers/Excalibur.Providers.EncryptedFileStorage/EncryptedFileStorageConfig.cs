using System;
using System.Text;
using System.Threading.Tasks;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using Plugin.DeviceInfo;

namespace Excalibur.Providers.EncryptedFileStorage
{
    /// <inheritdoc cref="IEncryptedProviderConfig"/>
    public class EncryptedFileStorageConfig : FileStorageConfig, IEncryptedProviderConfig
    {
        private readonly IExCrypto _crypto;
        private readonly IProtectedStore _protectedStore;
        private const string Test = "ExcaliburTestString";
        private const string TestIdentifier = "ex.ps.test";
        private string _password;

        /// <inheritdoc />
        public string ProtectedStoreKeyIdentifier { get; set; } = "ex.ps.key";

        /// <inheritdoc />
        public string ProtectedStoreSaltIdentifier { get; set; } = "ex.ps.salt";

        /// <inheritdoc />
        public string ProtectedStoreDeviceSaltIdentifier { get; set; } = "ex.ps.device.salt";

        /// <inheritdoc />
        public bool HasBeenInitialized { get; private set; }

        public EncryptedFileStorageConfig(IExCrypto crypto, IProtectedStore protectedStore)
        {
            _crypto = crypto;
            _protectedStore = protectedStore;
        }

        /// <inheritdoc />
        public async Task InitializeFirstTimeAndGenerate(string password, string protectedStoreFileName = "Excalibur.Store")
        {
            // Store the password
            _password = password;
            try
            {
                _protectedStore.Initialize(DeviceKey(), protectedStoreFileName);
            }
            catch (ProtectedStoreException)
            {
                _protectedStore.Remove();
                _protectedStore.Terminate();
                _protectedStore.Initialize(DeviceKey(), protectedStoreFileName);
            }

            // We generate an encryption key for the protected store key
            var keySalt = _crypto.GenerateRandom(32);
            var key = _crypto.CreateDerivedKey(_password, keySalt);

            // Generate some salt we use for encrypting / decrypting just the combinationKey
            var combinationKeySalt = _crypto.GenerateRandom(32);
            await _protectedStore.Save(ProtectedStoreDeviceSaltIdentifier, Convert.ToBase64String(combinationKeySalt)).ConfigureAwait(false);

            // We encrypt the key with the above information and store it for later use
            var keyEncrypted = _crypto.EncryptFromBytes(key, DeviceKey(), combinationKeySalt);
            await _protectedStore.Save(ProtectedStoreKeyIdentifier, Convert.ToBase64String(keyEncrypted)).ConfigureAwait(false);

            // We need some salt for storage encryption
            var storeSalt = _crypto.GenerateRandom(32);
            await _protectedStore.Save(ProtectedStoreSaltIdentifier, Convert.ToBase64String(storeSalt)).ConfigureAwait(false);

            await EncryptAndStoreTest().ConfigureAwait(false);

            HasBeenInitialized = true;
        }

        private async Task EncryptAndStoreTest()
        {
            var testEncrypted = _crypto.Encrypt(Test, await Key().ConfigureAwait(false), await Salt().ConfigureAwait(false));
            await _protectedStore.Save(TestIdentifier, Convert.ToBase64String(testEncrypted)).ConfigureAwait(false);
        }

        private async Task<bool> DecryptAndStoreTest()
        {
            var encryptedTest = await _protectedStore.GetStringForIdentifier(TestIdentifier).ConfigureAwait(false);
            var decryptedTest = _crypto.Decrypt(Convert.FromBase64String(encryptedTest), await Key().ConfigureAwait(false), await Salt().ConfigureAwait(false));
            return decryptedTest.Equals(Test);
        }

        /// <inheritdoc />
        public async Task<bool> InitializeAndTryDecrypt(string password, string protectedStoreFileName = "Excalibur.Store")
        {
            try
            {
                // Store the password
                _password = password;
                _protectedStore.Initialize(DeviceKey(), protectedStoreFileName);
                await Key().ConfigureAwait(false);
                if (await DecryptAndStoreTest().ConfigureAwait(false))
                {
                    HasBeenInitialized = true;

                    return true;
                }
            }
            catch (Exception)
            {
                // just clear to make sure
                // todo introduce password attempts
                Clear();
            }
            return false;
        }

        /// <inheritdoc />
        public string DeviceKey()
        {
            // Derive key for the password and deviceId combination
            var combinationKey = _crypto.CreateDerivedKey(_password, Encoding.UTF8.GetBytes(CrossDevice.Device.DeviceId));
            return Convert.ToBase64String(combinationKey);
        }

        private async Task<byte[]> DeviceSalt() => Convert.FromBase64String(await _protectedStore.GetStringForIdentifier(ProtectedStoreDeviceSaltIdentifier).ConfigureAwait(false));

        /// <inheritdoc />
        public async Task<string> Key()
        {
            var encryptedKey = await _protectedStore.GetStringForIdentifier(ProtectedStoreKeyIdentifier).ConfigureAwait(false);
            var decryptedKey = _crypto.DecryptToBytes(Convert.FromBase64String(encryptedKey), DeviceKey(), await DeviceSalt().ConfigureAwait(false));
            return Convert.ToBase64String(decryptedKey);
        }

        /// <inheritdoc />
        public async Task<byte[]> Salt() => Convert.FromBase64String(await _protectedStore.GetStringForIdentifier(ProtectedStoreSaltIdentifier).ConfigureAwait(false));

        /// <inheritdoc />
        public void Clear()
        {
            _password = null;
            _protectedStore.Terminate();
            HasBeenInitialized = false;
        }

        /// <inheritdoc />
        public async Task Reset()
        {
            try
            {
                await _protectedStore.Delete(TestIdentifier).ConfigureAwait(false);
                await _protectedStore.Delete(ProtectedStoreKeyIdentifier).ConfigureAwait(false);
                await _protectedStore.Delete(ProtectedStoreSaltIdentifier).ConfigureAwait(false);
                await _protectedStore.Delete(ProtectedStoreDeviceSaltIdentifier).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignore
            }

            _protectedStore.Remove();
            Clear();
        }
    }
}