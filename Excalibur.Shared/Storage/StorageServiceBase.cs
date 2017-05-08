﻿using System.Threading.Tasks;

namespace Excalibur.Shared.Storage
{
    public abstract class StorageServiceBase : IStorageService
    {
        public abstract Task<string> StoreAsync(string folder, string fullName, string contentAsString);
        public abstract Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes);
        public abstract Task<string> ReadAsTextAsync(string folder, string fullName);
        public abstract Task<byte[]> ReadAsBinaryAsync(string folder, string fullName);
        public abstract void DeleteFile(string folder, string fullName);
    }
}