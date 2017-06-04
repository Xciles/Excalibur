using Excalibur.Shared.Collections;
using Excalibur.Shared.Presentation;

namespace Excalibur.Tests.FormsCross.Presentation.Interfaces
{
    public interface ITodo : IPresentation<int, Observable.Todo, Observable.Todo>
    {
        IObservableCollection<Observable.Todo> CurrentUserTodoObservables { get; set; }
    }
}
