using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    public class BaseSingleBusinessInt<TDomain> : BaseSingleBusinessInt<TDomain, IServiceBase<TDomain>>
        where TDomain : StorageDomainInt, new()
    {
    }
    
    public class BaseSingleBusinessInt<TDomain, TService> : BaseSingleBusiness<int, TDomain, TService>
        where TDomain : StorageDomainInt, new()
        where TService : class, IServiceBase<TDomain>
    {
    }
}