using System.Threading.Tasks;

namespace Excalibur.Tests.FormsCross.Services.Interfaces
{
    public interface ISyncService
    {
        Task FullSyncAsync();
        Task PartialSyncAsync();
    }
}
