using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Excalibur.Cross.Business;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
using MvvmCross.Commands;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class TodoViewModel : ListViewModel<int, Observable.Todo, Observable.Todo, ITodo, TodoDetailViewModel>
    {
        public TodoViewModel()
        {
            if (!Observables.Any())
            {
                Resolver.Resolve<IListBusiness<int, Domain.Todo>>().PublishFromStorageAsync();
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
