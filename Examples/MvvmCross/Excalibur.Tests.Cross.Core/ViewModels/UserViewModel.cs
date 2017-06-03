using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.ViewModels;
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


        public virtual ICommand ItemSelected
        {
            get
            {
                return new MvxCommand<Observable.User>(item =>
                {
                    Resolver.Resolve<IPresentation<int, Observable.User, Observable.User>>().SetSelectedObservable(item.Id);
                });
            }
        }

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    await Task.Delay(5000);
                    await Resolver.Resolve<IListBusiness<int, Domain.User>>().UpdateFromServiceAsync();
                });
            }
        }
    }
}
