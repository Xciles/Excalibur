using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    public class BaseListBusiness<TId, TDomain> : BaseListBusiness<TId, TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : StorageDomain<TId>, new()
    {
    }

    public class BaseListBusiness<TId, TDomain, TService> : BusinessBase<TId, TDomain, TService>, IListBusiness<TId, TDomain>
        where TDomain : StorageDomain<TId>, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
        public virtual async Task<IList<TDomain>> GetAllAsync()
        {
            return await Storage.GetRangeAsync().ConfigureAwait(false);
        }

        public virtual async Task<TDomain> GetByIdAsync(TId id)
        {
            return await Storage.GetAsync(id).ConfigureAwait(false);
        }

        public override async Task UpdateFromServiceAsync()
        {
            var result = await Service.SyncDataAsync().ConfigureAwait(false) ?? new List<TDomain>();

            await StoreItemsAsync(result).ConfigureAwait(false);

            PublishListUpdated();
        }

        public override async Task PublishFromStorageAsync()
        {
            // todo Add initial range when loading from storage
            PublishListUpdated();
        }

        protected async Task StoreItemsAsync(IList<TDomain> objectsToStore)
        {
            await Storage.StoreRangeAsync(objectsToStore).ConfigureAwait(false);
        }

        public async Task DeleteItemAsync(TId id)
        {
            await Storage.DeleteAsync(id).ConfigureAwait(false);

            PublishListUpdated();
        }
    }
}