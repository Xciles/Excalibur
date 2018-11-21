using System.Linq;
using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.Presentation.Interfaces;
using MvvmCross;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels
{
    public class TodoViewModel : ListViewModel<int, Observable.Todo, Observable.Todo, ITodo, TodoDetailViewModel>
    {
        public TodoViewModel(ITodo presentation) : base(presentation)
        {
            if (!Observables.Any())
            {
                Mvx.IoCProvider.Resolve<IListBusiness<int, Domain.Todo>>().PublishFromStorage();
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
