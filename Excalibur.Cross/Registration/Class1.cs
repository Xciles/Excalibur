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
    public abstract class BaseExcaliburIoCConfig
    {
        public IMvxIoCProvider IoCProvider;

        protected BaseExcaliburIoCConfig(IMvxIoCProvider ioCProvider)
        {
            IoCProvider = ioCProvider;
        }
    }

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

    //public class ExcaliburIoCConfig : BaseExcaliburIoCConfig
    //{
    //    public ExcaliburIoCConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> ForSingleEntity<TKey, TDomain, TObservable>()
    //        where TDomain : ProviderDomain<TKey>, new()
    //        where TObservable : ObservableBase<TKey>, new()
    //    {
    //        return new ExcaliburSingleConfig<TKey, TDomain, TObservable>(IoCProvider);
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> ForListEntity<TKey, TDomain, TObservable>()
    //        where TDomain : ProviderDomain<TKey>, new()
    //        where TObservable : ObservableBase<TKey>, new()
    //    {
    //        return new ExcaliburListConfig<TKey, TDomain, TObservable>(IoCProvider);
    //    }
    //}

    public class ExcaliburListConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        public ExcaliburListConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }
    }

    public class ExcaliburSingleConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        public ExcaliburSingleConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefault()
        {
            WithDefaultBusiness();
            WithDefaultPresentation();
            WithDefaultMappers();

            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultMappers()
        {
            RegisterMapper();

            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithMapper(Action<MapperOptions<TKey, TDomain, TObservable>> options)
        {
            RegisterMapper(options);

            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultService<TService>()
            where TService : class, IServiceBase<TDomain>
        {
            IoCProvider.RegisterType<IServiceBase<TDomain>, TService>();

            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithService<TInterface, TService>()
            where TInterface : class, IServiceBase<TDomain>
            where TService : class, IServiceBase<TDomain>, TInterface
        {
            IoCProvider.RegisterType<TInterface, TService>();
            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
            where TInterface : class, ISingleBusiness<TDomain>
            where TBusiness : BaseSingleBusiness<TKey, TDomain>, TInterface
        {
            IoCProvider.RegisterType<TInterface, TBusiness>();
            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultBusiness()
        {
            IoCProvider.RegisterType<ISingleBusiness<TDomain>, BaseSingleBusiness<TKey, TDomain>>();
            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithPresentation<TInterface, TPresentation>()
            where TInterface : class, ISinglePresentation<TKey, TObservable>
            where TPresentation : BaseSinglePresentation<TKey, TDomain, TObservable>, TInterface
        {
            IoCProvider.ConstructAndRegisterSingleton<TInterface, TPresentation>();
            return this;
        }

        public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultPresentation()
        {
            IoCProvider.ConstructAndRegisterSingleton<ISinglePresentation<TKey, TObservable>, BaseSinglePresentation<TKey, TDomain, TObservable>>();
            return this;
        }
    }
}
