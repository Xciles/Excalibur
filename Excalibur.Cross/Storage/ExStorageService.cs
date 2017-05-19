using System;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.File;

namespace Excalibur.Cross.Storage
{
    public class ExStorageService : IStorageService
    {
        private readonly IMvxFileStore _fileStore;
        private readonly IMvxFileStoreAsync _fileStoreAsync;

        public ExStorageService()
        {
            _fileStore = Mvx.Resolve<IMvxFileStore>();
            _fileStoreAsync = Mvx.Resolve<IMvxFileStoreAsync>();
        }

        public async Task<string> StoreAsync(string folder, string fullName, string contentAsString)
        {
            var fullPath = FileChecks(folder, fullName);

            await _fileStoreAsync.WriteFileAsync(fullPath, contentAsString).ConfigureAwait(false);
            return _fileStore.NativePath(fullPath);
        }

        public async Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes)
        {
            var fullPath = FileChecks(folder, fullName);

            await _fileStoreAsync.WriteFileAsync(fullPath, contentAsBytes).ConfigureAwait(false);
            return _fileStore.NativePath(fullPath);
        }

        public async Task<string> ReadAsTextAsync(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            var result = await _fileStoreAsync.TryReadTextFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        public async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            var result = await _fileStoreAsync.TryReadBinaryFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        public void DeleteFile(string folder, string fullName)
        {
            var filestore = Mvx.Resolve<IMvxFileStore>();

            try
            {
                filestore.DeleteFile(filestore.PathCombine(folder, fullName));
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxTrace>().Trace(MvxTraceLevel.Error, "ExStorageService.DeleteFile", ex.Message + " - " + ex.StackTrace);
            }
        }

        private string FileChecks(string folder, string fileFullName)
        {
            try
            {
                _fileStore.EnsureFolderExists(folder);
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxTrace>().Trace(MvxTraceLevel.Error, "ExStorageService.FileChecks", ex.Message + " - " + ex.StackTrace);
            }

            var fullPath = _fileStore.PathCombine(folder, fileFullName);

            if (_fileStore.Exists(fullPath))
            {
                DeleteFile(folder, fileFullName);
            }
            return fullPath;
        }
    }
}
