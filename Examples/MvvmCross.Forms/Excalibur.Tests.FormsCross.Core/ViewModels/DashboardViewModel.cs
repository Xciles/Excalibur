using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Cross.Collections;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.FormsCross.Core.Presentation.Interfaces;
using MvvmCross;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private Observable.LoggedInUser _currentUser = new Observable.LoggedInUser();
        private IObservableCollection<Observable.Todo> _currentUserTodoObservables = new ExObservableCollection<Observable.Todo>(new List<Observable.Todo>());

        public DashboardViewModel()
        {
            var userPresentation = Mvx.IoCProvider.Resolve<ISinglePresentation<int, Observable.LoggedInUser>>();
            CurrentUserObservable = userPresentation.SelectedObservable;

            var todoPresentation = Mvx.IoCProvider.Resolve<ITodo>();
            CurrentUserTodoObservables = todoPresentation.CurrentUserTodoObservables;

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(5000);

                if (!CurrentUserTodoObservables.Any())
                {
                    await Mvx.IoCProvider.Resolve<IListBusiness<int, Domain.Todo>>().PublishFromStorage();
                }
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
