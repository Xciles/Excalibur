using System.Collections.Generic;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    public class BaseListBusinessOfInt<TDomain> : BaseListBusinessOfInt<TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : StorageDomainOfInt, new()
    {
    }
    
    public class BaseListBusinessOfInt<TDomain, TService> : BaseListBusiness<int, TDomain, TService>, IListBusinessOfInt<TDomain>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
    }
}