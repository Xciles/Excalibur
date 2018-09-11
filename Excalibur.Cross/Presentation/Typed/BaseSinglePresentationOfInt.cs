using Excalibur.Cross.Observable.Typed;
using Excalibur.Cross.Storage.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation.Typed
{
    ///  <inheritdoc />
    public class BaseSinglePresentationOfInt<TDomain, TObservable> : BaseSinglePresentation<int, TDomain, TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
    }
}