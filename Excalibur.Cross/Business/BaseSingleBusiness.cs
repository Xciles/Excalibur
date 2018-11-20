﻿using System.Linq;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Base.Storage;
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
        /// <returns>An await-able task</returns>
        public override async Task UpdateFromServiceAsync()
        {
            var result = await Service.SyncDataAsync().ConfigureAwait(false) ?? new TDomain();

            await StoreItemAsync(result).ConfigureAwait(false);

            PublishUpdated(result);
        }

        /// <summary>
        /// Publish a message to subscribers that contains the object managed by this entity. 
        /// Publish state will be <see cref="EDomainState.Updated"/>
        /// </summary>
        /// <returns>An await-able task</returns>
        public override async Task PublishFromStorageAsync()
        {
            PublishUpdated(await FirstOrDefault().ConfigureAwait(false));
        }

        /// <summary>
        /// Get the domain object that is managed by this entity
        /// </summary>
        /// <returns>An await able Task with the domain object as result</returns>
        public virtual async Task<TDomain> FirstOrDefault()
        {
            return await Storage.FirstOrDefault().ConfigureAwait(false);
        }

        /// <summary>
        /// Delete the domain object that is managed by this entity
        /// </summary>
        /// <returns>An await-able task</returns>
        public async Task DeleteAsync()
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