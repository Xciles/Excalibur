using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;
using Excalibur.Shared.Storage.Providers;
using PubSub;
using XLabs.Ioc;
using Excalibur.Utils;

namespace Excalibur.Shared.Business
{
    public abstract class BusinessBase<TId, TDomain, TService> : IBusiness
        where TDomain : StorageDomain<TId>, new()
        where TService : class
    {
        protected TService Service { get; set; }
        protected IObjectStorageProvider<TId, TDomain> Storage { get; set; }

        protected BusinessBase()
        {
            Service = Resolver.Resolve<TService>();
            Storage = Resolver.Resolve<IObjectStorageProvider<TId, TDomain>>();
        }

        protected void PublishListUpdated()
        {
            this.Publish<MessageBase<IList<TDomain>>>();
        }

        protected void PublishUpdated(TDomain updatedObject, EDomainState state = EDomainState.Updated)
        {
            this.Publish<MessageBase<TDomain>>(new MessageBase<TDomain>(updatedObject));
        }

        protected async Task StoreItemAsync(TDomain objectToStore)
        {
            await Storage.AddOrUpdate(objectToStore).ConfigureAwait(false);

            PublishListUpdated();
        }

        public abstract Task UpdateFromServiceAsync();
    }


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
            return await Storage.GetRange().ConfigureAwait(false);
        }

        public virtual async Task<TDomain> GetByIdAsync(TId id)
        {
            return await Storage.Get(id).ConfigureAwait(false);
        }

        public override async Task UpdateFromServiceAsync()
        {
            var result = await Service.SyncDataAsync().ConfigureAwait(false) ?? new List<TDomain>();

            await StoreItemsAsync(result).ConfigureAwait(false);

            PublishListUpdated();
        }

        protected async Task StoreItemsAsync(IList<TDomain> objectsToStore)
        {
            await Storage.StoreRange(objectsToStore).ConfigureAwait(false);
        }

        public async Task DeleteItemAsync(TId id)
        {
            await Storage.Delete(id).ConfigureAwait(false);

            PublishListUpdated();
        }
    }





    // Examples

    public class Participant : BaseListBusiness<int, ParticipantDomain, IServiceBase<IList<ParticipantDomain>>>
    {

    }


    public class ParticipantDomain : StorageDomain
    {

    }

    public class Friend : BaseListBusiness<int, FriendDomain, IFriendService>
    {

    }


    public class FriendDomain : StorageDomain
    {

    }

    public interface IFriendService : IServiceBase<IList<FriendDomain>>
    {

    }

    public class Bla : IObjectStorageProvider<int, FriendDomain>
    {
        public Task StoreRange(IList<FriendDomain> objectsToStore)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<FriendDomain>> GetRange()
        {
            throw new System.NotImplementedException();
        }

        public Task<FriendDomain> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddOrUpdate(FriendDomain objectToStore)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}