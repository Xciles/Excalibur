using PCLCrypto;

namespace Excalibur.Providers.Encryption
{
    /// <summary>    
    /// Cryptographic helper inspired by
    /// http://www.c-sharpcorner.com/UploadFile/4088a7/using-cryptography-in-portable-xamarin-formswindows-phone/   
    /// </summary>    
    public interface IExCrypto
    {
        /// <summary>    
        /// Creates a random buffer with given length in bytes that contains data.    
        /// </summary>    
        /// <param name="lengthInBytes">No. of bytes</param>    
        /// <returns></returns>    
        byte[] GenerateRandom(int lengthInBytes);

        /// <summary>    
        /// Creates a derived key from a combination of password and salt with a certain length and number of iterations
        /// </summary>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>
        /// <param name="keyLengthInBytes">The key length</param>    
        /// <param name="iterations">Number of iterations</param>    
        /// <returns></returns>    
        byte[] CreateDerivedKey(string password, byte[] salt, int keyLengthInBytes = 32, int iterations = 5000);

        /// <summary>    
        /// Encrypts given string data using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Encrypted bytes</returns>    
        byte[] Encrypt(string data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Encrypts given bytes data using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Encrypted bytes</returns>    
        byte[] EncryptFromBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Decrypts given bytes to string using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <param name="password">Password used for encryption</param>    
        /// <param name="salt">Salt used for encryption</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Decrypted data as string</returns>    
        string Decrypt(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Decrypts given bytes to bytes using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <param name="password">Password used for encryption</param>    
        /// <param name="salt">Salt used for encryption</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Decrypted data as byte[]</returns>    
        byte[] DecryptToBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>
        /// Hash a given string with an algorithm
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="algorithm">The algorithm to use</param>
        /// <returns>Hashed data as byte[]</returns>
        byte[] Hash(string data, HashAlgorithm algorithm = HashAlgorithm.Sha256);

        /// <summary>
        /// Hash a given byte[] with an algorithm
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="algorithm">The algorithm to use</param>
        /// <returns>Hashed data as byte[]</returns>
        byte[] Hash(byte[] data, HashAlgorithm algorithm = HashAlgorithm.Sha256);

        /// <summary>
        /// Hash a given string with an algorithm
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="keyMaterial"></param>
        /// <param name="algorithm">The algorithm to use</param>
        /// <returns>Hashed data as byte[]</returns>
        byte[] HashMac(string data, byte[] keyMaterial, MacAlgorithm algorithm = MacAlgorithm.HmacSha256);

        /// <summary>
        /// Hash a given byte[] with an algorithm
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="keyMaterial"></param>
        /// <param name="algorithm">The algorithm to use</param>
        /// <returns>Hashed data as byte[]</returns>
        byte[] HashMac(byte[] data, byte[] keyMaterial, MacAlgorithm algorithm = MacAlgorithm.HmacSha256);
    }
}