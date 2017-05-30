using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Presentation;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private Observable.LoggedInUser _currentUser = new Observable.LoggedInUser();
        private IObservableCollection<Observable.Todo> _currentUserTodoObservables = new ExObservableCollection<Observable.Todo>(new List<Observable.Todo>());

        public DashboardViewModel()
        {
            var userPresentation = Resolver.Resolve<ISinglePresentation<int, Observable.LoggedInUser>>();
            CurrentUserObservable = userPresentation.SelectedObservable;

            var todoPresentation = Resolver.Resolve<ITodo>();
            CurrentUserTodoObservables = todoPresentation.CurrentUserTodoObservables;

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(5000);

                await Resolver.Resolve<IListBusiness<int, Domain.Todo>>().PublishFromStorageAsync();
            });
        }

        public Observable.LoggedInUser CurrentUserObservable
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        public IObservableCollection<Observable.Todo> CurrentUserTodoObservables
        {
            get { return _currentUserTodoObservables; }
            set { SetProperty(ref _currentUserTodoObservables, value); }
        }
    }
}
