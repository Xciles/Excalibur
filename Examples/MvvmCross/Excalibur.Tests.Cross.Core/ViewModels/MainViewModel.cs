using Excalibur.Cross.ViewModels;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public void ShowMenu()
        {
            ShowViewModel<DashboardViewModel>();
            ShowViewModel<MenuViewModel>();
        }
    }
}
