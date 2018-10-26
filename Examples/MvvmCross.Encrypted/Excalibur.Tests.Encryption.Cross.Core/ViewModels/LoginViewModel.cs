using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels;
using MvvmCross;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private string _email;
        private string _password;
        private bool _isLoading;

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;

            Email = "myemail@10hourmail.com";
            Password = "VerySecret";
            IsLoading = false;
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public IMvxAsyncCommand LoginCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    IsLoading = true;
                    if (await _loginService.LoginAsync(Email, Password))
                    {
                        // Todo init sync things
                        Mvx.IoCProvider.Resolve<ISyncService>().FullSyncAsync().ConfigureAwait(false);

                        await NavigationService.Navigate<RegisterPinViewModel>();
                    }
                    else
                    {
                        // Todo alert dialog
                        IsLoading = false;
                    }
                }, () => !IsLoading);
            }
        }
    }
}
