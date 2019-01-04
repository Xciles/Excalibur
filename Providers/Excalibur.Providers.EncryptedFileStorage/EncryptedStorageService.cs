using System;
using System.Threading.Tasks;
using Excalibur.Base.Storage;
using Excalibur.Providers.Encryption;
using Excalibur.Providers.FileStorage;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.EncryptedFileStorage
{
    /// <summary>
    /// MvvmCross implementation of the <see cref="IStorageService"/>, however this one is encrypted!
    /// The encrypted storage service makes use of the ProtectedStore via <see cref="IEncryptedProviderConfig"/> to make encryption keys and salts
    ///     and the <see cref="IExCrypto"/> to actually encrypt and decrypt.
    /// It will require that the <see cref="IEncryptedProviderConfig"/> to be initialized. If it hasn't, this will throw an exception to make sure.
    /// </summary>
    public class EncryptedStorageService : ExStorageService
    {
        private const string ErrorString = "Did you forget to initialize the IEncryptedProviderConfig?";
        private readonly IEncryptedProviderConfig _config;
        private readonly IExCrypto _exCrypto;

        public EncryptedStorageService(IMvxFileStore fileStore, IEncryptedProviderConfig config, IMvxFileStoreAsync fileStoreAsync, IExCrypto exCrypto) : base(fileStore, fileStoreAsync)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config), ErrorString);
            _exCrypto = exCrypto;
        }

        /// <inheritdoc />
        public override async Task<string> Store(string folder, string fullName, string contentAsString)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            var encryptedBytes = _exCrypto.Encrypt(contentAsString, await _config.Key().ConfigureAwait(false), await _config.Salt().ConfigureAwait(false));
            return await base.Store(folder, fullName, encryptedBytes).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override async Task<string> Store(string folder, string fullName, byte[] contentAsBytes)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            var encryptedBytes = _exCrypto.EncryptFromBytes(contentAsBytes, await _config.Key().ConfigureAwait(false), await _config.Salt().ConfigureAwait(false));
            return await base.Store(folder, fullName, encryptedBytes).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override async Task<string> ReadAsText(string folder, string fullName)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            if (Exists(folder, fullName))
            {
                var encryptedBytes = await base.ReadAsBinary(folder, fullName).ConfigureAwait(false);
                return _exCrypto.Decrypt(encryptedBytes, await _config.Key().ConfigureAwait(false), await _config.Salt().ConfigureAwait(false));
            }

            return string.Empty;
        }

        /// <inheritdoc />
        public override async Task<byte[]> ReadAsBinary(string folder, string fullName)
        {
            if (!_config.HasBeenInitialized) throw new ArgumentException(ErrorString);

            if (Exists(folder, fullName))
            {
                var encryptedBytes = await base.ReadAsBinary(folder, fullName).ConfigureAwait(false);
                return _exCrypto.DecryptToBytes(encryptedBytes, await _config.Key().ConfigureAwait(false), await _config.Salt().ConfigureAwait(false));
            }

            return new byte[] { };
        }
    }
}