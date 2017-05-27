using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace Excalibur.Tests.Cross.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly ILoginService _loginService;

        public AppStart(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async void Start(object hint = null)
        {
            // login things
            if (await _loginService.ValidateAsync())
            {
                // todo init sync and loading of data
                ShowViewModel<MainViewModel>();
            }
            else
            {
                ShowViewModel<LoginViewModel>();
            }
        }
    }
}
