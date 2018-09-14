using Excalibur.Cross.Services;
using Excalibur.Cross.Storage;
using Excalibur.Cross.Storage.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Business.Typed
{
    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain> : BaseSingleBusinessOfInt<TDomain, IServiceBase<TDomain>>
        where TDomain : StorageDomainOfInt, new()
    {
        protected BaseSingleBusinessOfInt(IServiceBase<TDomain> service, IObjectStorageProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain, TService> : BaseSingleBusiness<int, TDomain, TService>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<TDomain>
    {
        protected BaseSingleBusinessOfInt(TService service, IObjectStorageProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }
}