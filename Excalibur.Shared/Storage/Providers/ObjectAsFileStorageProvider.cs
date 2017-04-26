using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Excalibur.Shared.Storage.Providers
{
    public class ObjectAsFileStorageProvider<T, TId> : IObjectStorageProvider<T, TId>
        where T : StorageDomain<TId>
    {
        private const string DataFolder = "data";
        private const string FileName = "{0}.json";

        private readonly IStorageService _storageService;

        public ObjectAsFileStorageProvider(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task StoreRange(IList<T> objectsToStore)
        {
            // Delete the file before writing.
            // Sometimes write operation will fail when trying to write to a file that already exisits
            _storageService.DeleteFile(DataFolder, String.Format(FileName, typeof(T).Name));

            var objectAsString = JsonConvert.SerializeObject(objectsToStore, JsonSerializerSettings());
            await _storageService.StoreAsync(DataFolder, String.Format(FileName, typeof(T).Name), objectAsString).ConfigureAwait(false);
        }

        public async Task<IList<T>> GetRange()
        {
            var objectAsString = await _storageService.ReadAsTextAsync(DataFolder, String.Format(FileName, typeof(T).Name)).ConfigureAwait(false) ?? String.Empty;

            var result = JsonConvert.DeserializeObject<IList<T>>(objectAsString, JsonSerializerSettings()) ?? new List<T>();

            return result;
        }

        public async Task<T> Get(TId id)
        {
            // todo: Seek actual id from disk instead of searching the list
            var items = await GetRange();
            return items.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<bool> AddOrUpdate(T objectToStore)
        {
            // todo: Seek actual id from disk instead of searching the list
            // todo: Actual add/update at correct index and move the bytes
            var items = await GetRange();
            var item = items.FirstOrDefault(x => x.Id.Equals(objectToStore.Id));

            if (item != null)
            {
                items[items.IndexOf(item)] = objectToStore;
            }
            else
            {
                items.Add(objectToStore);
            }

            await StoreRange(items);

            return true;
        }

        public async Task<bool> Delete(TId id)
        {
            // todo: Seek actual id from disk instead of searching the list
            // todo: Actual delete at correct index and moving the bytes
            var items = await GetRange();
            var item = items.FirstOrDefault(x => x.Id.Equals(id));

            if (item != null)
            {
                items.Remove(item);
                await StoreRange(items);
                return true;
            }
            return false;
        }

        private static JsonSerializerSettings JsonSerializerSettings()
        {
            return new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }
    }
}