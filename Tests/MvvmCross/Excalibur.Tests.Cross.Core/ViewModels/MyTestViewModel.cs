using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Tests.Cross.Core.Domain;
using Excalibur.Tests.Cross.Core.Observable;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MyTestViewModel : ListViewModel<int, MyTestObservable, MyTestObservable>
    {
        public MyTestViewModel()
        {
            Resolver.Resolve<IListBusiness<int, MyTestDomain>>().UpdateFromServiceAsync();
        }
    }
}
