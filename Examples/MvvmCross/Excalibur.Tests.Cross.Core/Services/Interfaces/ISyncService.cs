using System.Threading.Tasks;

namespace Excalibur.Tests.Cross.Core.Services.Interfaces
{
    public interface ISyncService
    {
        Task FullSyncAsync();
        Task PartialSyncAsync();
    }
}
