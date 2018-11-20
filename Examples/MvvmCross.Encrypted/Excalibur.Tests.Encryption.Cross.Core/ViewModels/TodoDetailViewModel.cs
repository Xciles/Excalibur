using System.Threading.Tasks;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.Presentation.Interfaces;
using MvvmCross;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels
{
    public class TodoDetailViewModel : DetailViewModel<int, Observable.Todo, ITodo>
    {
        private Observable.User _userObservable = new Observable.User();


        public TodoDetailViewModel(ITodo presentation) : base(presentation)
        {
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            var userPresentation = Mvx.IoCProvider.Resolve<IListPresentation<int, Observable.User, Observable.User>>();

            var user = await userPresentation.GetObservable(SelectedObservable.UserId);
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
