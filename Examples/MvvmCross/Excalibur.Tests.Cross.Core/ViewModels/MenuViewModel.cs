using Excalibur.Cross.ViewModels;
using MvvmCross.Core.ViewModels;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public IMvxCommand ShowDashboardCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<DashboardViewModel>();
                });
            }
        }
        public IMvxCommand ShowUsersCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<UserViewModel>();
                });
            }
        }
    }
}
