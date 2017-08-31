using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Presentation
{
    public class BaseListPresentationOfInt<TDomain, TObservable> : BaseListPresentationOfInt<TDomain, TObservable, TObservable>, IListPresentationOfInt<TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
    }
    
    public class BaseListPresentationOfInt<TDomain, TObservable, TSelectedObservable> : BaseListPresentation<int, TDomain, TObservable, TSelectedObservable>, IListPresentationOfInt<TObservable, TSelectedObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}