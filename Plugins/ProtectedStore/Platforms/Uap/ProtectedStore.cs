using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace Excalibur.MvvmCross.Plugin.ProtectedStore.Platforms.Uap
{
    /// <inheritdoc />
    public class ProtectedStore : IProtectedStore
    {
        /// <inheritdoc />
        public string GetStringForIdentifier(string identifier)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Create a DataProtectionProvider object.
                var provider = new DataProtectionProvider();

                foreach (var path in store.GetFileNames(GetStringsPath(identifier)))
                {
                    using (var stream = new BinaryReader(new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, store)))
                    {
                        var length = stream.ReadInt32();
                        var dataAsBytes = stream.ReadBytes(length);

                        // Decrypt the protected message specified on input.
                        var buffUnprotected = provider.UnprotectAsync(dataAsBytes.AsBuffer()).GetResults();

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
        public IEnumerable<string> GetStringsForIdentifier(string identifier)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Create a DataProtectionProvider object.
                var provider = new DataProtectionProvider();

                foreach (var path in store.GetFileNames(GetStringsPath(identifier)))
                {
                    using (var stream = new BinaryReader(new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, store)))
                    {
                        var length = stream.ReadInt32();
                        var dataAsBytes = stream.ReadBytes(length);

                        // Decrypt the protected message specified on input.
                        var buffUnprotected = provider.UnprotectAsync(dataAsBytes.AsBuffer()).GetResults();

                        // Execution of the SampleUnprotectData method resumes here
                        // after the awaited task (Provider.UnprotectAsync) completes
                        // Convert the unprotected message from an IBuffer object to a string.
                        var strClearText = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffUnprotected);

                        yield return strClearText;
                    }
                }
            }
        }

        /// <inheritdoc />
        public void Save(string stringToSave, string identifier)
        {
            var provider = new DataProtectionProvider(identifier);
            var messageBuffer = CryptographicBuffer.ConvertStringToBinary(stringToSave, BinaryStringEncoding.Utf8);
            var protectedBuffer = provider.ProtectAsync(messageBuffer).GetResults();

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
        public void Delete(string identifier)
        {
            var path = GetStringsPath(identifier);
            using (var storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                storageFile.DeleteFile(path);
            }
        }

        private static string GetStringsPath(string identifier) => string.Format("Excalibur-{0}", identifier);
    }
}
