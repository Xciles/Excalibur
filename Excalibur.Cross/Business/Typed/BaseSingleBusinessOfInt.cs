using Excalibur.Base.Providers;
using Excalibur.Cross.Services;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Business.Typed
{
    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain> : BaseSingleBusinessOfInt<TDomain, IServiceBase<TDomain>>
        where TDomain : ProviderDomainOfInt, new()
    {
        public BaseSingleBusinessOfInt(IServiceBase<TDomain> service, IDatabaseProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain, TService> : BaseSingleBusiness<int, TDomain, TService>
        where TDomain : ProviderDomainOfInt, new()
        where TService : class, IServiceBase<TDomain>
    {
        public BaseSingleBusinessOfInt(TService service, IDatabaseProvider<int, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }
}