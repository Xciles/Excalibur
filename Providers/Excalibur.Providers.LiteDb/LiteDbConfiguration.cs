using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using LiteDB;

namespace Excalibur.Providers.LiteDb
{
    public class LiteDbConfiguration : IProviderConfiguration<LiteDbConfig>
    {
        public LiteDbConfig Configuration { get; private set; }

        public void Configure(IProviderConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (!(config is LiteDbConfig liteConfig)) throw new ArgumentException("Please provide LiteDbConfig instance", nameof(config));

            Configuration = liteConfig;
        }
    }

    public class LiteDbConfig : IProviderConfig
    {
        public string ConnectionString { get; set; }
    }

    public interface ILiteDbProvider<T> : IDatabaseProvider<T>
    {
        bool EnsureIndex<TK>(Expression<Func<T, TK>> property, string expression, bool unique = false);
        bool EnsureIndex<TK>(Expression<Func<T, TK>> property, bool unique = false);

        bool DropIndex(string field);

        // EnsureIndex
        // DropIndex
    }

    public class LiteDbProvider<T> : ILiteDbProvider<T>
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
        private readonly LiteDbConfig _providerConfig;

        public LiteDbProvider(IProviderConfiguration<LiteDbConfig> providerConfiguration)
        {
            _providerConfig = providerConfiguration.Configuration;
        }

        public virtual Task Insert(T item)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                collection.Insert(item);

                EnsureIndexOnInsert();
            }

            return Task.FromResult(0);
        }

        public virtual Task InsertBulk(IEnumerable<T> items)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                collection.InsertBulk(items);

                EnsureIndexOnInsert();
            }

            return Task.FromResult(0);
        }

        public virtual Task<bool> Upsert(T item)
        {
            bool result;
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                result = collection.Upsert(item);

                EnsureIndexOnInsert();
            }

            return Task.FromResult(result);
        }

        public virtual Task<bool> Update(T item)
        {
            bool result;
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                result = collection.Update(item);

                EnsureIndexOnUpdate();
            }

            return Task.FromResult(result);
        }

        public virtual Task Delete(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                collection.Delete(predicate);
            }

            return Task.FromResult(0);
        }

        public virtual Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                return Task.FromResult(collection.Find(predicate, skip, take));
            }
        }

        public virtual Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                return Task.FromResult(collection.FindOne(predicate));
            }
        }

        public virtual bool EnsureIndex<TK>(Expression<Func<T, TK>> property, string expression, bool unique = false)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                return collection.EnsureIndex(property, expression, unique);
            }
        }

        public virtual bool EnsureIndex<TK>(Expression<Func<T, TK>> property, bool unique = false)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                return collection.EnsureIndex(property, unique);
            }
        }

        public virtual bool DropIndex(string field)
        {
            using (var db = new LiteDatabase(_providerConfig.ConnectionString))
            {
                var collection = db.GetCollection<T>();
                return collection.DropIndex(field);
            }
        }

        public virtual void EnsureIndexOnInsert() { }

        public virtual void EnsureIndexOnUpdate() { }
    }
}
