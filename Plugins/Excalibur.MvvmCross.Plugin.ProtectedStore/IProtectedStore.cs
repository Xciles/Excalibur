using System.Collections.Generic;

// Initial version based on: 
// - https://github.com/escfrya/Locator/tree/master/Xamarin.Auth
// - http://stackoverflow.com/questions/28429004/correct-way-to-store-encryption-key-for-sqlcipher-database
namespace Excalibur.MvvmCross.Plugin.ProtectedStore
{
    public interface IProtectedStore
    {
        IEnumerable<string> GetStringsForIdentifier(string identifier);
        void Save(string stringToSave, string identifier);
        void Delete(string identifier);
    }
}
