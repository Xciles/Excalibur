using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;
using XLabs.Ioc;
using Excalibur.Shared.Presentation;

namespace Excalibur.Tests.FormsCross.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private MenuItem _menuItem;
        private IEnumerable<MenuItem> _menu;
        private MvxCommand<MenuItem> _onSelectedChangedCommand;
        private Observable.LoggedInUser _currentUser = new Observable.LoggedInUser();
        private IMvxAsyncCommand _showCurrentUserCommand;

            
        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Menu = new[] {
                new MenuItem { Title = "Users", Description = "Users", ViewModelType = typeof(UserViewModel) },
                new MenuItem { Title = "Todos", Description = "Todos", ViewModelType = typeof(TodoViewModel) }
            };

            var userPresentation = Resolver.Resolve<ISinglePresentation<int, Observable.LoggedInUser>>();
            CurrentUserObservable = userPresentation.SelectedObservable;
        }

        public Observable.LoggedInUser CurrentUserObservable
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        public MenuItem SelectedMenu
        {
            get { return _menuItem; }
            set
            {
                if (SetProperty(ref _menuItem, value))
                    OnSelectedChangedCommand.Execute(value);
            }
        }

        public IEnumerable<MenuItem> Menu
        {
            get { return _menu; }
            set { SetProperty(ref _menu, value); }
        }

        public ICommand OnSelectedChangedCommand
        {
            get
            {
                return _onSelectedChangedCommand ?? (_onSelectedChangedCommand = new MvxCommand<MenuItem>((item) =>
                {
                    if (item == null)
                        return;

                    var vmType = item.ViewModelType;

                    // We demand to clear the Navigation stack as we are changing the section
                    var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });

                    // Show the ViewModel in the Detail NavigationPage
                    // todo change to new navigationservice
                    ShowViewModel(vmType, presentationBundle: presentationBundle);
                }));
            }
        }

        public IMvxAsyncCommand ShowCurrentUserCommand
        {
            get
            {
                _showCurrentUserCommand = _showCurrentUserCommand ?? new MvxAsyncCommand(() => Mvx.Resolve<IMvxNavigationService>().Navigate<CurrentUserViewModel>());
                return _showCurrentUserCommand;
            }
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Type ViewModelType { get; set; }
    }
}