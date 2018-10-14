using Excalibur.Base.Storage;
using Excalibur.Base.Storage.Typed;

namespace Excalibur.Providers.File
{
    /// <inheritdoc />
    /// <summary>
    /// <see cref="ObjectAsFileStorageProvider{TId,T}"/> but where TId is set to int
    /// </summary>
    public class ObjectAsFileStorageProviderOfInt<T> : ObjectAsFileStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
        public ObjectAsFileStorageProviderOfInt(IStorageService storageService) : base(storageService)
        {
        }
    }
}