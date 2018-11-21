using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excalibur.Cross.Business
{
    /// <summary>
    /// An entity to represent a list of items and with it a list of domain objects.
    /// This should be used when for example managing a list of objects. 
    /// For example, users, todos, comments, etc.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be represented</typeparam>
    public interface IListBusiness<in TId, TDomain> : IBusiness
    {
        /// <summary>
        /// Get all domain objects that are managed by this business entity
        /// </summary>
        /// <returns>All available objects</returns>
        Task<IEnumerable<TDomain>> FindAll();

        /// <summary>
        /// Get a single domain object from storage by a TId
        /// </summary>
        /// <param name="id">The Id of the object to get</param>
        /// <returns>The requested object</returns>
        Task<TDomain> GetByIdAsync(TId id);

        /// <summary>
        /// Deletes a domain object by a TId
        /// </summary>
        /// <param name="id">The Id of the object to delete</param>
        Task DeleteItemAsync(TId id);

        /// <summary>
        /// Find the first item within the store based on a predicate
        /// </summary>
        /// <param name="predicate">The predicate that should be used for searching</param>
        /// <returns>The first item in the store</returns>
        Task<TDomain> FirstOrDefault(Expression<Func<TDomain, bool>> predicate);

        /// <summary>
        /// Find items based on a predicate
        /// </summary>
        /// <param name="predicate">The predicate that should be used for searching</param>
        /// <param name="skip">Amount of items to skip</param>
        /// <param name="take">Amount of items to return</param>
        /// <returns>All found items</returns>
        Task<IEnumerable<TDomain>> Find(Expression<Func<TDomain, bool>> predicate, int skip = 0, int take = int.MaxValue);
    }
}