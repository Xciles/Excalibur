//using System.Threading.Tasks;
//using Excalibur.Base.Storage;

//namespace Excalibur.Providers.EncryptedFileStorage
//{
//    /// <summary>
//    /// This interface provides an implementation for storing files encrypted.
//    /// </summary>
//    public interface IEncryptedStorageService : IStorageService
//    {
//        /// <summary>
//        /// Encrypt and store a string on a certain location
//        /// </summary>
//        /// <param name="folder">The name of the folder</param>
//        /// <param name="fullName">The name of the file</param>
//        /// <param name="contentAsString">The content that should be written to file</param>
//        /// <param name="password">Password to use</param>
//        /// <param name="salt">Salt to use</param>
//        /// <returns></returns>
//        Task<string> StoreAsync(string folder, string fullName, string contentAsString, string password, byte[] salt);
//        /// <summary>
//        /// Encrypt and store a byte[] on a certain location
//        /// </summary>
//        /// <param name="folder">The name of the folder</param>
//        /// <param name="fullName">The name of the file</param>
//        /// <param name="contentAsBytes">The content that should be written to file</param>
//        /// <param name="password">Password to use</param>
//        /// <param name="salt">Salt to use</param>
//        /// <returns></returns>
//        Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes, string password, byte[] salt);
//        /// <summary>
//        /// Decrypt and read a file as string from a certain location
//        /// </summary>
//        /// <param name="folder">The name of the folder</param>
//        /// <param name="fullName">The name of the file</param>
//        /// <param name="password">Password to use</param>
//        /// <param name="salt">Salt to use</param>
//        /// <returns>File content as string</returns>
//        Task<string> ReadAsTextAsync(string folder, string fullName, string password, byte[] salt);
//        /// <summary>
//        /// Decrypt and read a file as byte[] from a certain location
//        /// </summary>
//        /// <param name="folder">The name of the folder</param>
//        /// <param name="fullName">The name of the file</param>
//        /// <param name="password">Password to use</param>
//        /// <param name="salt">Salt to use</param>
//        /// <returns>File content as byte[]</returns>
//        Task<byte[]> ReadAsBinaryAsync(string folder, string fullName, string password, byte[] salt);
//    }
//}