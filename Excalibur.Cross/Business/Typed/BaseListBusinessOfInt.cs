using System.Collections.Generic;
using Excalibur.Cross.Services;
using Excalibur.Cross.Storage.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Business.Typed
{
    /// <inheritdoc />
    public class BaseListBusinessOfInt<TDomain> : BaseListBusinessOfInt<TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : StorageDomainOfInt, new()
    {
    }

    /// <inheritdoc cref="BaseListBusiness{TId, TDomain, TService}" />
    public class BaseListBusinessOfInt<TDomain, TService> : BaseListBusiness<int, TDomain, TService>, IListBusinessOfInt<TDomain>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
    }
}