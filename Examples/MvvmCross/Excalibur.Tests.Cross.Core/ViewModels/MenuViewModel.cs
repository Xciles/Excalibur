using System.Windows.Input;
using Excalibur.Cross.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private MvxPresentationHint popToRootHint = Mvx.Resolve<MvxPresentationHint>();

        public IMvxCommand PopToRootCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ChangePresentation(popToRootHint);
                });
            }
        }

        public IMvxCommand ShowUsersCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<UserViewModel>();
                });
            }
        }

        public IMvxCommand ShowTodosCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<TodoViewModel>();
                });
            }
        }

        public IMvxCommand ShowCurrentUserCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<CurrentUserViewModel>();
                });
            }
        }
    }
}
