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
        void Configure(IProviderConfig config);
    }

    public interface IProviderConfig
    {
    }

    public interface IDatabaseProvider<T>
    {
        Task Insert(T item);
        Task InsertBulk(IEnumerable<T> items);

        Task<bool> Upsert(T item);
        Task<bool> Update(T item);
        Task Delete(Expression<Func<T, bool>> predicate);


        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, int skip = 0, int take = int.MaxValue);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
