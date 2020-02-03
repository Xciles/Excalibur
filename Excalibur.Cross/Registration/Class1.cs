using Excalibur.Cross.Observable;
using Excalibur.Cross.Providers;
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

    }
}
