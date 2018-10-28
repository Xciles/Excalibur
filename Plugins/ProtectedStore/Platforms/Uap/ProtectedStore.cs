using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace Excalibur.MvvmCross.Plugin.ProtectedStore.Platforms.Uap
{
    /// <inheritdoc />
    public class ProtectedStore : IProtectedStore
    {
        private const string Descriptor = "LOCAL=user";

        /// <inheritdoc />
        public async Task<string> GetStringForIdentifier(string identifier)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Create a DataProtectionProvider object.
                var provider = new DataProtectionProvider(Descriptor);

                foreach (var path in store.GetFileNames(GetStringsPath(identifier)))
                {
                    using (var stream = new BinaryReader(new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, store)))
                    {
                        var length = stream.ReadInt32();
                        var dataAsBytes = stream.ReadBytes(length);

                        // Decrypt the protected message specified on input.
                        var buffUnprotected = await provider.UnprotectAsync(dataAsBytes.AsBuffer());

                        // Execution of the SampleUnprotectData method resumes here
                        // after the awaited task (Provider.UnprotectAsync) completes
                        // Convert the unprotected message from an IBuffer object to a string.
                        var strClearText = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffUnprotected);

                        return strClearText;
                    }
                }

                return "";
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetStringsForIdentifier(string identifier)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Create a DataProtectionProvider object.
                var provider = new DataProtectionProvider(Descriptor);
                var returnList = new List<string>();

                foreach (var path in store.GetFileNames(GetStringsPath(identifier)))
                {
                    using (var stream = new BinaryReader(new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, store)))
                    {
                        var length = stream.ReadInt32();
                        var dataAsBytes = stream.ReadBytes(length);

                        // Decrypt the protected message specified on input.
                        var buffUnprotected = await provider.UnprotectAsync(dataAsBytes.AsBuffer());

                        // Execution of the SampleUnprotectData method resumes here
                        // after the awaited task (Provider.UnprotectAsync) completes
                        // Convert the unprotected message from an IBuffer object to a string.
                        returnList.Add(CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffUnprotected));
                    }
                }

                return returnList;
            }
        }

        /// <inheritdoc />
        public async Task Save(string identifier, string stringToSave)
        {
            var provider = new DataProtectionProvider(Descriptor);
            var messageBuffer = CryptographicBuffer.ConvertStringToBinary(stringToSave, BinaryStringEncoding.Utf8);
            var protectedBuffer = await provider.ProtectAsync(messageBuffer);

            var path = GetStringsPath(identifier);

            using (var storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, storageFile))
                {
                    stream.Write(BitConverter.GetBytes(protectedBuffer.Length), 0, sizeof(int));
                    stream.Write(protectedBuffer.ToArray(), 0, (int)protectedBuffer.Length);
                }
            }
        }

        /// <inheritdoc />
        public Task Delete(string identifier)
        {
            var path = GetStringsPath(identifier);
            using (var storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                storageFile.DeleteFile(path);
            }

            return Task.CompletedTask;
        }

        private static string GetStringsPath(string identifier) => string.Format("Excalibur-{0}", identifier);
    }
}
