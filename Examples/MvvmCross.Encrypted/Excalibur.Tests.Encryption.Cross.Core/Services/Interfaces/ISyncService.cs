using System.Threading.Tasks;

namespace Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces
{
    public interface ISyncService
    {
        Task FullSyncAsync();
        Task PartialSyncAsync();
    }
}
