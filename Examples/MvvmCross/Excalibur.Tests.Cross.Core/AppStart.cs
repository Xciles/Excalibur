using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly ILoginService _loginService;

        public AppStart(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public void Start(object hint = null)
        {
            // login things
            if (_loginService.ValidateAsync().GetAwaiter().GetResult())
            {
                // todo init sync and loading of data
                Resolver.Resolve<ISyncService>().PartialSyncAsync().ConfigureAwait(false);

                Mvx.Resolve<IMvxNavigationService>().Navigate<MainViewModel>();
            }
            else
            {
                Mvx.Resolve<IMvxNavigationService>().Navigate<LoginViewModel>();
            }
        }
    }
}
