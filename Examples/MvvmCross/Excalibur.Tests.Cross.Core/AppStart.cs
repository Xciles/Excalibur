using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core
{
    public class AppStart : MvxAppStart, IMvxAppStart
    {
        private readonly ILoginService _loginService;

        public AppStart(IMvxApplication application, ILoginService loginService) :
            base(application)
        {
            _loginService = loginService;
        }

        public override void ResetStart()
        {
            // login things
            if (_loginService.ValidateAsync().GetAwaiter().GetResult())
            {
                // todo init sync and loading of data
                Resolver.Resolve<ISyncService>().PartialSyncAsync().ConfigureAwait(false);

                var navigationService = Mvx.Resolve<IMvxNavigationService>();

                navigationService.Navigate<MainViewModel>();
                navigationService.Navigate<DashboardViewModel>();
            }
            else
            {
                Mvx.Resolve<IMvxNavigationService>().Navigate<LoginViewModel>();
            }
        }
    }
}
