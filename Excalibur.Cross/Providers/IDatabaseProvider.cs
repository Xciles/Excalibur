using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excalibur.Cross.Providers
{
    /// <summary>
    /// A database provider that provides a general data storage interface. 
    /// This class will use TId as a type identifier for objects
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    public interface IDatabaseProvider<in TId, T>
        where T : ProviderDomain<TId>
    {
        /// <summary>
        /// Insert an item
        /// </summary>
        /// <param name="item">The objects to insert</param>
        Task Insert(T item);

        /// <summary>
        /// Insert multiple items at once
        /// </summary>
        /// <param name="items">The objects to insert</param>
        Task InsertBulk(IEnumerable<T> items);

        /// <summary>
        /// Insert or update an item
        /// </summary>
        /// <param name="item">The object to insert or update</param>
        /// <returns>True, if inserted, false if updated.</returns>
        Task<bool> Upsert(T item);

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item">The object to update</param>
        Task<bool> Update(T item);

        /// <summary>
        /// Delete stored items based on a predicate in the store
        /// </summary>
        /// <param name="predicate">The predicate that should be used for deleting</param>
        Task Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find all items within a store
        /// </summary>
        /// <returns>All items in the store</returns>
        Task<IEnumerable<T>> FindAll();

        /// <summary>
        /// Find an item based on an Id
        /// </summary>
        /// <param name="id">The Id to find the item for</param>
        /// <returns>The stored item</returns>
        Task<T> FindById(TId id);

        /// <summary>
        /// Find items based on a predicate
        /// </summary>
        /// <param name="predicate">The predicate that should be used for searching</param>
        /// <param name="skip">Amount of items to skip</param>
        /// <param name="take">Amount of items to return</param>
        /// <returns>All found items</returns>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue);

        /// <summary>
        /// Find the first item within the store
        /// </summary>
        /// <returns>The first item in the store</returns>
        Task<T> FirstOrDefault();

        /// <summary>
        /// Find the first item within the store based on a predicate
        /// </summary>
        /// <param name="predicate">The predicate that should be used for searching</param>
        /// <returns>The first item in the store</returns>
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Clear the database for a given type
        /// </summary>
        Task Clear();
    }
}