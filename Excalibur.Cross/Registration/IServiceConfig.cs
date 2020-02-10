using System.Collections.Generic;
using Excalibur.Cross.Providers;
using Excalibur.Cross.Services;

namespace Excalibur.Cross.Registration
{
    public interface IServiceConfig<TKey, TDomain>
        where TDomain : ProviderDomain<TKey>, new()
    {
        void WithDefaultService<TService>()
            where TService : class, IServiceBase<TDomain>;

        void WithService<TInterface, TService>()
            where TInterface : class, IServiceBase<TDomain>
            where TService : class, IServiceBase<TDomain>, TInterface;
    }
}