using System.Collections.Generic;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.FormsCross.Core.ViewModels.Menu;
using MvvmCross.Commands;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private IMvxAsyncCommand _showDashboardCommand;
        private IMvxAsyncCommand _showUsersCommand;
        private IMvxAsyncCommand _showTodosCommand;
        private IMvxAsyncCommand _showCurrentUserCommand;

        private List<MenuItem> _menuItems;

        public List<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);

        }

        public MenuViewModel()
        {
            MenuItems = new List<MenuItem>();
            MenuItems.Add(new CurrentUserMenuItem() { Title = TextSource.GetText("CurrentUser"), Navigate = ShowCurrentUserCommand });

            MenuItems.Add(new HeaderMenuItem() { Title = TextSource.GetText("OtherItems") });
            MenuItems.Add(new MenuItem() { Title = TextSource.GetText("Dashboard"), Navigate = ShowDashboardCommand });
            MenuItems.Add(new MenuItem() { Title = TextSource.GetText("Users"), Navigate = ShowUsersCommand });
            MenuItems.Add(new MenuItem() { Title = TextSource.GetText("Todos"), Navigate = ShowTodosCommand });
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
