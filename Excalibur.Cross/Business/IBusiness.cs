using System.Threading.Tasks;
using Excalibur.Cross.Services;

namespace Excalibur.Cross.Business
{
    /// <summary>
    /// Business entity interface for Excalibur
    /// This class should manage storage, communication via services etc.
    /// </summary>
    public interface IBusiness
    {
        /// <summary>
        /// Updates the storage entities using a <see cref="IServiceBase{T}"/>. 
        /// Afterwards this should publish a message to all subscribers
        /// </summary>
        Task UpdateFromService();

        /// <summary>
        /// Publish a message to notify subscribers. Entities should be loaded (if needed) from storage, not from services.
        /// </summary>
        Task PublishFromStorage();

        /// <summary>
        /// Method that will be called right after the <see cref="IServiceBase{T}.SyncData"/> and before actually storing items.
        /// This provides a way to do additional checks or update/delete items that were not returned.
        /// </summary>
        Task AfterServiceSyncData();

        /// <summary>
        /// Clears the underlining database provider, removing all items that are currently stored.
        /// This will also publish a message that makes sure the presentation will update properly.
        /// </summary>
        Task Clear();
    }
}
