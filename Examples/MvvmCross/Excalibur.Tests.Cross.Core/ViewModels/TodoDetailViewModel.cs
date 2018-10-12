using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Cross.Core.Presentation.Interfaces;
using MvvmCross;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class TodoDetailViewModel : DetailViewModel<int, Observable.Todo, ITodo>
    {
        private Observable.User _userObservable = new Observable.User();


        public TodoDetailViewModel(ITodo presentation) : base(presentation)
        {
            var userPresentation = Mvx.IoCProvider.Resolve<IListPresentation<int, Observable.User, Observable.User>>();

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
