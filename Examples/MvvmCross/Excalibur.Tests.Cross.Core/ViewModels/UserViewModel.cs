using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Excalibur.Cross.Business;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Cross.Core.Observable;
using MvvmCross;
using MvvmCross.Commands;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class UserViewModel : ListViewModel<int, Observable.User, Observable.User, IListPresentation<int, Observable.User, Observable.User>, UserDetailViewModel>
    {
        public UserViewModel(IListPresentation<int, User, User> presentation) : base(presentation)
        {
            if (!Observables.Any())
            {
                Mvx.IoCProvider.Resolve<IListBusiness<int, Domain.User>>().PublishFromStorage();
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
                    await Mvx.IoCProvider.Resolve<IListBusiness<int, Domain.User>>().UpdateFromService();

                    IsLoading = false;
                });
            }
        }
    }
}
