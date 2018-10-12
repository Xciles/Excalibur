using System.Threading.Tasks;
using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.State;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.Cross.Core
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
                // todo init sync and loading of data
                Mvx.IoCProvider.Resolve<ISyncService>().PartialSyncAsync().ConfigureAwait(false);

                await NavigationService.Navigate<MainViewModel>();
                //await NavigationService.Navigate<DashboardViewModel>();
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
        }
    }
}
