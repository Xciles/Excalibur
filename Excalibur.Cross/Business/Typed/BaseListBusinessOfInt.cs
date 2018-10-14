using System.Collections.Generic;
using Excalibur.Base.Providers;
using Excalibur.Base.Storage;
using Excalibur.Cross.Services;
using Excalibur.Cross.Storage;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Business.Typed
{
    /// <inheritdoc />
    public class BaseListBusinessOfInt<TDomain> : BaseListBusinessOfInt<TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : ProviderDomainOfInt, new()
    {
        public BaseListBusinessOfInt(IServiceBase<IList<TDomain>> service, IDatabaseProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <inheritdoc cref="BaseListBusiness{TId, TDomain, TService}" />
    public class BaseListBusinessOfInt<TDomain, TService> : BaseListBusiness<int, TDomain, TService>, IListBusinessOfInt<TDomain>
        where TDomain : ProviderDomainOfInt, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
        public BaseListBusinessOfInt(TService service, IDatabaseProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }
}