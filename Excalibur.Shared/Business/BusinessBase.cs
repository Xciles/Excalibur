using System.Collections.Generic;
using System.Threading.Tasks;
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
        public abstract Task PublishFromStorageAsync();
    }
}