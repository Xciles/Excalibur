using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Presentation
{
    public class BaseListPresentationInt<TDomain, TObservable> : BaseListPresentation<int, TDomain, TObservable, TObservable>, IListPresentationInt<TObservable>
        where TDomain : StorageDomainInt
        where TObservable : ObservableBaseInt, new()
    {
    }
    
    public class BaseListPresentationInt<TDomain, TObservable, TSelectedObservable> : BaseListPresentation<int, TDomain, TObservable, TSelectedObservable>, IListPresentationInt<TObservable, TSelectedObservable>
        where TDomain : StorageDomainInt
        where TObservable : ObservableBaseInt, new()
        where TSelectedObservable : ObservableBaseInt, new()
    {
    }
}