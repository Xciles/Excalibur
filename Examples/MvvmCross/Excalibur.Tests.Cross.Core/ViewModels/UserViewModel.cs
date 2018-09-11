using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Excalibur.Cross.Business;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using MvvmCross.Commands;
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

        public IMvxAsyncCommand ReloadCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
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
