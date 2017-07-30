using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Presentation
{
    public class BaseSinglePresentationInt<TDomain, TObservable> : BaseSinglePresentation<int, TDomain, TObservable>
        where TDomain : StorageDomainInt
        where TObservable : ObservableBaseInt, new()
    {
    }
}