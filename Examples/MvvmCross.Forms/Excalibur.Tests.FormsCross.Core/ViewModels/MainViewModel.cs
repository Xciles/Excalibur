using System.Threading.Tasks;
using Excalibur.Cross.ViewModels;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Task.Run(async () =>
            {
                // todo load from initial state
                // todo initial sync
                // todo sync in background

            }).ConfigureAwait(false);
        }

        public override void ViewAppeared()
        {
            MvxNotifyTask.Create(async () => {
                await NavigationService.Navigate<DashboardViewModel>();
                await NavigationService.Navigate<MenuViewModel>();
            });
        }
    }
}