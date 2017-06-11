using System;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.File;

namespace Excalibur.Cross.Storage
{
    public class ExStorageService : StorageServiceBase, IStorageService
    {
        private readonly IMvxFileStore _fileStore;
        private readonly IMvxFileStoreAsync _fileStoreAsync;

        public ExStorageService()
        {
            _fileStore = Mvx.Resolve<IMvxFileStore>();
            _fileStoreAsync = Mvx.Resolve<IMvxFileStoreAsync>();
        }

        public override async Task<string> StoreAsync(string folder, string fullName, string contentAsString)
        {
            var fullPath = FileChecks(folder, fullName);

            await _fileStoreAsync.WriteFileAsync(fullPath, contentAsString).ConfigureAwait(false);
            return _fileStore.NativePath(fullPath);
        }

        public override async Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes)
        {
            var fullPath = FileChecks(folder, fullName);

            await _fileStoreAsync.WriteFileAsync(fullPath, contentAsBytes).ConfigureAwait(false);
            return _fileStore.NativePath(fullPath);
        }

        public override async Task<string> ReadAsTextAsync(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            var result = await _fileStoreAsync.TryReadTextFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        public override async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            var result = await _fileStoreAsync.TryReadBinaryFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        public override void DeleteFile(string folder, string fullName)
        {
            try
            {
                _fileStore.DeleteFile(_fileStore.PathCombine(folder, fullName));
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxTrace>().Trace(MvxTraceLevel.Error, "ExStorageService.DeleteFile", ex.Message + " - " + ex.StackTrace);
            }
        }

        public override bool Exists(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            return _fileStore.Exists(fullPath);
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
