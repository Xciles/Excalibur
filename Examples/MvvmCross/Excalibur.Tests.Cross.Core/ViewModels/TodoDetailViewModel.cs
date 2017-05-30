using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Presentation;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class TodoDetailViewModel : DetailViewModel<int, Observable.Todo, ITodo>
    {
        private Observable.User _userObservable = new Observable.User();

        public TodoDetailViewModel()
        {
            var userPresentation = Resolver.Resolve<IPresentation<int, Observable.User, Observable.User>>();

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
