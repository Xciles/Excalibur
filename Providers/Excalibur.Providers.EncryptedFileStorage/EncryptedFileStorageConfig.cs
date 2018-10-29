using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using MvvmCross;
using Plugin.DeviceInfo;

namespace Excalibur.Providers.EncryptedFileStorage
{
    public class EncryptedFileStorageConfig : FileStorageConfig, IEncryptedProviderConfig
    {
        private readonly IExCrypto _crypto;
        private readonly IProtectedStore _protectedStore;
        private const string Test = "ExcaliburTestString";
        private const string TestIdentifier = "ex.ps.test";

        public string ProtectedStoreKeyIdentifier { get; set; } = "ex.ps.key";
        public string ProtectedStoreDeviceSaltIdentifier { get; set; } = "ex.ps.device.salt";
        public string ProtectedStoreSaltIdentifier { get; set; } = "ex.ps.salt";

        public bool HasBeenInitialized { get; private set; }
        private string _password;

        public EncryptedFileStorageConfig(IExCrypto crypto, IProtectedStore protectedStore)
        {
            _crypto = crypto;
            _protectedStore = protectedStore;
        }

        public async Task InitializeFirstTimeAndGenerate(string password)
        {
            // Store the password
            _password = password;

            // We generate an encryption key for the protected store key
            var keySalt = _crypto.GenerateRandom(32);
            var key = _crypto.CreateDerivedKey(_password, keySalt);

            // Generate some salt we use for encrypting / decrypting just the combinationKey
            var combinationKeySalt = _crypto.GenerateRandom(32);
            await _protectedStore.Save(ProtectedStoreDeviceSaltIdentifier, Convert.ToBase64String(combinationKeySalt));

            // We encrypt the key with the above information and store it for later use
            var keyEncrypted = _crypto.EncryptFromBytes(key, DeviceKey(), combinationKeySalt);
            await _protectedStore.Save(ProtectedStoreKeyIdentifier, Convert.ToBase64String(keyEncrypted));

            // We need some salt for storage encryption
            var storeSalt = _crypto.GenerateRandom(32);
            await _protectedStore.Save(ProtectedStoreSaltIdentifier, Convert.ToBase64String(storeSalt));

            await EncryptAndStoreTest();

            HasBeenInitialized = true;
        }

        private async Task EncryptAndStoreTest()
        {
            var testEncrypted = _crypto.Encrypt(Test, await Key(), await Salt());
            await _protectedStore.Save(TestIdentifier, Convert.ToBase64String(testEncrypted));
        }

        private async Task<bool> DecryptAndStoreTest()
        {
            var encryptedTest = await _protectedStore.GetStringForIdentifier(TestIdentifier);
            var decryptedTest = _crypto.Decrypt(Convert.FromBase64String(encryptedTest), await Key(), await Salt());
            return decryptedTest.Equals(Test);
        }

        public async Task<bool> InitializeAndTryDecrypt(string password)
        {
            try
            {
                // Store the password
                _password = password;
                await Key();
                if (await DecryptAndStoreTest())
                {
                    HasBeenInitialized = true;

                    return true;
                }
            }
            catch (Exception)
            {
                // just ignore
                // todo introduce password attempts
            }
            return false;
        }

        public string DeviceKey()
        {
            // Derive key for the password and deviceId combination
            var combinationKey = _crypto.CreateDerivedKey(_password, Encoding.UTF8.GetBytes(CrossDevice.Device.DeviceId));
            return Convert.ToBase64String(combinationKey);
        }

        private async Task<byte[]> DeviceSalt()
        {
            return Convert.FromBase64String(await _protectedStore.GetStringForIdentifier(ProtectedStoreDeviceSaltIdentifier));
        }

        public async Task<string> Key()
        {
            var encryptedKey = await _protectedStore.GetStringForIdentifier(ProtectedStoreKeyIdentifier);
            var decryptedKey = _crypto.DecryptToBytes(Convert.FromBase64String(encryptedKey), DeviceKey(), await DeviceSalt());
            return Convert.ToBase64String(decryptedKey);
        }

        public async Task<byte[]> Salt()
        {
            return Convert.FromBase64String(await _protectedStore.GetStringForIdentifier(ProtectedStoreSaltIdentifier));
        }

        public void Clear()
        {
            _password = null;
            HasBeenInitialized = false;
        }

        public async Task Reset()
        {
            await _protectedStore.Delete(TestIdentifier);
            await _protectedStore.Delete(ProtectedStoreKeyIdentifier);
            await _protectedStore.Delete(ProtectedStoreDeviceSaltIdentifier);
            await _protectedStore.Delete(ProtectedStoreSaltIdentifier);
        }
    }
}