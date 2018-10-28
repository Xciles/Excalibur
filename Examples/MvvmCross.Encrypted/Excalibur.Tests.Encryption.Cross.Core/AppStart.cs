using System.Threading.Tasks;
using Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Encrypted.Cross.Core.State;
using Excalibur.Tests.Encrypted.Cross.Core.Utils;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.Encrypted.Cross.Core
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
            if (_state.HasConfiguration()) 
            //if (_state.Pin?.Length == ExampleConstants.PinRequirements.Length) 
            {
                await NavigationService.Navigate<PinLoginViewModel>();
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
        }
    }
}
