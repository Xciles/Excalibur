using Excalibur.Cross.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public void ShowMenu()
        {
            Mvx.Resolve<IMvxNavigationService>().Navigate<DashboardViewModel>();
            Mvx.Resolve<IMvxNavigationService>().Navigate<MenuViewModel>();
        }
    }
}
