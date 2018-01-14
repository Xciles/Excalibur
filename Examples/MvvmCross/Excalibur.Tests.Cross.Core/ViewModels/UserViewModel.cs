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
    public class UserViewModel : ListViewModel<int, Observable.User, Observable.User, IListPresentation<int, Observable.User, Observable.User>, UserDetailViewModel>
    {
        public UserViewModel()
        {
            if (!Observables.Any())
            {
                Resolver.Resolve<IListBusiness<int, Domain.User>>().PublishFromStorageAsync();
            }
        }

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    // Todo fix IsLoading presentation ref
                    IsLoading = true;

                    await Task.Delay(5000);
                    await Resolver.Resolve<IListBusiness<int, Domain.User>>().UpdateFromServiceAsync();

                    IsLoading = false;
                });
            }
        }
    }
}
