using System.Linq;
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
            if (Observables.Any())
            {
                Resolver.Resolve<IListBusiness<int, Domain.User>>().PublishFromStorageAsync();
            }
        }
    }
}
