using System.Threading.Tasks;
using Excalibur.Tests.FormsCross.Core.Services.Interfaces;
using Excalibur.Tests.FormsCross.Core.State;
using Excalibur.Tests.FormsCross.Core.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.FormsCross.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IApplicationState _state;
        private readonly ILoginService _loginService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IApplicationState state, ILoginService loginService) : base(application, navigationService)
        {
            _state = state;
            _loginService = loginService;
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            // https://github.com/MvvmCross/MvvmCross/blob/bdaa09299714d94cf3f2c548a465d994c20d52f0/docs/_documentation/advanced/customizing-appstart.md

            // login things
            if (await _loginService.ValidateAsync().ConfigureAwait(false))
                //if (true)
            {
                await NavigationService.Navigate<MainViewModel>();
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
        }
    }
}