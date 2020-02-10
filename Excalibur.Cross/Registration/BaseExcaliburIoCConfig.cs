using System;
using Excalibur.Cross.Extensions;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Providers;
using MvvmCross.IoC;

namespace Excalibur.Cross.Registration
{
    public abstract class BaseExcaliburIoCConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        public IMvxIoCProvider IoCProvider;

        protected BaseExcaliburIoCConfig(IMvxIoCProvider ioCProvider)
        {
            IoCProvider = ioCProvider;
        }

        protected void RegisterMapper()
        {
            RegisterMapper(options =>
            {
                options.DefaultDomainMapper();
                options.DefaultObservableMapper();
            });
        }

        protected void RegisterMapper(Action<MapperOptions<TKey, TDomain, TObservable>> options)
        {
            var mapperOptions = new MapperOptions<TKey, TDomain, TObservable>(this);

            options(mapperOptions);
        }
    }
}