using System.Windows.Input;
using Excalibur.Cross.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly MvxPresentationHint _popToRootHint = Mvx.Resolve<MvxPresentationHint>();
        private IMvxAsyncCommand _showUsersCommand;
        private IMvxAsyncCommand _showTodosCommand;
        private IMvxAsyncCommand _showCurrentUserCommand;


        public IMvxCommand PopToRootCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ChangePresentation(_popToRootHint);
                });
            }
        }

        public IMvxAsyncCommand ShowUsersCommand
        {
            get
            {
                _showUsersCommand = _showUsersCommand ?? new MvxAsyncCommand(() => NavigationService.Navigate<UserViewModel>());
                return _showUsersCommand;
            }
        }

        public IMvxAsyncCommand ShowTodosCommand
        {
            get
            {
                _showTodosCommand = _showTodosCommand ?? new MvxAsyncCommand(() => NavigationService.Navigate<TodoViewModel>());
                return _showTodosCommand;
            }
        }

        public IMvxAsyncCommand ShowCurrentUserCommand
        {
            get
            {
                _showCurrentUserCommand = _showCurrentUserCommand ?? new MvxAsyncCommand(() => NavigationService.Navigate<CurrentUserViewModel>());
                return _showCurrentUserCommand;
            }
        }
    }
}
