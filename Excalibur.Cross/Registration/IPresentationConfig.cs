using Excalibur.Cross.Observable;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.Providers;

namespace Excalibur.Cross.Registration
{
    public interface IPresentationConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        IServiceConfig<TKey, TDomain> WithPresentation<TInterface, TPresentation>()
            where TInterface : class, ISinglePresentation<TKey, TObservable>
            where TPresentation : BaseSinglePresentation<TKey, TDomain, TObservable>, TInterface;
        IServiceConfig<TKey, TDomain> WithDefaultPresentation();
    }
}