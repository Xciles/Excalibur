using System.Linq;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    public class BaseSingleBusiness<TId, TDomain> : BaseSingleBusiness<TId, TDomain, IServiceBase<TDomain>>
        where TDomain : StorageDomain<TId>, new()
    {
    }

    public class BaseSingleBusiness<TId, TDomain, TService> : BusinessBase<TId, TDomain, TService>, ISingleBusiness<TDomain>
        where TDomain : StorageDomain<TId>, new()
        where TService : class, IServiceBase<TDomain>
    {
        public override async Task UpdateFromServiceAsync()
        {
            var result = await Service.SyncDataAsync().ConfigureAwait(false) ?? new TDomain();

            await StoreItemAsync(result).ConfigureAwait(false);

            PublishUpdated(result);
        }

        public virtual async Task<TDomain> GetAsync()
        {
            var list = await Storage.GetRange().ConfigureAwait(false);
            return list.FirstOrDefault();
        }

        public async Task DeleteAsync()
        {
            var itemToDelete = await GetAsync().ConfigureAwait(false);

            if (itemToDelete != null)
            {
                await Storage.Delete(itemToDelete.Id).ConfigureAwait(false);

                PublishUpdated(itemToDelete, EDomainState.Deleted);
            }
        }
    }
}