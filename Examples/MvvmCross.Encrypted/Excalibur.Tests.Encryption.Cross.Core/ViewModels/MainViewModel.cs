using System.Threading.Tasks;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces;
using MvvmCross;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Task.Run(async () =>
            {
                await Mvx.IoCProvider.Resolve<ISyncService>().PartialSyncAsync();
                // todo load from initial state
                // todo initial sync
                // todo sync in background

            }).ConfigureAwait(false);
        }

        public void ShowMenu()
        {
            NavigationService.Navigate<DashboardViewModel>();
            NavigationService.Navigate<MenuViewModel>();
        }
    }
}
