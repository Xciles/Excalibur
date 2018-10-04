using System.Collections.Generic;
using Excalibur.Base.Storage;
using Excalibur.Base.Storage.Typed;
using Excalibur.Cross.Services;
using Excalibur.Cross.Storage;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Business.Typed
{
    /// <inheritdoc />
    public class BaseListBusinessOfInt<TDomain> : BaseListBusinessOfInt<TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : StorageDomainOfInt, new()
    {
        protected BaseListBusinessOfInt(IServiceBase<IList<TDomain>> service, IObjectStorageProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <inheritdoc cref="BaseListBusiness{TId, TDomain, TService}" />
    public class BaseListBusinessOfInt<TDomain, TService> : BaseListBusiness<int, TDomain, TService>, IListBusinessOfInt<TDomain>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
        protected BaseListBusinessOfInt(TService service, IObjectStorageProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }
}