namespace Excalibur.Shared.Storage
{
    public interface IObjectStorageProviderOfInt<T> : IObjectStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}