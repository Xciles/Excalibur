using Excalibur.Shared.Storage;

namespace Excalibur.Providers.File
{
    public class ObjectAsFileStorageProviderInt<T> : ObjectAsFileStorageProvider<int, T>
        where T : StorageDomainInt
    {
    }
}