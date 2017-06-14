using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Presentation;
using Excalibur.Tests.FormsCross.Presentation.Interfaces;
using XLabs.Ioc;

namespace Excalibur.Tests.FormsCross.ViewModels
{
    public class TodoDetailViewModel : DetailViewModel<int, Observable.Todo, ITodo>
    {
        private Observable.User _userObservable = new Observable.User();

        public TodoDetailViewModel()
        {
            var userPresentation = Resolver.Resolve<IListPresentation<int, Observable.User, Observable.User>>();

            var user = userPresentation.GetObservable(SelectedObservable.UserId);
            if (user != null)
            {
                UserObservable = user;
            }
        }

        public Observable.User UserObservable
        {
            get { return _userObservable; }
            set { SetProperty(ref _userObservable, value); }
        }
    }
}
