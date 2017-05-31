using System.Linq;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
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
    }
}
