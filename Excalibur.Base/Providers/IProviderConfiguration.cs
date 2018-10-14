using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excalibur.Base.Providers
{
    public interface IProviderConfiguration<T>
        where T : IProviderConfig
    {
        T Configuration { get; }
        void Configure();
    }

    public interface IProviderConfig
    {
    }

    public interface IDatabaseProvider<TId, T>
        where T : ProviderDomain<TId>
    {
        Task Insert(T item);
        Task InsertBulk(IEnumerable<T> items);

        Task<bool> Upsert(T item);
        Task<bool> Update(T item);
        Task Delete(Expression<Func<T, bool>> predicate);


        //todo implement FindFirst
        Task<IEnumerable<T>> FindAll();
        Task<T> FindById(TId id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
    }

    public abstract class ProviderDomain<TId>
    {
        private TId _id = default(TId);

        public TId Id
        {
            get => _id;
            set => _id = value;
        }

        // Todo add == operator
        // Todo add Equals operator
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }
    }

    public abstract class ProviderDomainOfInt : ProviderDomain<int>
    {
    }
}
