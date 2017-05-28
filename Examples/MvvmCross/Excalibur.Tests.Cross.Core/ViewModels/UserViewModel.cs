using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Shared.Presentation;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class UserViewModel : ListViewModel<int, Observable.User, Observable.User, IPresentation<int, Observable.User, Observable.User>, UserDetailViewModel>
    {
        public UserViewModel()
        {
            Resolver.Resolve<IListBusiness<int, Domain.User>>().UpdateFromServiceAsync();
        }
    }
}
