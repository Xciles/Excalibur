using System.Threading.Tasks;

namespace Excalibur.Cross.Storage
{
    /// <summary>
    /// Base implementation for the <see cref="IStorageService"/> used for storing files. 
    /// </summary>
    public abstract class StorageServiceBase : IStorageService
    {
        /// <inheritdoc />
        public abstract Task<string> Store(string folder, string fullName, string contentAsString);

        /// <inheritdoc />
        public abstract Task<string> Store(string folder, string fullName, byte[] contentAsBytes);

        /// <inheritdoc />
        public abstract Task<string> ReadAsText(string folder, string fullName);

        /// <inheritdoc />
        public abstract Task<byte[]> ReadAsBinary(string folder, string fullName);

        /// <inheritdoc />
        public abstract void DeleteFile(string folder, string fullName);

        /// <inheritdoc />
        public abstract bool Exists(string folder, string fullName);
    }
}
