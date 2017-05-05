using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excalibur.Shared.Storage.Providers
{
    public interface IObjectStorageProvider<in TId, T> where T : StorageDomain<TId>
    {
        Task StoreRange(IList<T> objectsToStore);
        Task<IList<T>> GetRange(); // todo: change to take skip, returning all for now

        Task<T> Get(TId id);
        Task<bool> AddOrUpdate(T objectToStore);
        Task<bool> Delete(TId id);

        // GetRange
        // SetRange
        // Create
        // Update
        // Read
        // Delete
    }
}
