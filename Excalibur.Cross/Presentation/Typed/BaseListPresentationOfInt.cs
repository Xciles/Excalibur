using Excalibur.Base.Providers;
using Excalibur.Cross.Business;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable.Typed;
using MvvmCross.Base;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation.Typed
{
    /// <inheritdoc cref="BaseListPresentationOfInt{TDomain,TObservable,TObservable}"/>
    public class BaseListPresentationOfInt<TDomain, TObservable> : BaseListPresentationOfInt<TDomain, TObservable, TObservable>, IListPresentationOfInt<TObservable>
        where TDomain : ProviderDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
        public BaseListPresentationOfInt(
            IObjectMapper<TDomain, TObservable> domainMapper, 
            IObjectMapper<TObservable, TObservable> observableSelectedMapper, 
            IListBusiness<int, TDomain> listBusiness,
            IMvxMainThreadAsyncDispatcher dispatcher) 
            : base(domainMapper, domainMapper, observableSelectedMapper, listBusiness, dispatcher)
        {
        }
    }

    /// <inheritdoc cref="BaseListPresentation{TId,TDomain,TObservable,TSelectedObservable}"/>
    public class BaseListPresentationOfInt<TDomain, TObservable, TSelectedObservable> : BaseListPresentation<int, TDomain, TObservable, TSelectedObservable>, IListPresentationOfInt<TObservable, TSelectedObservable>
        where TDomain : ProviderDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
        public BaseListPresentationOfInt(
            IObjectMapper<TDomain, TObservable> domainObservableMapper, 
            IObjectMapper<TDomain, TSelectedObservable> domainSelectedMapper, 
            IObjectMapper<TObservable, TSelectedObservable> observableSelectedMapper, 
            IListBusiness<int, TDomain> listBusiness,
            IMvxMainThreadAsyncDispatcher dispatcher) 
            : base(domainObservableMapper, domainSelectedMapper, observableSelectedMapper, listBusiness, dispatcher)
        {
        }
    }
}