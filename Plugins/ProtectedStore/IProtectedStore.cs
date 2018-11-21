using System.Collections.Generic;
using System.Threading.Tasks;

// Initial version based on: 
// - https://github.com/escfrya/Locator/tree/master/Xamarin.Auth
// - http://stackoverflow.com/questions/28429004/correct-way-to-store-encryption-key-for-sqlcipher-database
namespace Excalibur.MvvmCross.Plugin.ProtectedStore
{
    /// <summary>
    /// This provides a way to store and retrieve something in the KeyStore, KeyChain or DataProtectionProvider.
    /// </summary>
    public interface IProtectedStore
    {
        /// <summary>
        /// Returns stored information (as string) for a given identifier
        /// </summary>
        /// <param name="identifier">Identifier to retrieve the stored information for</param>
        /// <returns>Stored information for the identifier</returns>
        Task<string> GetStringForIdentifier(string identifier);

        /// <summary>
        /// Returns all stored information (as string) for a given identifier
        /// </summary>
        /// <param name="identifier">Identifier to retrieve the stored information for</param>
        /// <returns>Stored information for the identifier</returns>
        Task<IEnumerable<string>> GetStringsForIdentifier(string identifier);

        /// <summary>
        /// Saves a certain combination into secure storage.
        /// Identifier acts as the key.
        /// </summary>
        /// <param name="identifier">The Identifier as key</param>
        /// <param name="stringToSave">The string to save</param>
        Task Save(string identifier, string stringToSave);

        /// <summary>
        /// Deletes all information for a given identifier
        /// </summary>
        /// <param name="identifier">The Identifier that was used as key</param>
        Task Delete(string identifier);

        /// <summary>
        /// Provides a way to platform specifically initialize if needed.
        /// Currently this is only being used by Android to create a store with a certain password.
        /// </summary>
        /// <param name="password">The password for the KeyStore</param>
        /// <param name="fileName">A filename for the KeyStore</param>
        void Initialize(string password, string fileName = "Excalibur.Store");

        /// <summary>
        /// Unloads the store, removes everything from memory if needed and clears the current state
        /// </summary>
        void Terminate();

        /// <summary>
        /// Removes the store if needed.
        /// </summary>
        void Remove();
    }
}
