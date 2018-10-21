using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using LiteDB;

namespace Excalibur.Providers.LiteDb
{
    /// <inheritdoc />
    public class LiteDbProvider<TId, T> : ILiteDbProvider<TId, T> 
        where T : ProviderDomain<TId>
    {
        protected ILiteDbInstance LiteDbInstance { get; private set; }

        public LiteDbProvider(ILiteDbInstance liteDbInstance)
        {
            LiteDbInstance = liteDbInstance;
        }

        /// <inheritdoc />
        public virtual Task Insert(T item)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            collection.Insert(item);

            EnsureIndexOnInsert(collection);

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public virtual Task InsertBulk(IEnumerable<T> items)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();

            foreach (var item in items)
            {
                collection.Upsert(item);
            }

            EnsureIndexOnInsert(collection);

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public virtual Task<bool> Upsert(T item)
        {
            bool result;
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            result = collection.Upsert(item);

            EnsureIndexOnInsert(collection);

            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public virtual Task<bool> Update(T item)
        {
            bool result;
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            result = collection.Update(item);

            EnsureIndexOnUpdate(collection);

            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public virtual Task Delete(Expression<Func<T, bool>> predicate)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            collection.Delete(predicate);

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task<IEnumerable<T>> FindAll()
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindAll());
        }

        /// <inheritdoc />
        public Task<T> FindById(TId id)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindOne(x => x.Id.Equals(id)));
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.Find(predicate, skip, take));
        }

        /// <inheritdoc />
        public virtual Task<T> FirstOrDefault()
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindAll().FirstOrDefault());
        }

        /// <inheritdoc />
        public virtual Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return Task.FromResult(collection.FindOne(predicate));
        }

        /// <inheritdoc />
        public virtual bool EnsureIndex<TK>(Expression<Func<T, TK>> property, string expression, bool unique = false)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return collection.EnsureIndex(property, expression, unique);
        }

        /// <inheritdoc />
        public virtual bool EnsureIndex<TK>(Expression<Func<T, TK>> property, bool unique = false)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return collection.EnsureIndex(property, unique);
        }

        /// <inheritdoc />
        public virtual bool DropIndex(string field)
        {
            var collection = LiteDbInstance.LiteDatabase.GetCollection<T>();
            return collection.DropIndex(field);
        }

        /// <summary>
        /// This method is called after every insert.
        /// You can provide your own custom EnsureIndexes here.
        /// </summary>
        /// <param name="collection">The collection that was used to insert the item(s) in.</param>
        public virtual void EnsureIndexOnInsert(LiteCollection<T> collection) { }

        /// <summary>
        /// This method is called after every update.
        /// You can provide your own custom EnsureIndexes here.
        /// </summary>
        /// <param name="collection">The collection that was used to update the item(s) in.</param>
        public virtual void EnsureIndexOnUpdate(LiteCollection<T> collection) { }
    }
}