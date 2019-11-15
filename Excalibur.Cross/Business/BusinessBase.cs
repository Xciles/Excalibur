using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Cross.Utils;
using PubSub;

namespace Excalibur.Cross.Business
{
    /// <summary>
    /// Abstract base for business entities. 
    /// This entity provides various Publish methods (using <see cref="PubSub"/>) and methods that should be implemented.
    /// 
    /// The constructor will resolve a corresponding TService and <see cref="IDatabaseProvider{TId,T}"/> for the 
    /// given TDomain />. The provider will be used to store and retrieve stored information.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TService">The type of the service that should be used for communications</typeparam>
    public abstract class BusinessBase<TId, TDomain, TService> : IBusiness
        where TDomain : ProviderDomain<TId>, new()
        where TService : class
    {
        /// <summary>
        /// Instance of a given TService that will and can be used for web requests.
        /// For example, this instance will be used in by <see cref="UpdateFromService"/>
        /// </summary>
        protected TService Service { get; set; }

        /// <summary>
        /// Publish and Subscribe hub for PubSub functionality.
        /// </summary>
        protected Hub Hub { get; set; } = Hub.Default;

        /// <summary>
        /// This bool indicates to delete the locally stored items that were not returned or keep them when updating from a
        ///     service <see cref="UpdateFromService"/>.
        /// </summary>
        protected bool DeleteNotReturnedItems { get; set; } = false;

        /// <summary>
        /// Instance of a given <see cref="IDatabaseProvider{TId,TDomain}"/> that will be used for storing entities
        /// </summary>
        protected IDatabaseProvider<TId, TDomain> Storage { get; set; }

        /// <summary>
        /// Initializes the instance.
        /// Resolves a Service and Storage that will be used.
        /// </summary>
        protected BusinessBase(TService service, IDatabaseProvider<TId, TDomain> storageProvider)
        {
            Service = service;
            Storage = storageProvider;
        }

        /// <summary>
        /// Publishes a message using <see cref="MessageBase{T}"/> to notify subscribers underlying objects have changed.
        /// This is a general message. It does not contain the objects.
        /// </summary>
        protected void PublishListUpdated() => Hub.Publish<MessageBase<IList<TDomain>>>();

        /// <summary>
        /// Publishes a message using <see cref="MessageBase{T}"/> to notify subscribers that one object has changed. 
        /// This also includes which object was updated and why it was updated.
        /// </summary>
        /// <param name="updatedObject">The object that was updated.</param>
        /// <param name="state">The state of the object.</param>
        protected void PublishUpdated(TDomain updatedObject, EDomainState state = EDomainState.Updated) => Hub.Publish(new MessageBase<TDomain>(updatedObject, state));

        /// <summary>
        /// Stores and persists an object using <see cref="Storage"/>.
        /// </summary>
        /// <param name="objectToStore">The object that should be stored</param>
        protected async Task StoreItem(TDomain objectToStore)
        {
            await Storage.Insert(objectToStore).ConfigureAwait(false);

            PublishListUpdated();
        }

        /// <inheritdoc />
        public async Task Clear()
        {
            await Storage.Clear().ConfigureAwait(false);

            PublishListUpdated();
        }

        /// <inheritdoc />
        public abstract Task UpdateFromService();

        /// <inheritdoc />
        public abstract Task PublishFromStorage();

        /// <inheritdoc />
        public virtual Task AfterServiceSyncData() => Task.CompletedTask;
    }
}