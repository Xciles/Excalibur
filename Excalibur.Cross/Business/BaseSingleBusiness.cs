using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Cross.Services;

namespace Excalibur.Cross.Business
{
    /// <inheritdoc />
    public class BaseSingleBusiness<TId, TDomain> : BaseSingleBusiness<TId, TDomain, IServiceBase<TDomain>>
        where TDomain : ProviderDomain<TId>, new()
    {
        public BaseSingleBusiness(IServiceBase<TDomain> service, IDatabaseProvider<TId, TDomain> storageProvider) : base(service, storageProvider)
        {
        }
    }

    /// <summary>
    /// Base implementation that manages a single object or domain object. 
    /// This class will manage storage, service communication etc. 
    /// 
    /// Also see <see cref="BusinessBase{TId,TDomain,TService}"/>
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TService">The type of the service that should be used for communications</typeparam>
    public class BaseSingleBusiness<TId, TDomain, TService> : BusinessBase<TId, TDomain, TService>, ISingleBusiness<TDomain>
        where TDomain : ProviderDomain<TId>, new()
        where TService : class, IServiceBase<TDomain>
    {
        public BaseSingleBusiness(TService service, IDatabaseProvider<TId, TDomain> storageProvider) : base(service, storageProvider)
        {
        }

        /// <summary>
        /// Updates the domain object from service using <see cref="BusinessBase{TId,TDomain,TService}.Service"/>
        /// </summary>
        public override async Task UpdateFromService()
        {
            var result = await Service.SyncData().ConfigureAwait(false) ?? new TDomain();

            if (DeleteNotReturnedItems)
            {
                await Delete();
            }

            await AfterServiceSyncData();

            await StoreItem(result).ConfigureAwait(false);

            PublishUpdated(result);
        }

        /// <inheritdoc />
        /// <summary>
        /// Publish a message to subscribers that contains the object managed by this entity. 
        /// Publish state will be <see cref="F:Excalibur.Cross.Business.EDomainState.Updated" />
        /// </summary>
        public override async Task PublishFromStorage() => PublishUpdated(await FirstOrDefault().ConfigureAwait(false));

        /// <inheritdoc />
        public virtual async Task<TDomain> FirstOrDefault() => await Storage.FirstOrDefault().ConfigureAwait(false);

        /// <inheritdoc />
        public async Task Delete()
        {
            var itemToDelete = await FirstOrDefault().ConfigureAwait(false);

            if (itemToDelete != null)
            {
                await Storage.Delete(x => x.Id.Equals(itemToDelete.Id)).ConfigureAwait(false);

                PublishUpdated(itemToDelete, EDomainState.Deleted);
            }
        }
    }
}