using System.Windows.Input;
using Excalibur.Cross.ViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly MvxPresentationHint _popToRootHint = Mvx.Resolve<MvxPresentationHint>();
        private IMvxAsyncCommand _showDashboardCommand;
        private IMvxAsyncCommand _showUsersCommand;
        private IMvxAsyncCommand _showTodosCommand;
        private IMvxAsyncCommand _showCurrentUserCommand;


        public IMvxAsyncCommand PopToRootCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await NavigationService.ChangePresentation(_popToRootHint);
                });
            }
        }

        public IMvxAsyncCommand ShowDashboardCommand
        {
            get
            {
                _showDashboardCommand = _showDashboardCommand ?? new MvxAsyncCommand(async () => await NavigationService.Navigate<DashboardViewModel>());
                return _showDashboardCommand;
            }
        }

        public IMvxAsyncCommand ShowUsersCommand
        {
            get
            {
                _showUsersCommand = _showUsersCommand ?? new MvxAsyncCommand(async () => await NavigationService.Navigate<UserViewModel>());
                return _showUsersCommand;
            }
        }

        public IMvxAsyncCommand ShowTodosCommand
        {
            get
            {
                _showTodosCommand = _showTodosCommand ?? new MvxAsyncCommand(async () => await NavigationService.Navigate<TodoViewModel>());
                return _showTodosCommand;
            }
        }

        public IMvxAsyncCommand ShowCurrentUserCommand
        {
            get
            {
                _showCurrentUserCommand = _showCurrentUserCommand ?? new MvxAsyncCommand(async () => await NavigationService.Navigate<CurrentUserViewModel>());
                return _showCurrentUserCommand;
            }
        }
    }
}
