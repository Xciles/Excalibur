using System;
using Excalibur.Cross.Extensions;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Providers;

namespace Excalibur.Cross.Registration
{
    public interface IExcaliburConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        IServiceConfig<TKey, TDomain> WithDefault();
        IBusinessConfig<TKey, TDomain, TObservable> WithDefaultMappers();
        IBusinessConfig<TKey, TDomain, TObservable> WithMapper(Action<MapperOptions<TKey, TDomain, TObservable>> options);
    }
}