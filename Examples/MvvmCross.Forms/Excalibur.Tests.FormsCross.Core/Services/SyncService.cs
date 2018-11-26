using System.Threading;
using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Tests.FormsCross.Core.Services.Interfaces;
using MvvmCross;

namespace Excalibur.Tests.FormsCross.Core.Services
{
    public class SyncService : ISyncService
    {
        private static Timer _timer;
        private static bool _isSyncing = false;

        public SyncService()
        {
            var delayInMs = 10 * 60 * 1000; // 10 mins in milliseconds
            _timer = new Timer(CallbackAsync, null, delayInMs, delayInMs);
        }

        private async void CallbackAsync(object o)
        {
            if (_isSyncing) return;
            _isSyncing = true;

            await PartialSyncAsync();

            _isSyncing = false;
        }

        public async Task FullSyncAsync()
        {
            await Mvx.IoCProvider.Resolve<IListBusiness<int, Domain.User>>().UpdateFromService();
            await Mvx.IoCProvider.Resolve<IListBusiness<int, Domain.Todo>>().UpdateFromService();
        }

        public async Task PartialSyncAsync()
        {
            await Task.Delay(5000);
            await FullSyncAsync();
        }
    }
}
