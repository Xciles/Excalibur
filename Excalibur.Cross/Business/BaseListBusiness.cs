using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Cross.Services;

namespace Excalibur.Cross.Business
{
    /// <inheritdoc />
    public class BaseListBusiness<TId, TDomain> : BaseListBusiness<TId, TDomain, IServiceBase<IList<TDomain>>>
        where TDomain : ProviderDomain<TId>, new()
    {
        public BaseListBusiness(IServiceBase<IList<TDomain>> service, IDatabaseProvider<TId, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <summary>
    /// Base implementation of a business entity that manages multiple objects or domain objects.
    /// This class will manage storage, service communication etc. 
    /// 
    /// Also see <see cref="BusinessBase{TId,TDomain,TService}"/>
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TService">The type of the service that should be used for communications</typeparam>
    public class BaseListBusiness<TId, TDomain, TService> : BusinessBase<TId, TDomain, TService>, IListBusiness<TId, TDomain>
        where TDomain : ProviderDomain<TId>, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
        public BaseListBusiness(TService service, IDatabaseProvider<TId, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
        
        /// <inheritdoc />
        public virtual async Task<IEnumerable<TDomain>> FindAll() => await Storage.FindAll().ConfigureAwait(false);

        /// <inheritdoc />
        public virtual async Task<TDomain> FindById(TId id) => await Storage.FindById(id).ConfigureAwait(false);

        /// <inheritdoc />
        public virtual async Task<TDomain> FirstOrDefault(Expression<Func<TDomain, bool>> predicate) => await Storage.FirstOrDefault(predicate).ConfigureAwait(false);

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TDomain>> Find(Expression<Func<TDomain, bool>> predicate, int skip = 0, int take = int.MaxValue)
        {
            return await Storage.Find(predicate, skip, take).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override async Task UpdateFromService()
        {
            var result = await Service.SyncData().ConfigureAwait(false) ?? new List<TDomain>();

            if (DeleteNotReturnedItems)
            {
                await Storage.Delete(gr => result.All(y => !gr.Id.Equals(y.Id)));
            }

            await AfterServiceSyncData();

            await StoreItems(result).ConfigureAwait(false);

            PublishListUpdated();
        }

        /// <summary>
        /// Publish a message to subscribers that do not contain the actual objects. They have to be retrieved separately. 
        /// Note: Might add an initial range when loading the first time
        /// </summary>
        public override Task PublishFromStorage()
        {
            // todo Add initial range when loading from storage
            PublishListUpdated();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Stores incoming objectsToStore using a provider (FileStorage, LiteDb etc). 
        /// This stores all entities.
        /// </summary>
        /// <param name="objectsToStore">The objects to store</param>
        protected async Task StoreItems(IList<TDomain> objectsToStore) => await Storage.InsertBulk(objectsToStore).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task DeleteItem(TId id)
        {
            var itemToDelete = await FirstOrDefault(x => x.Id.Equals(id)).ConfigureAwait(false);

            if (itemToDelete != null)
            {
                await Storage.Delete(x => x.Id.Equals(id)).ConfigureAwait(false);

                PublishUpdated(itemToDelete, EDomainState.Deleted);
            }
        }
    }
}