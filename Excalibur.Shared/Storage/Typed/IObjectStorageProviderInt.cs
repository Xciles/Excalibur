namespace Excalibur.Shared.Storage
{
    public interface IObjectStorageProviderInt<T> : IObjectStorageProvider<int, T>
        where T : StorageDomainInt
    {
    }
}