using System;
using Excalibur.Base.Observable;
using Excalibur.Base.Providers;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Utils;
using PubSub;

namespace Excalibur.Cross.Presentation
{
    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This base provides an implementation for Domain objects based on a single object.
    /// 
    /// Single presentation will manage the one object. 
    /// The selected observable will be used by the List implementation <see cref="IListPresentation{TId,TObservable,TSelectedObservable}"/> as the reference
    /// that will be updated when selecting a Observable.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TObservable">The type that should be used for details information</typeparam>
    public class BaseSinglePresentation<TId, TDomain, TObservable> : BasePresentation<TId, TDomain, TObservable>, IDisposable
    where TDomain : ProviderDomain<TId>
    where TObservable : ObservableBase<TId>, new()
    {
        /// <summary>
        /// Publish and Subscribe hub for PubSub functionality.
        /// </summary>
        protected Hub Hub { get; set; } = Hub.Default;

        /// <summary>
        /// Initializes a new BaseSinglePresentation 
        /// This Resolves the Domain to Selected mapper
        /// Also subscribes to Single item publish message
        /// </summary>
        public BaseSinglePresentation(IObjectMapper<TDomain, TObservable> domainSelectedMapper) : base(domainSelectedMapper)
        {
            // retrieve mappers
            Hub.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainSelectedMapper = domainSelectedMapper;
        }

        /// <summary>
        /// Handler that manages single object updates.
        /// 
        /// This will update an object as the selected observable.
        /// </summary>
        /// <param name="messageBase"></param>
        protected virtual void ItemUpdatedHandler(MessageBase<TDomain> messageBase) => DomainSelectedMapper.UpdateDestination(messageBase.Object, SelectedObservable);

        /// <inheritdoc />
        ~BaseSinglePresentation()
        {
            Dispose(false);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                Hub.Unsubscribe<TDomain>();
            }
        }
    }
}
