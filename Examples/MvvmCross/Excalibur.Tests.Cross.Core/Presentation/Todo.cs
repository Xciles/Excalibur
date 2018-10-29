using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Cross.Collections;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.Utils;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
using MvvmCross;
using MvvmCross.Base;

namespace Excalibur.Tests.Cross.Core.Presentation
{
    public class Todo : BaseListPresentation<int, Domain.Todo, Observable.Todo, Observable.Todo>, ITodo
    {
        private IObservableCollection<Observable.Todo> _currentUserTodoObservables = new ExObservableCollection<Observable.Todo>(new List<Observable.Todo>());

        public Todo(IObjectMapper<Domain.Todo, Observable.Todo> domainSelectedMapper, IObjectMapper<Domain.Todo, Observable.Todo> domainObservableMapper, IObjectMapper<Observable.Todo, Observable.Todo> observableSelectedMapper, IListBusiness<int, Domain.Todo> listBusiness, IMvxMainThreadAsyncDispatcher dispatcher) : base(domainSelectedMapper, domainObservableMapper, observableSelectedMapper, listBusiness, dispatcher)
        {
        }

        protected override async Task ListUpdatedHandler(MessageBase<IList<Domain.Todo>> messageBase)
        {
            await base.ListUpdatedHandler(messageBase);

            Cde.Wait(1000);

            var userPresentation = Mvx.IoCProvider.Resolve<ISinglePresentation<int, Observable.LoggedInUser>>();
            if (!userPresentation.SelectedObservable.IsTransient())
            {
                var dispatcher = Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>();
                dispatcher.ExecuteOnMainThreadAsync(() => { CurrentUserTodoObservables.Clear(); });
                var todosFromUser = Observables.Where(x => x.UserId.Equals(userPresentation.SelectedObservable.Id)).Take(5);
                foreach (var todo in todosFromUser)
                {
                    dispatcher.ExecuteOnMainThreadAsync(() => { CurrentUserTodoObservables.Add(todo); });
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
