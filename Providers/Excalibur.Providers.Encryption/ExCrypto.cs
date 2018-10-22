using System.Text;
using PCLCrypto;

namespace Excalibur.Providers.Encryption
{
    /// <inheritdoc />
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