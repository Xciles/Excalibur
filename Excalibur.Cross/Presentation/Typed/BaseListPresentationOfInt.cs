using Excalibur.Cross.Business;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable.Typed;
using Excalibur.Cross.Storage.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation.Typed
{
    /// <inheritdoc cref="BaseListPresentationOfInt{TDomain,TObservable,TObservable}"/>
    public class BaseListPresentationOfInt<TDomain, TObservable> : BaseListPresentationOfInt<TDomain, TObservable, TObservable>, IListPresentationOfInt<TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
        public BaseListPresentationOfInt(
            IObjectMapper<TDomain, TObservable> domainMapper, 
            IObjectMapper<TObservable, TObservable> observableSelectedMapper, 
            IListBusiness<int, TDomain> listBusiness, 
            IExMainThreadDispatcher dispatcher) 
            : base(domainMapper, domainMapper, observableSelectedMapper, listBusiness, dispatcher)
        {
        }
    }

    /// <inheritdoc cref="BaseListPresentation{TId,TDomain,TObservable,TSelectedObservable}"/>
    public class BaseListPresentationOfInt<TDomain, TObservable, TSelectedObservable> : BaseListPresentation<int, TDomain, TObservable, TSelectedObservable>, IListPresentationOfInt<TObservable, TSelectedObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
        public BaseListPresentationOfInt(
            IObjectMapper<TDomain, TSelectedObservable> domainSelectedMapper, 
            IObjectMapper<TDomain, TObservable> domainObservableMapper, 
            IObjectMapper<TObservable, TSelectedObservable> observableSelectedMapper, 
            IListBusiness<int, TDomain> listBusiness, 
            IExMainThreadDispatcher dispatcher) 
            : base(domainSelectedMapper, domainObservableMapper, observableSelectedMapper, listBusiness, dispatcher)
        {
        }
    }
}