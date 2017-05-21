using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Tests.Cross.Core.Domain;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class UserDetailViewModel : DetailViewModel<int, Observable.User, Observable.User>
    {
        public UserDetailViewModel()
        {
            Resolver.Resolve<IListBusiness<int, MyTestDomain>>().UpdateFromServiceAsync();
        }
    }
}
