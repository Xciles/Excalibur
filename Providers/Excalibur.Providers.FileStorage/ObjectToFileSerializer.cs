using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Base.Storage;
using MvvmCross;
using Newtonsoft.Json;

namespace Excalibur.Providers.FileStorage
{
    /// <summary>
    /// Class that provides custom file serializer settings that should be used when storing a file.
    /// </summary>
    public abstract class ObjectToFileSerializer
    {
        /// <summary>
        /// Method that should be implemented and will return the custom <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <returns>An instance of <see cref="JsonSerializerSettings"/></returns>
        public abstract JsonSerializerSettings JsonSerializerSettings();
    }

    public class FileStorageConfiguration : IProviderConfiguration<FileStorageConfig>
    {
        public FileStorageConfig Configuration { get; private set; }

        public FileStorageConfiguration(IProviderConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (!(config is FileStorageConfig liteConfig)) throw new ArgumentException("Please provide FileStorageConfig instance", nameof(config));

            Configuration = liteConfig;
        }

        public void Configure()
        {
            Mvx.IoCProvider.RegisterSingleton<IProviderConfiguration<FileStorageConfig>>(this);
        }
    }

    public class FileStorageConfig : IProviderConfig
    {
        public string DataFolder { get; set; } = "data";
        public string FileNamingFormat { get; set; } = "{0}.json";
    }

    public interface IFileStorageProvider<TId, T> : IDatabaseProvider<TId, T> 
        where T : ProviderDomain<TId>
    {
    }

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

        public async Task InsertBulk(IEnumerable<T> items)
        {
            // Delete the file before writing.
            // Sometimes write operation will fail when trying to write to a file that already exists
            _storageService.DeleteFile(DataFolder, String.Format(FileNamingFormat, typeof(T).Name));

            var objectAsString = JsonConvert.SerializeObject(items, JsonSerializerSettings());
            await _storageService.StoreAsync(DataFolder, String.Format(FileNamingFormat, typeof(T).Name), objectAsString).ConfigureAwait(false);
        }

        public async Task<bool> Upsert(T item)
        {
            await Insert(item);
            return true;
        }

        public async Task<bool> Update(T item)
        {
            await Insert(item);
            return true;
        }

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

        public async Task<IEnumerable<T>> FindAll()
        {
            var objectAsString = await _storageService.ReadAsTextAsync(DataFolder, String.Format(FileNamingFormat, typeof(T).Name)).ConfigureAwait(false) ?? String.Empty;

            return JsonConvert.DeserializeObject<IEnumerable<T>>(objectAsString, JsonSerializerSettings()) ?? Enumerable.Empty<T>();
        }

        public async Task<T> FindById(TId id)
        {
            // todo: Seek actual id from disk instead of searching the list
            var items = await FindAll().ConfigureAwait(false);
            return items.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = Int32.MaxValue)
        {
            var items = await FindAll().ConfigureAwait(false);
            return items.Where(predicate.Compile()).Skip(skip).Take(take);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            // todo: Seek actual id from disk instead of searching the list
            var items = await FindAll().ConfigureAwait(false);
            return items.FirstOrDefault(predicate.Compile());
        }

        public override JsonSerializerSettings JsonSerializerSettings()
        {
            return new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }
    }
}