using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class UserViewModel : ListViewModel<int, Observable.User, Observable.User, UserDetailViewModel>
    {
        public UserViewModel()
        {
            Resolver.Resolve<IListBusiness<int, Domain.User>>().UpdateFromServiceAsync();
        }
    }
}
