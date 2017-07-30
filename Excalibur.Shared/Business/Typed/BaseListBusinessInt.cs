using System.Collections.Generic;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    public class BaseListBusinessInt<TDomain> : BaseListBusinessInt<TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : StorageDomainInt, new()
    {
    }
    
    public class BaseListBusinessInt<TDomain, TService> : BaseListBusiness<int, TDomain, TService>, IListBusinessInt<TDomain>
        where TDomain : StorageDomainInt, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
    }
}