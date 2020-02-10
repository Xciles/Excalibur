using Excalibur.Cross.Business;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Providers;

namespace Excalibur.Cross.Registration
{
    public interface IBusinessConfig<TKey, TDomain, TObservable>
        where TDomain : ProviderDomain<TKey>, new()
        where TObservable : ObservableBase<TKey>, new()
    {
        IPresentationConfig<TKey, TDomain, TObservable> WithBusiness<TInterface, TBusiness>()
            where TInterface : class, ISingleBusiness<TDomain>
            where TBusiness : BaseSingleBusiness<TKey, TDomain>, TInterface;
        IPresentationConfig<TKey, TDomain, TObservable> WithDefaultBusiness();
    }
}