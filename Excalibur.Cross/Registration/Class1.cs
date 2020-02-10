using System;
using Excalibur.Cross.Business;
using Excalibur.Cross.Extensions;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.Providers;
using Excalibur.Cross.Services;
using MvvmCross.IoC;

namespace Excalibur.Cross.Registration
{
    public class ExcaliburListConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        public ExcaliburListConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }
    }

    public class ExcaliburSingleConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>, IExcaliburConfig<TKey, TDomain, TObservable>, IBusinessConfig<TKey, TDomain, TObservable>, IPresentationConfig<TKey, TDomain, TObservable>, IServiceConfig<TKey, TDomain>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        public ExcaliburSingleConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }

        public IServiceConfig<TKey, TDomain> WithDefault()
        {
            WithDefaultMappers();
            WithDefaultBusiness();
            WithDefaultPresentation();

            return this;
        }

        public IBusinessConfig<TKey, TDomain, TObservable> WithDefaultMappers()
        {
            RegisterMapper();

            return this;
        }

        public IBusinessConfig<TKey, TDomain, TObservable> WithMapper(Action<MapperOptions<TKey, TDomain, TObservable>> options)
        {
            RegisterMapper(options);

            return this;
        }

        public void WithDefaultService<TService>()
            where TService : class, IServiceBase<TDomain>
        {
            IoCProvider.RegisterType<IServiceBase<TDomain>, TService>();
        }

        public void WithService<TInterface, TService>()
            where TInterface : class, IServiceBase<TDomain>
            where TService : class, IServiceBase<TDomain>, TInterface
        {
            IoCProvider.RegisterType<TInterface, TService>();
        }

        public IPresentationConfig<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
            where TInterface : class, ISingleBusiness<TDomain>
            where TBusiness : BaseSingleBusiness<TKey, TDomain>, TInterface
        {
            IoCProvider.RegisterType<TInterface, TBusiness>();
            return this;
        }

        public IPresentationConfig<TKey, TDomain, TObservable> WithDefaultBusiness()
        {
            IoCProvider.RegisterType<ISingleBusiness<TDomain>, BaseSingleBusiness<TKey, TDomain>>();
            return this;
        }

        public IServiceConfig<TKey, TDomain> WithPresentation<TInterface, TPresentation>()
            where TInterface : class, ISinglePresentation<TKey, TObservable>
            where TPresentation : BaseSinglePresentation<TKey, TDomain, TObservable>, TInterface
        {
            IoCProvider.ConstructAndRegisterSingleton<TInterface, TPresentation>();
            return this;
        }

        public IServiceConfig<TKey, TDomain> WithDefaultPresentation()
        {
            IoCProvider.ConstructAndRegisterSingleton<ISinglePresentation<TKey, TObservable>, BaseSinglePresentation<TKey, TDomain, TObservable>>();
            return this;
        }
    }
}
