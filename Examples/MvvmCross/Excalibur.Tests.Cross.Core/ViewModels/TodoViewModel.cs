using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class TodoViewModel : ListViewModel<int, Observable.Todo, Observable.Todo, TodoDetailViewModel>
    {
        public TodoViewModel()
        {
            Resolver.Resolve<IListBusiness<int, Domain.Todo>>().UpdateFromServiceAsync();
        }
    }
}
