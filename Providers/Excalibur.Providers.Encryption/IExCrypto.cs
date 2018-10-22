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
        /// Creates Salt with given length in bytes.    
        /// </summary>    
        /// <param name="lengthInBytes">No. of bytes</param>    
        /// <returns></returns>    
        byte[] CreateSalt(int lengthInBytes);

        /// <summary>    
        /// Creates a derived key from a combination of password and salt with a certain length and number of iterations
        /// </summary>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>
        /// <param name="keyLengthInBytes">The key length</param>    
        /// <param name="iterations">Number of iterations</param>    
        /// <returns></returns>    
        byte[] CreateDerivedKey(string password, byte[] salt, int keyLengthInBytes = 32, int iterations = 1000);

        /// <summary>    
        /// Encrypts given string data using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Encrypted bytes</returns>    
        byte[] EncryptAes(string data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Encrypts given bytes data using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Encrypted bytes</returns>    
        byte[] EncryptAesFromBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Decrypts given bytes to string using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <param name="password">Password used for encryption</param>    
        /// <param name="salt">Salt used for encryption</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Decrypted data as string</returns>    
        string DecryptAes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);

        /// <summary>    
        /// Decrypts given bytes to bytes using AesCbcPkcs7 as default
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <param name="password">Password used for encryption</param>    
        /// <param name="salt">Salt used for encryption</param>    
        /// <param name="algorithm">The algorithm that should be used</param>
        /// <returns>Decrypted data as byte[]</returns>    
        byte[] DecryptAesToBytes(byte[] data, string password, byte[] salt, SymmetricAlgorithm algorithm = SymmetricAlgorithm.AesCbcPkcs7);
    }
}