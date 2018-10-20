using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Java.Security;
using Javax.Crypto;

namespace Excalibur.MvvmCross.Plugin.ProtectedStore.Platforms.Android
{
    public class ProtectedStore : IProtectedStore
    {
        private readonly Context _context;
        private readonly KeyStore _keyStore;
        private readonly KeyStore.PasswordProtection _passwordProtection;

        private static readonly object FileLock = new object();
        private static char[] _password;
        private static string _fileName = "Excalibur.Store";

        public static void Init(string password, string fileName = "Excalibur.Store")
        {
            _password = password.ToCharArray();
            _fileName = fileName;
        }

        public ProtectedStore()
        {
            if (_password == null)
            {
                throw new ArgumentNullException(nameof(_password), "Please call ProtectedStore.Init(<password>, <fileName?>) first.");
            }

            _context = global::Android.App.Application.Context;
            _keyStore = KeyStore.GetInstance(KeyStore.DefaultType);
            _passwordProtection = new KeyStore.PasswordProtection(_password);

            try
            {
                lock (FileLock)
                {
                    using (var stream = _context.OpenFileInput(_fileName))
                    {
                        _keyStore.Load(stream, _password);
                    }
                }
            }
            catch (Java.IO.FileNotFoundException)
            {
                _keyStore.Load(null, _password);
            }
        }

        public IEnumerable<string> GetStringsForIdentifier(string identifier)
        {
            var returnList = new List<string>();

            var postfix = MakeAlias(identifier);

            var aliases = _keyStore.Aliases();
            while (aliases.HasMoreElements)
            {
                var alias = aliases.NextElement().ToString();
                if (!alias.EndsWith(postfix)) continue;

                if (_keyStore.GetEntry(alias, _passwordProtection) is KeyStore.SecretKeyEntry entry)
                {
                    var bytes = entry.SecretKey.GetEncoded();
                    var password = Encoding.UTF8.GetString(bytes);
                    returnList.Add(password);
                }
            }

            return returnList;
        }

        public void Delete(string identifier)
        {
            var alias = MakeAlias(identifier);

            _keyStore.DeleteEntry(alias);
            Save();
        }

        public void Save(string stringToSave, string identifier)
        {
            var alias = MakeAlias(identifier);

            var secretKey = new SecretString(stringToSave);
            var entry = new KeyStore.SecretKeyEntry(secretKey);
            _keyStore.SetEntry(alias, entry, _passwordProtection);

            Save();
        }

        private void Save()
        {
            lock (FileLock)
            {
                using (var stream = _context.OpenFileOutput(_fileName, FileCreationMode.Private))
                {
                    _keyStore.Store(stream, _password);
                }
            }
        }

        private static string MakeAlias(string serviceId) => "Excalibur-" + serviceId;

        private class SecretString : Java.Lang.Object, ISecretKey
        {
            private readonly byte[] _bytes;

            public SecretString(string stringToSave)
            {
                _bytes = Encoding.UTF8.GetBytes(stringToSave);
            }

            public byte[] GetEncoded() => _bytes;
            public string Algorithm => "RAW";
            public string Format => "RAW";
        }
    }
}
