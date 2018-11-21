using System;
using System.Linq.Expressions;
using Excalibur.Base.Providers;

namespace Excalibur.Providers.LiteDb
{
    /// <summary>
    /// A database provider that provides a general data storage interface.
    /// Providers implementing this interface should use LiteDb As data store
    /// This class will use TId as a type identifier for objects
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    public interface ILiteDbProvider<in TId, T> : IDatabaseProvider<TId, T> 
        where T : ProviderDomain<TId>
    {
        /// <summary>
        /// Create a new permanent index in all documents inside this collections if index not exists already.
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="property">Property linq expression</param>
        /// <param name="expression">Create a custom expression function to be indexed</param>
        /// <param name="unique">Create a unique keys index?</param>
        /// <returns></returns>
        bool EnsureIndex<TK>(Expression<Func<T, TK>> property, string expression, bool unique = false);

        /// <summary>
        /// Create a new permanent index in all documents inside this collections if index not exists already.
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="property">Property linq expression</param>
        /// <param name="unique">Create a unique keys index?</param>
        /// <returns></returns>
        bool EnsureIndex<TK>(Expression<Func<T, TK>> property, bool unique = false);

        /// <summary>
        /// Drop index and release slot for another index
        /// </summary>
        /// <param name="field">Field to drop the index for</param>
        /// <returns></returns>
        bool DropIndex(string field);
    }
}