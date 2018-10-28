using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Security;

namespace Excalibur.MvvmCross.Plugin.ProtectedStore.Platforms.Ios
{
    /// <inheritdoc />
    public class ProtectedStore : IProtectedStore
    {
        /// <inheritdoc />
        public Task<string> GetStringForIdentifier(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword)
            {
                Service = identifier
            };

            var record = SecKeyChain.QueryAsRecord(query, out _);

            return Task.FromResult(GetStringFromRecord(record));
        }

        /// <inheritdoc />
        public Task<IEnumerable<string>> GetStringsForIdentifier(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword)
            {
                Service = identifier
            };

            var records = SecKeyChain.QueryAsRecord(query, 1000, out _);

            return records != null 
                    ? 
                    Task.FromResult<IEnumerable<string>>(records.Select(GetStringFromRecord).ToList()) 
                    : 
                    Task.FromResult(Enumerable.Empty<string>());
        }

        /// <inheritdoc />
        public Task Save(string identifier, string stringToSave)
        {
            //
            // Remove any existing record
            //
            var existing = FindString(identifier);

            if (existing != null)
            {
                Delete(identifier);
            }

            //
            // Add this record
            //
            var record = new SecRecord(SecKind.GenericPassword)
            {
                Service = identifier,
                Generic = NSData.FromString(stringToSave, NSStringEncoding.UTF8),
                Accessible = SecAccessible.WhenUnlocked
            };

            var statusCode = SecKeyChain.Add(record);

            if (statusCode != SecStatusCode.Success)
            {
                throw new Exception("Could not save account to KeyChain: " + statusCode);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Delete(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword)
            {
                Service = identifier
            };

            var statusCode = SecKeyChain.Remove(query);

            if (statusCode != SecStatusCode.Success)
            {
                throw new Exception("Could not delete account from KeyChain: " + statusCode);
            }

            return Task.CompletedTask;
        }

        private string GetStringFromRecord(SecRecord record) => NSString.FromData(record.Generic, NSStringEncoding.UTF8);

        private string FindString(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword)
            {
                Service = identifier
            };

            var record = SecKeyChain.QueryAsRecord(query, out _);

            return record != null ? GetStringFromRecord(record) : null;
        }
    }
}
