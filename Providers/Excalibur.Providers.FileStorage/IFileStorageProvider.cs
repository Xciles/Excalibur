using Excalibur.Base.Providers;

namespace Excalibur.Providers.FileStorage
{
    /// <summary>
    /// A database provider that provides a general data storage interface.
    /// Provider using this interface should use file storage as data store.
    /// This class will use TId as a type identifier for objects
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    public interface IFileStorageProvider<TId, T> : IDatabaseProvider<TId, T> 
        where T : ProviderDomain<TId>
    {
    }
}