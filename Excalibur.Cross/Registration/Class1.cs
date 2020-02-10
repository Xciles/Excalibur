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

    public interface IFirst<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        IService<TKey, TDomain> WithDefault();
        IBusiness<TKey, TDomain, TObservable> WithDefaultMappers();
        IBusiness<TKey, TDomain, TObservable> WithMapper(Action<MapperOptions<TKey, TDomain, TObservable>> options);
    }

    public interface IService<TKey, TDomain>
        where TDomain : ProviderDomain<TKey>, new()
    {
        void WithDefaultService<TService>()
            where TService : class, IServiceBase<TDomain>;

        void WithService<TInterface, TService>()
            where TInterface : class, IServiceBase<TDomain>
            where TService : class, IServiceBase<TDomain>, TInterface;
    }

    public interface IBusiness<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        IPresentation<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
            where TInterface : class, ISingleBusiness<TDomain>
            where TBusiness : BaseSingleBusiness<TKey, TDomain>, TInterface;
        IPresentation<TKey, TDomain, TObservable> WithDefaultBusiness();
    }


    public interface IPresentation<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        IService<TKey, TDomain> WithPresentation<TInterface, TPresentation>()
            where TInterface : class, ISinglePresentation<TKey, TObservable>
            where TPresentation : BaseSinglePresentation<TKey, TDomain, TObservable>, TInterface;
        IService<TKey, TDomain> WithDefaultPresentation();
    }

    public class ExcaliburSingleConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>, IFirst<TKey, TDomain, TObservable>, IBusiness<TKey, TDomain, TObservable>, IPresentation<TKey, TDomain, TObservable>, IService<TKey, TDomain>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        public ExcaliburSingleConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }

        public IService<TKey, TDomain> WithDefault()
        {
            WithDefaultMappers();
            WithDefaultBusiness();
            WithDefaultPresentation();

            return this;
        }

        // todo introduce Interfaces for ordering

        public IBusiness<TKey, TDomain, TObservable> WithDefaultMappers()
        {
            RegisterMapper();

            return this;
        }

        public IBusiness<TKey, TDomain, TObservable> WithMapper(Action<MapperOptions<TKey, TDomain, TObservable>> options)
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

        public IPresentation<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
            where TInterface : class, ISingleBusiness<TDomain>
            where TBusiness : BaseSingleBusiness<TKey, TDomain>, TInterface
        {
            IoCProvider.RegisterType<TInterface, TBusiness>();
            return this;
        }

        public IPresentation<TKey, TDomain, TObservable> WithDefaultBusiness()
        {
            IoCProvider.RegisterType<ISingleBusiness<TDomain>, BaseSingleBusiness<TKey, TDomain>>();
            return this;
        }

        public IService<TKey, TDomain> WithPresentation<TInterface, TPresentation>()
            where TInterface : class, ISinglePresentation<TKey, TObservable>
            where TPresentation : BaseSinglePresentation<TKey, TDomain, TObservable>, TInterface
        {
            IoCProvider.ConstructAndRegisterSingleton<TInterface, TPresentation>();
            return this;
        }

        public IService<TKey, TDomain> WithDefaultPresentation()
        {
            IoCProvider.ConstructAndRegisterSingleton<ISinglePresentation<TKey, TObservable>, BaseSinglePresentation<TKey, TDomain, TObservable>>();
            return this;
        }
    }
}
