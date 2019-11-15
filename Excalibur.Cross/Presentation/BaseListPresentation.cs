using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Cross.Business;
using Excalibur.Cross.Collections;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Utils;
using MvvmCross.Base;
using PubSub;

namespace Excalibur.Cross.Presentation
{
    /// <inheritdoc cref="BaseListPresentation{TId,TDomain,TObservable,TObservable}"/>
    public class BaseListPresentation<TId, TDomain, TObservable> : BaseListPresentation<TId, TDomain, TObservable, TObservable>, IListPresentation<TId, TObservable>
        where TDomain : ProviderDomain<TId>
        where TObservable : ObservableBase<TId>, new()
    {
        public BaseListPresentation(
            IObjectMapper<TDomain, TObservable> domainMapper,
            IObjectMapper<TObservable, TObservable> observableSelectedMapper,
            IListBusiness<TId, TDomain> listBusiness,
            IMvxMainThreadAsyncDispatcher dispatcher)
            : base(domainMapper, domainMapper, observableSelectedMapper, listBusiness, dispatcher)
        {
        }
    }

    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This base provides an implementation for Domain objects based on a list.
    /// 
    /// This presentation will manage the all objects. 
    /// References to observables will be updated when an update is published and will be updated with new information when needed.
    /// The selected observable will be used by when navigating to detail view models.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TObservable">The type that should be used for the collections of objects</typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    public class BaseListPresentation<TId, TDomain, TObservable, TSelectedObservable> : BasePresentation<TId, TDomain, TSelectedObservable>, IListPresentation<TId, TObservable, TSelectedObservable>, IDisposable
        where TDomain : ProviderDomain<TId>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        /// <summary>
        /// Publish and Subscribe hub for PubSub functionality.
        /// </summary>
        protected Hub Hub { get; set; } = Hub.Default;

        protected IListBusiness<TId, TDomain> ListBusiness { get; set; }
        protected IMvxMainThreadAsyncDispatcher Dispatcher { get; set; }
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Object mapper that can be used for mapping from TDomain to a TObservable or vice versa.
        /// </summary>
        protected IObjectMapper<TDomain, TObservable> DomainObservableMapper { get; set; }

        /// <summary>
        /// Object mapper that can be used for mapping from TObservable to a TSelectedObservable or vice versa.
        /// </summary>
        protected IObjectMapper<TObservable, TSelectedObservable> ObservableSelectedMapper { get; set; }

        /// <summary>
        /// Initializes a new BaseListPresentation 
        /// This Resolves the Domain to Observable mapper and Observable to Selected Observable mapper.
        /// Also subscribes to both the List and Single item publish message
        /// </summary>
        public BaseListPresentation(
            IObjectMapper<TDomain, TObservable> domainObservableMapper,
            IObjectMapper<TDomain, TSelectedObservable> domainSelectedMapper,
            IObjectMapper<TObservable, TSelectedObservable> observableSelectedMapper,
            IListBusiness<TId, TDomain> listBusiness,
            IMvxMainThreadAsyncDispatcher dispatcher)
            : base(domainSelectedMapper)
        {
            // retrieve mappers
            Hub.Subscribe<MessageBase<IList<TDomain>>>(async (message) => { await ListUpdatedHandler(message).ConfigureAwait(false); });
            Hub.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainObservableMapper = domainObservableMapper;
            ObservableSelectedMapper = observableSelectedMapper;
            ListBusiness = listBusiness;
            Dispatcher = dispatcher;
        }

        /// <inheritdoc />
        public IObservableCollection<TObservable> Observables
        {
            get => _observables;
            set => SetProperty(ref _observables, value);
        }

        /// <summary>
        /// Todo double check the Task change.
        /// Todo Implement range update
        /// 
        /// Handler for managing the publish message when a list updated is being published
        /// This will make sure, using a semaphore, that just the one thread will update at a given time.
        /// 
        /// Old items will be removed from the observables and new ones will be added. A business will be used to retrieve the objects that should be mapped.
        /// When busy <see cref="BasePresentation{TId,TDomain,TSelectedObservable}.IsLoading"/> will be used to indicate if the Presentation is busy.
        /// </summary>
        /// <param name="messageBase"></param>
        /// <returns></returns>
        protected virtual async Task ListUpdatedHandler(MessageBase<IList<TDomain>> messageBase)
        {
            // todo Might need to add Task.Run/Startnews around the dispatcher threads
            IsLoading = true;

            await _semaphore.WaitAsync((30 * 1000)).ConfigureAwait(false); // 30 sec

            var objects = (await ListBusiness.ObjectsForListUpdatedHandler().ConfigureAwait(false)).ToList();

            var deleteIds = 0;
            try
            {
                deleteIds = Observables.Select(x => x.Id).Except(objects.Select(x => x.Id)).Count();
            }
            catch (Exception)
            {
                // ignored
            }

            var count = objects.Count + deleteIds;
            VerifyAndResetCountdown(count);

            foreach (var observable in Observables.Reverse())
            {
                if (!objects.Select(x => x.Id).Contains(observable.Id))
                {
                    var tmpObservable = observable;
                    Dispatcher.ExecuteOnMainThreadAsync(() =>
                    {
                        Observables.Remove(tmpObservable);
                        SignalCde();
                    }).ConfigureAwait(false);
                }
            }

            foreach (var domainObject in objects)
            {
                if (ObservablesContainsId(domainObject.Id))
                {
                    var observable = Observables.First(x => x.Id.Equals(domainObject.Id));
                    DomainObservableMapper.UpdateDestination(domainObject, observable);
                    SignalCde();
                }
                else
                {
                    var observable = DomainObservableMapper.Map(domainObject);
                    Dispatcher.ExecuteOnMainThreadAsync(() =>
                    {
                        Observables.Add(observable);
                        SignalCde();
                    }).ConfigureAwait(false);
                }
            }

            if (SelectedObservable.IsTransient() && Observables.Any())
            {
                ObservableSelectedMapper.UpdateDestination(Observables.First(), SelectedObservable);
            }

            Task.Factory.StartNew(() =>
            {
                Cde.Wait(1000);
                _semaphore.Release();
            }).ConfigureAwait(false);
            IsLoading = false;
        }

        /// <summary>
        /// Handler that manages single object updates.
        /// 
        /// This will update an object within the Observable Collection and if the object is selected it will be updated as well.
        /// </summary>
        /// <param name="messageBase"></param>
        protected virtual void ItemUpdatedHandler(MessageBase<TDomain> messageBase)
        {
            // Update item in the list
            var itemInList = Observables.FirstOrDefault(x => x.Id.Equals(messageBase.Object.Id));
            if (itemInList != null)
            {
                DomainObservableMapper.UpdateDestination(messageBase.Object, itemInList);
            }

            // Update the selected item only if updated and is selected
            if (SelectedObservable.Id.Equals(messageBase.Object.Id) && messageBase.State == EDomainState.Updated)
            {
                DomainSelectedMapper.UpdateDestination(messageBase.Object, SelectedObservable);
            }

            // Delete the item if it is deleted
            if (SelectedObservable.Id.Equals(messageBase.Object.Id) && messageBase.State == EDomainState.Deleted && itemInList != null)
            {
                Observables.Remove(itemInList);
            }
        }

        /// <summary>
        /// Just a check to see if the Observables contain an object with a certain Id
        /// </summary>
        /// <param name="id">The id to check if exists within the Observables</param>
        /// <returns>True if found, false otherwise</returns>
        protected virtual bool ObservablesContainsId(TId id) => Observables.FirstOrDefault(x => x.Id.Equals(id)) != null;

        /// <summary>
        /// Method to be used when navigating to for example detail view models or when selecting a certain object. 
        /// This will make sure the object wanted for selection will be mapped in <see cref="BasePresentation{TId,TDomain,TSelectedObservable}.SelectedObservable"/>.
        /// 
        /// <see cref="BasePresentation{TId,TDomain,TSelectedObservable}.SelectedObservable"/> can then be used for detailed information about the object that was selected.
        /// </summary>
        /// <param name="observableId">The id of the object that should be set as SelectedObservable</param>
        public virtual async Task SetSelectedObservable(TId observableId)
        {
            try
            {
                if (Observables.Any())
                {
                    var usedObservable = Observables.FirstOrDefault(x => x.Id.Equals(observableId));
                    if (usedObservable != null)
                    {
                        ObservableSelectedMapper.UpdateDestination(usedObservable, SelectedObservable);
                    }
                    else
                    {
                        var result = await ListBusiness.FindById(observableId).ConfigureAwait(false);
                        if (result != null)
                        {
                            DomainSelectedMapper.UpdateDestination(result, SelectedObservable);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // todo logging
            }
        }

        /// <inheritdoc />
        public virtual async Task<TObservable> GetObservable(TId observableId)
        {
            if (Observables.Any() && ObservablesContainsId(observableId))
            {
                return Observables.First(x => x.Id.Equals(observableId));
            }

            var result = await ListBusiness.FindById(observableId).ConfigureAwait(false);
            if (result != null)
            {
                return DomainObservableMapper.Map(result);
            }

            return default(TObservable);
        }

        /// <inheritdoc />
        public override void Clear()
        {
            base.Clear();
            Observables.Clear();
            RaisePropertyChanged(() => Observables);
        }

        /// <inheritdoc />
        ~BaseListPresentation()
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
