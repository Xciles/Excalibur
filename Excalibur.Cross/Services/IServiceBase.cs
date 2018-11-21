using System.Threading.Tasks;

namespace Excalibur.Cross.Services
{
    /// <summary>
    /// Interface for a service class that will manage communication with internal and external endpoints
    /// </summary>
    /// <typeparam name="T">The type of the object that is used for communication</typeparam>
    public interface IServiceBase<T>
    {
        /// <summary>
        /// Base method for syncing data.
        /// </summary>
        /// <returns>The objects that were downloaded/synced</returns>
        Task<T> SyncData();
    }
}
