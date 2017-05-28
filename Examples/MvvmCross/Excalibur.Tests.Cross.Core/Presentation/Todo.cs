using System.Collections.Generic;
using System.Linq;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Presentation;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
using Excalibur.Utils;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.Presentation
{
    public class Todo : BasePresentation<int, Domain.Todo, Observable.Todo, Observable.Todo>, ITodo
    {
        private IObservableCollection<Observable.Todo> _currentUserTodoObservables = new ExObservableCollection<Observable.Todo>(new List<Observable.Todo>());

        protected override void ListUpdatedHandler(MessageBase<IList<Domain.Todo>> messageBase)
        {
            base.ListUpdatedHandler(messageBase);

            Cde.Wait(1000);

            var userPresentation = Resolver.Resolve<IPresentation<int, Observable.LoggedInUser>>();
            if (!userPresentation.SelectedObservable.IsTransient())
            {
                CurrentUserTodoObservables.Clear();
                var todosFromUser = Observables.Where(x => x.UserId.Equals(userPresentation.SelectedObservable.Id)).Take(5);
                foreach (var todo in todosFromUser)
                {
                    CurrentUserTodoObservables.Add(todo);
                }
            }
        }

        public IObservableCollection<Observable.Todo> CurrentUserTodoObservables
        {
            get { return _currentUserTodoObservables; }
            set { SetProperty(ref _currentUserTodoObservables, value); }
        }
    }
}
