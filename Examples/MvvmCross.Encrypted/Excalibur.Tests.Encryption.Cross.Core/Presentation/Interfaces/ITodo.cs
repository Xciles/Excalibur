using Excalibur.Cross.Collections;
using Excalibur.Cross.Presentation;

namespace Excalibur.Tests.Encrypted.Cross.Core.Presentation.Interfaces
{
    public interface ITodo : IListPresentation<int, Observable.Todo, Observable.Todo>
    {
        IObservableCollection<Observable.Todo> CurrentUserTodoObservables { get; set; }
    }
}
