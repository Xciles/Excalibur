using System.Threading.Tasks;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.FormsCross.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
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
        
        public override void ViewAppeared()
        {
            MvxNotifyTask.Create(async () =>
            {
                await NavigationService.Navigate<MenuViewModel>();
                await NavigationService.Navigate<DashboardViewModel>();
            });
        }
    }
}
