using Excalibur.Base.Providers;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation
{
    ///  <inheritdoc />
    public class BaseSinglePresentationOfInt<TDomain, TObservable> : BaseSinglePresentation<int, TDomain, TObservable>
        where TDomain : ProviderDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
        public BaseSinglePresentationOfInt(IObjectMapper<TDomain, TObservable> domainSelectedMapper) : base(domainSelectedMapper)
        {
        }
    }
}