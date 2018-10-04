using Excalibur.Base.Storage.Typed;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation.Typed
{
    ///  <inheritdoc />
    public class BaseSinglePresentationOfInt<TDomain, TObservable> : BaseSinglePresentation<int, TDomain, TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
        public BaseSinglePresentationOfInt(IObjectMapper<TDomain, TObservable> domainSelectedMapper) : base(domainSelectedMapper)
        {
        }
    }
}