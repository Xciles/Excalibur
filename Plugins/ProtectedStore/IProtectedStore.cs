using System.Collections.Generic;

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
        /// Returns all stored information (as string) for a given identifier
        /// </summary>
        /// <param name="identifier">Identifier to retrieve the stored information for</param>
        /// <returns>Stored information for the identifier</returns>
        IEnumerable<string> GetStringsForIdentifier(string identifier);
        /// <summary>
        /// Saves a certain combination into secure storage.
        /// Identifier acts as the key.
        /// </summary>
        /// <param name="stringToSave">The string to save</param>
        /// <param name="identifier">The Identifier as key</param>
        void Save(string stringToSave, string identifier);
        /// <summary>
        /// Deletes all information for a given identifier
        /// </summary>
        /// <param name="identifier">The Identifier that was used as key</param>
        void Delete(string identifier);
    }
}
