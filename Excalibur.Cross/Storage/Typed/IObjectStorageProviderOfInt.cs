// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Storage.Typed
{
    /// <inheritdoc />
    public interface IObjectStorageProviderOfInt<T> : IObjectStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}