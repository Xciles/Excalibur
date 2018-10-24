using System.Text;
using PCLCrypto;

namespace Excalibur.Providers.Encryption
{
    /// <inheritdoc />
    public class ExCrypto : IExCrypto
    {
        /// <inheritdoc />
        public byte[] GenerateRandom(int lengthInBytes = 15) => WinRTCrypto.CryptographicBuffer.GenerateRandom(lengthInBytes);

        /// <inheritdoc />
        public byte[] CreateDerivedKey(string password, byte[] salt, int keyLengthInBytes = 32, int iterations = 5000)
        {
            return NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
        }
 
        /// <inheritdoc />
        public byte[] Encrypt(string data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            return EncryptFromBytes(Encoding.UTF8.GetBytes(data), password, salt, algorithm);
        }

        /// <inheritdoc />
        public byte[] EncryptFromBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            byte[] key = CreateDerivedKey(password, salt);

            var algorithmProvider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var symmetricKey = algorithmProvider.CreateSymmetricKey(key);
            return WinRTCrypto.CryptographicEngine.Encrypt(symmetricKey, data);
        }

        /// <inheritdoc />
        public string Decrypt(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            var bytes = DecryptToBytes(data, password, salt, algorithm);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        /// <inheritdoc />
        public byte[] DecryptToBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7)
        {
            byte[] key = CreateDerivedKey(password, salt);

            var algorithmProvider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var symmetricKey = algorithmProvider.CreateSymmetricKey(key);
            return WinRTCrypto.CryptographicEngine.Decrypt(symmetricKey, data);
        }

        /// <inheritdoc />
        public byte[] Hash(string data, HashAlgorithm algorithm = HashAlgorithm.Sha256)
        {
            return Hash(Encoding.UTF8.GetBytes(data), algorithm);
        }

        /// <inheritdoc />
        public byte[] Hash(byte[] data, HashAlgorithm algorithm = HashAlgorithm.Sha256)
        {
            var algorithmProvider = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(algorithm);
            return algorithmProvider.HashData(data);
        }

        /// <inheritdoc />
        public byte[] HashMac(string data, byte[] keyMaterial, MacAlgorithm algorithm = MacAlgorithm.HmacSha256)
        {
            return HashMac(Encoding.UTF8.GetBytes(data), keyMaterial, algorithm);
        }

        /// <inheritdoc />
        public byte[] HashMac(byte[] data, byte[] keyMaterial, MacAlgorithm algorithm = MacAlgorithm.HmacSha256)
        {
            var algorithmProvider = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(algorithm);
            var hasher = algorithmProvider.CreateHash(keyMaterial);
            hasher.Append(data);
            return hasher.GetValueAndReset();
        }
    }
}