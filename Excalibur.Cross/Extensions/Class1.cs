using System.Collections.Generic;
using Excalibur.Base.Observable;
using Excalibur.Base.Providers;
using Excalibur.Base.Registration;
using Excalibur.Cross.Business;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.Services;
using MvvmCross.IoC;

namespace Excalibur.Cross.Extensions
{
    public static class ExcaliburListConfigExtensions
    {

    }
    public static class ExcaliburSingleConfigExtensions
    {

    }

    public static class IoCProviderExtensions
    {
        ///// <summary>
        ///// Registers a <see cref="BaseObjectMapper{TDomain, TObservable}"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/> and 
        ///// Registers a <see cref="BaseObjectMapper{TObservable, TObservable}"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> and 
        ///// Registers a <see cref="LiteDbProvider{TKey, TDomain}"/> as implementation for <see cref="IDatabaseProvider{TKey, TDomain}"/> and
        ///// </summary>
        ///// <typeparam name="TKey"></typeparam>
        ///// <typeparam name="TDomain"></typeparam>
        ///// <typeparam name="TObservable"></typeparam>
        //public static void RegisterDefaultMappersAndDatabaseProvider<TKey, TDomain, TObservable>(this IMvxIoCProvider ioCProvider)
        //    where TDomain : ProviderDomain<TKey>
        //    where TObservable : new()
        //{
        //    ioCProvider.RegisterDefaultMappersFor<TDomain, TObservable>();
        //    ioCProvider.RegisterDefaultDatabaseProvider<TKey, TDomain>();
        //}

        ///// <summary>
        ///// Registers a <see cref="LiteDbProvider{TKey, TDomain}"/> as implementation for <see cref="IDatabaseProvider{TKey, TDomain}"/>
        ///// </summary>
        ///// <typeparam name="TKey">The type of the unique identifier of the domain entity</typeparam>
        ///// <typeparam name="TDomain">The domain entity to be saved</typeparam>
        //public static void RegisterDefaultDatabaseProvider<TKey, TDomain>(this IMvxIoCProvider ioCProvider)
        //    where TDomain : ProviderDomain<TKey>
        //{
        //    ioCProvider.RegisterType<IDatabaseProvider<TKey, TDomain>, LiteDbProvider<TKey, TDomain>>();
        //}

        /// <summary>
        /// Registers a <see cref="BaseObjectMapper{TDomain, TObservable}"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/> and 
        /// Registers a <see cref="BaseObjectMapper{TObservable, TObservable}"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> and 
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <typeparam name="TObservable"></typeparam>
        public static void RegisterDefaultMappersFor<TDomain, TObservable>(this IMvxIoCProvider ioCProvider)
            where TObservable : new()
        {
            ioCProvider.RegisterType<IObjectMapper<TDomain, TObservable>, BaseObjectMapper<TDomain, TObservable>>();
            ioCProvider.RegisterType<IObjectMapper<TObservable, TObservable>, BaseObjectMapper<TObservable, TObservable>>();
        }

        /// <summary>
        /// Registers a <see cref="BaseObjectMapper{TObservable, TObservable}"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> and 
        /// </summary>
        /// <typeparam name="TObservable"></typeparam>
        public static void RegisterObservableMapperFor<TObservable>(this IMvxIoCProvider ioCProvider)
            where TObservable : new()
        {
            ioCProvider.RegisterType<IObjectMapper<TObservable, TObservable>, BaseObjectMapper<TObservable, TObservable>>();
        }

        /// <summary>
        /// Registers a <see cref="BaseObjectMapper{TDomain, TObservable}"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/>
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        public static void RegisterDomainMapperFor<TDomain, TObservable>(this IMvxIoCProvider ioCProvider)
            where TObservable : new()
        {
            ioCProvider.RegisterType<IObjectMapper<TDomain, TObservable>, BaseObjectMapper<TDomain, TObservable>>();
        }

        ///// <summary>
        ///// Register the chain of Entity, Service, Business and Presentation that Excalibur requires
        ///// </summary>
        ///// <param name="ioCProvider"></param>
        //public static ExcaliburIoCConfig RegisterExcaliburChain(this IMvxIoCProvider ioCProvider)
        //{
        //    return new ExcaliburIoCConfig(ioCProvider);
        //}

        /// <summary>
        /// Register the chain of Entity, Service, Business and Presentation that Excalibur requires for a single entity
        /// </summary>
        /// <param name="ioCProvider"></param>
        public static ExcaliburSingleConfig<TKey, TDomain, TObservable> RegisterExcaliburSingleEntity<TKey, TDomain, TObservable>(this IMvxIoCProvider ioCProvider)
            where TDomain : ProviderDomain<TKey>, new()
            where TObservable : ObservableBase<TKey>, new()
        {
            return new ExcaliburSingleConfig<TKey, TDomain, TObservable>(ioCProvider);
        }

        /// <summary>
        /// Register the chain of Entity, Service, Business and Presentation that Excalibur requires for a list entity
        /// </summary>
        /// <param name="ioCProvider"></param>
        public static ExcaliburListConfig<TKey, TDomain, TObservable> RegisterExcaliburListEntity<TKey, TDomain, TObservable>(this IMvxIoCProvider ioCProvider)
            where TDomain : ProviderDomain<TKey>, new()
            where TObservable : ObservableBase<TKey>, new()
        {
            return new ExcaliburListConfig<TKey, TDomain, TObservable>(ioCProvider);
        }
    }

    //public class ExcaliburListConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>
    //       where TDomain : ProviderDomain<TKey>, new()
    //       where TObservable : ObservableBase<TKey>, new()
    //{
    //    public ExcaliburListConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }

    //    /// <summary>
    //    /// Registers <typeparamref name="TMapper"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/> to map from domain to observable and
    //    /// Registers a default <see cref="BaseObjectMapper{TSource,TDestination}"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> to map from observable to observable
    //    /// </summary>
    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithDomainMapper<TMapper>()
    //        where TMapper : BaseObjectMapper<TDomain, TObservable>, IObjectMapper<TDomain, TObservable>
    //    {
    //        IoCProvider.RegisterType<IObjectMapper<TDomain, TObservable>, TMapper>();
    //        IoCProvider.RegisterObservableMapperFor<TObservable>();
    //        return this;
    //    }

    //    /// <summary>
    //    /// Registers <typeparamref name="TDomainMapper"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/> to map from domain to observable
    //    /// Registers <typeparamref name="TObservableMapper"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> to map from observable to observable
    //    /// </summary>
    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithMappers<TDomainMapper, TObservableMapper>()
    //        where TDomainMapper : BaseObjectMapper<TDomain, TObservable>, IObjectMapper<TDomain, TObservable>
    //        where TObservableMapper : BaseObjectMapper<TObservable, TObservable>, IObjectMapper<TObservable, TObservable>
    //    {
    //        IoCProvider.RegisterType<IObjectMapper<TDomain, TObservable>, TDomainMapper>();
    //        IoCProvider.RegisterType<IObjectMapper<TObservable, TObservable>, TObservableMapper>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithDefaultMappers()
    //    {
    //        IoCProvider.RegisterDefaultMappersFor<TDomain, TObservable>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithService<TInterface, TService>()
    //        where TInterface : class, IServiceBase<IList<TDomain>>
    //        where TService : class, TInterface
    //    {
    //        IoCProvider.RegisterType<TInterface, TService>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithDefaultService<TService>()
    //        where TService : class, IServiceBase<IList<TDomain>>
    //    {
    //        IoCProvider.RegisterType<IServiceBase<IList<TDomain>>, TService>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
    //        where TInterface : class, IListBusiness<TKey, TDomain>
    //        where TBusiness : BaseListBusiness<TKey, TDomain>, TInterface
    //    {
    //        IoCProvider.RegisterType<TInterface, TBusiness>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithDefaultBusiness()
    //    {
    //        IoCProvider.RegisterType<IListBusiness<TKey, TDomain>, BaseListBusiness<TKey, TDomain>>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithPresentation<TInterface, TPresentation>()
    //        where TInterface : class, IListPresentation<TKey, TObservable>
    //        where TPresentation : BaseListPresentation<TKey, TDomain, TObservable>, TInterface
    //    {
    //        IoCProvider.ConstructAndRegisterSingleton<TInterface, TPresentation>();
    //        return this;
    //    }

    //    public ExcaliburListConfig<TKey, TDomain, TObservable> WithDefaultPresentation()
    //    {
    //        IoCProvider.ConstructAndRegisterSingleton<IListPresentation<TKey, TObservable>, BaseListPresentation<TKey, TDomain, TObservable>>();
    //        return this;
    //    }
    //}
    //public class ExcaliburSingleConfig<TKey, TDomain, TObservable> : BaseExcaliburIoCConfig<TKey, TDomain, TObservable>
    //    where TDomain : ProviderDomain<TKey>, new()
    //    where TObservable : ObservableBase<TKey>, new()
    //{
    //    public ExcaliburSingleConfig(IMvxIoCProvider ioCProvider) : base(ioCProvider) { }

    //    /// <summary>
    //    /// Registers <typeparamref name="TMapper"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/> to map from domain to observable and
    //    /// Registers a default <see cref="BaseObjectMapper{TObservable, TObservable}"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> to map from observable to observable
    //    /// </summary>
    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDomainMapper<TMapper>()
    //        where TMapper : BaseObjectMapper<TDomain, TObservable>, IObjectMapper<TDomain, TObservable>
    //    {
    //        IoCProvider.RegisterType<IObjectMapper<TDomain, TObservable>, TMapper>();
    //        IoCProvider.RegisterObservableMapperFor<TObservable>();
    //        return this;
    //    }

    //    /// <summary>
    //    /// Registers <typeparamref name="TDomainMapper"/> as implementation for <see cref="IObjectMapper{TDomain, TObservable}"/> to map from domain to observable
    //    /// Registers <typeparamref name="TObservableMapper"/> as implementation for <see cref="IObjectMapper{TObservable, TObservable}"/> to map from observable to observable
    //    /// </summary>
    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithMappers<TDomainMapper, TObservableMapper>()
    //        where TDomainMapper : BaseObjectMapper<TDomain, TObservable>, IObjectMapper<TDomain, TObservable>
    //        where TObservableMapper : BaseObjectMapper<TObservable, TObservable>, IObjectMapper<TObservable, TObservable>
    //    {
    //        IoCProvider.RegisterType<IObjectMapper<TDomain, TObservable>, TDomainMapper>();
    //        IoCProvider.RegisterType<IObjectMapper<TObservable, TObservable>, TObservableMapper>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultMappers()
    //    {
    //        IoCProvider.RegisterDefaultMappersFor<TDomain, TObservable>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithService<TInterface, TService>()
    //        where TInterface : class, IServiceBase<TDomain>
    //        where TService : class, IServiceBase<TDomain>, TInterface
    //    {
    //        IoCProvider.RegisterType<TInterface, TService>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultService<TService>()
    //        where TService : class, IServiceBase<TDomain>
    //    {
    //        IoCProvider.RegisterType<IServiceBase<TDomain>, TService>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
    //        where TInterface : class, ISingleBusiness<TDomain>
    //        where TBusiness : BaseSingleBusiness<TKey, TDomain>, TInterface
    //    {
    //        IoCProvider.RegisterType<TInterface, TBusiness>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultBusiness()
    //    {
    //        IoCProvider.RegisterType<ISingleBusiness<TDomain>, BaseSingleBusiness<TKey, TDomain>>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithPresentation<TInterface, TPresentation>()
    //        where TInterface : class, ISinglePresentation<TKey, TObservable>
    //        where TPresentation : BaseSinglePresentation<TKey, TDomain, TObservable>, TInterface
    //    {
    //        IoCProvider.ConstructAndRegisterSingleton<TInterface, TPresentation>();
    //        return this;
    //    }

    //    public ExcaliburSingleConfig<TKey, TDomain, TObservable> WithDefaultPresentation()
    //    {
    //        IoCProvider.ConstructAndRegisterSingleton<ISinglePresentation<TKey, TObservable>, BaseSinglePresentation<TKey, TDomain, TObservable>>();
    //        return this;
    //    }
    //}
}
