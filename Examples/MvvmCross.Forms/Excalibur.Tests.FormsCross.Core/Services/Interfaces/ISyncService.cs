using System.Threading.Tasks;

namespace Excalibur.Tests.FormsCross.Core.Services.Interfaces
{
    public interface ISyncService
    {
        Task FullSyncAsync();
        Task PartialSyncAsync();
    }
}
