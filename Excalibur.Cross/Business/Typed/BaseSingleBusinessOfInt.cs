using Excalibur.Base.Storage;
using Excalibur.Base.Storage.Typed;
using Excalibur.Cross.Services;
using Excalibur.Cross.Storage;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Business.Typed
{
    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain> : BaseSingleBusinessOfInt<TDomain, IServiceBase<TDomain>>
        where TDomain : StorageDomainOfInt, new()
    {
        public BaseSingleBusinessOfInt(IServiceBase<TDomain> service, IObjectStorageProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain, TService> : BaseSingleBusiness<int, TDomain, TService>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<TDomain>
    {
        public BaseSingleBusinessOfInt(TService service, IObjectStorageProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }
}