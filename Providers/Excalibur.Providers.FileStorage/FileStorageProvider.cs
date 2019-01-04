using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Base.Storage;
using Newtonsoft.Json;

namespace Excalibur.Providers.FileStorage
{
    /// <summary>
    /// A provider that uses file storage as the underlying database.
    /// This class will use TId as a type identifier for objects
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    public class FileStorageProvider<TId, T> : ObjectToFileSerializer, IFileStorageProvider<TId, T>
        where T : ProviderDomain<TId>
    {
        private string DataFolder => _providerConfig.DataFolder;
        private string FileNamingFormat => _providerConfig.FileNamingFormat;

        private readonly IStorageService _storageService;
        private readonly FileStorageConfig _providerConfig;

        public FileStorageProvider(IStorageService storageService, IProviderConfiguration<FileStorageConfig> providerConfiguration)
        {
            _storageService = storageService;
            _providerConfig = providerConfiguration.Configuration;
        }

        /// <inheritdoc />
        public async Task Insert(T item)
        {
            // todo: Seek actual id from disk instead of searching the list
            // todo: Actual add/update at correct index and move the bytes
            var items = (await FindAll().ConfigureAwait(false)).ToList();
            var result = items.FirstOrDefault(x => x.Id.Equals(item.Id));

            if (result != null)
            {
                items[items.IndexOf(result)] = item;
            }
            else
            {
                items.Add(item);
            }

            await InsertBulk(items).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task InsertBulk(IEnumerable<T> items)
        {
            // Delete the file before writing.
            // Sometimes write operation will fail when trying to write to a file that already exists
            _storageService.DeleteFile(DataFolder, string.Format(FileNamingFormat, typeof(T).Name));

            var objectAsString = JsonConvert.SerializeObject(items, JsonSerializerSettings());
            await _storageService.Store(DataFolder, string.Format(FileNamingFormat, typeof(T).Name), objectAsString).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Upsert(T item)
        {
            await Insert(item).ConfigureAwait(false);
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> Update(T item)
        {
            await Insert(item).ConfigureAwait(false);
            return true;
        }

        /// <inheritdoc />
        public async Task Delete(Expression<Func<T, bool>> predicate)
        {
            // todo: Seek actual id from disk instead of searching the list
            // todo: Actual delete at correct index and moving the bytes
            var items = (await FindAll().ConfigureAwait(false)).ToList();
            var item = items.FirstOrDefault(predicate.Compile());

            if (item != null)
            {
                items.Remove(item);
                await InsertBulk(items).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> FindAll()
        {
            var objectAsString = await _storageService.ReadAsText(DataFolder, string.Format(FileNamingFormat, typeof(T).Name)).ConfigureAwait(false) ?? String.Empty;

            return JsonConvert.DeserializeObject<IEnumerable<T>>(objectAsString, JsonSerializerSettings()) ?? Enumerable.Empty<T>();
        }

        /// <inheritdoc />
        public async Task<T> FindById(TId id)
        {
            // todo: Seek actual id from disk instead of searching the list
            var items = await FindAll().ConfigureAwait(false);
            return items.FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue)
        {
            var items = await FindAll().ConfigureAwait(false);
            return items.Where(predicate.Compile()).Skip(skip).Take(take);
        }

        /// <inheritdoc />
        public async Task<T> FirstOrDefault()
        {
            var items = await FindAll().ConfigureAwait(false);
            return items.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            // todo: Seek actual id from disk instead of searching the list
            var items = await FindAll().ConfigureAwait(false);
            return items.FirstOrDefault(predicate.Compile());
        }

        /// <inheritdoc />
        public Task Clear()
        {
            _storageService.DeleteFile(DataFolder, string.Format(FileNamingFormat, typeof(T).Name));

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override JsonSerializerSettings JsonSerializerSettings() => new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
    }
}