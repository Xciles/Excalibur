using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excalibur.Shared.Storage.Providers
{
    /// <summary>
    /// A object storage provider that provides a general data storage interface
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    public interface IObjectStorageProvider<in TId, T> where T : StorageDomain<TId>
    {
        /// <summary>
        /// Store a range of <see cref="T"/>
        /// </summary>
        /// <param name="objectsToStore">The objects to store</param>
        /// <returns>An await able Task</returns>
        Task StoreRange(IList<T> objectsToStore);
        /// <summary>
        /// Gets a range of <see cref="T"/> from storage
        /// </summary>
        /// <returns>An await able Task with the range as result</returns>
        Task<IList<T>> GetRange(); // todo: change to take skip, returning all for now

        /// <summary>
        /// Get a certain <see cref="T"/>
        /// </summary>
        /// <param name="id">The id of the object that will be retrieved</param>
        /// <returns>An await able Task with the requested object as result</returns>
        Task<T> Get(TId id);
        /// <summary>
        /// Adds or updates an object to storage
        /// </summary>
        /// <param name="objectToStore">The object to store</param>
        /// <returns>An await able Task with the success as result</returns>
        Task<bool> AddOrUpdate(T objectToStore);
        /// <summary>
        /// Deletes an object with a certain Id
        /// </summary>
        /// <param name="id">The id of the object that will be deleted</param>
        /// <returns>An await able Task with the success as result</returns>
        Task<bool> Delete(TId id);

        // GetRange
        // SetRange
        // Create
        // Update
        // Read
        // Delete
    }
}
