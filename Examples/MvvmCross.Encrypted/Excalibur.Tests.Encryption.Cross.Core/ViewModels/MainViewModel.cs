using Excalibur.Cross.ViewModels;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public void ShowMenu()
        {
            NavigationService.Navigate<DashboardViewModel>();
            NavigationService.Navigate<MenuViewModel>();
        }
    }
}
