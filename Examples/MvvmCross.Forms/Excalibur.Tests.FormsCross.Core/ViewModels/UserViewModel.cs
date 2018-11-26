using System.Linq;
using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using MvvmCross;
using MvvmCross.Commands;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
{
    public class UserViewModel : ListViewModel<int, Observable.User, Observable.User, IListPresentation<int, Observable.User, Observable.User>, UserDetailViewModel>
    {
        public UserViewModel(IListPresentation<int, Observable.User, Observable.User> presentation) : base(presentation)
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
