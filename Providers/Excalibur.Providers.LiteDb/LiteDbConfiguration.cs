using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using LiteDB;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.LiteDb
{
    public class LiteDbConfiguration : IProviderConfiguration<LiteDbConfig>
    {
        public LiteDbConfig Configuration { get; private set; }

        public LiteDbConfiguration(IProviderConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (!(config is LiteDbConfig liteConfig)) throw new ArgumentException("Please provide LiteDbConfig instance", nameof(config));

            Configuration = liteConfig;
        }

        public void Configure()
        {
            var fileStore = Mvx.IoCProvider.Resolve<IMvxFileStore>();

            Configuration.ConnectionString = $"Filename={fileStore.NativePath(Configuration.FileName)};{Configuration.Options}";

            Mvx.IoCProvider.RegisterSingleton<IProviderConfiguration<LiteDbConfig>>(this);
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ILiteDbInstance, LiteDbInstance>();
        }
    }

    public class LiteDbConfig : IProviderConfig
    {
        public string FileName { get; set; }
        public string Options { get; set; }

        internal string ConnectionString { get; set; }
    }

    public interface ILiteDbProvider<TId, T> : IDatabaseProvider<TId, T> 
        where T : ProviderDomain<TId>
    {
        bool EnsureIndex<TK>(Expression<Func<T, TK>> property, string expression, bool unique = false);
        bool EnsureIndex<TK>(Expression<Func<T, TK>> property, bool unique = false);

        bool DropIndex(string field);

        // EnsureIndex
        // DropIndex
    }

    public interface ILiteDbInstance
    {
        LiteDatabase LiteDatabase { get; }
    }

    public class LiteDbInstance : ILiteDbInstance, IDisposable
    {
        private LiteDbConfig _providerConfig;
        public LiteDatabase LiteDatabase { get; }

        public LiteDbInstance(IProviderConfiguration<LiteDbConfig> providerConfiguration)
        {
            _providerConfig = providerConfiguration.Configuration;
            LiteDatabase = new LiteDatabase(_providerConfig.ConnectionString);
        }

        ~LiteDbInstance()
        {
            Dispose(false);
        }

        private void ReleaseUnmanagedResources()
        {
            LiteDatabase.Dispose();
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                LiteDatabase?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class LiteDbProvider<TId, T> : ILiteDbProvider<TId, T> 
        where T : ProviderDomain<TId>
    {
        // Insert
        // BulkInsert
        // Update
        // Upsert
        // Delete

        // Find
        // FindOne

        // EnsureIndex
        // DropIndex

        // DeleteDatabase

        // Provider > LiteProvider > LiteImplementation
        // Makes sure the the base provider can be used as well as custom providers per implementation (and in businessess/presentation w/e)
        private readonly ILiteDbInstance _liteDbInstance;

        public LiteDbProvider(ILiteDbInstance liteDbInstance)
        {
            _liteDbInstance = liteDbInstance;
        }

        public virtual Task Insert(T item)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            collection.Insert(item);

            EnsureIndexOnInsert();

            return Task.FromResult(0);
        }

        public virtual Task InsertBulk(IEnumerable<T> items)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();

            foreach (var item in items)
            {
                collection.Upsert(item);
            }

            EnsureIndexOnInsert();

            return Task.FromResult(0);
        }

        public virtual Task<bool> Upsert(T item)
        {
            bool result;
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            result = collection.Upsert(item);

            EnsureIndexOnInsert();

            return Task.FromResult(result);
        }

        public virtual Task<bool> Update(T item)
        {
            bool result;
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            result = collection.Update(item);

            EnsureIndexOnUpdate();

            return Task.FromResult(result);
        }

        public virtual Task Delete(Expression<Func<T, bool>> predicate)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            collection.Delete(predicate);

            return Task.FromResult(0);
        }

        public Task<IEnumerable<T>> FindAll()
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindAll());
        }

        public Task<T> FindById(TId id)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindOne(x => x.Id.Equals(id)));
        }

        public virtual Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.Find(predicate, skip, take));
        }

        public virtual Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindOne(predicate));
        }

        public virtual bool EnsureIndex<TK>(Expression<Func<T, TK>> property, string expression, bool unique = false)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return collection.EnsureIndex(property, expression, unique);
        }

        public virtual bool EnsureIndex<TK>(Expression<Func<T, TK>> property, bool unique = false)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return collection.EnsureIndex(property, unique);
        }

        public virtual bool DropIndex(string field)
        {
            var collection = _liteDbInstance.LiteDatabase.GetCollection<T>();
            return collection.DropIndex(field);
        }

        public virtual void EnsureIndexOnInsert() { }

        public virtual void EnsureIndexOnUpdate() { }
    }
}
