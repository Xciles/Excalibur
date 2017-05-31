using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Excalibur.Shared.Business;
using Excalibur.Shared.Collections;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using Excalibur.Utils;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    public class BasePresentation<TId, TDomain, TObservable, TSelectedObservable> : BPresentation<TId, TDomain, TSelectedObservable>, IPresentation<TId, TObservable, TSelectedObservable>
        where TDomain : StorageDomain<TId>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        protected IObjectMapper<TDomain, TObservable> DomainObservableMapper { get; set; }
        protected IObjectMapper<TObservable, TSelectedObservable> ObservableSelectedMapper { get; set; }
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public BasePresentation()
        {
            // retrieve mappers
            this.Subscribe<MessageBase<IList<TDomain>>>(async (message) => { await ListUpdatedHandler(message); });
            this.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainObservableMapper = Resolver.Resolve<IObjectMapper<TDomain, TObservable>>();
            ObservableSelectedMapper = Resolver.Resolve<IObjectMapper<TObservable, TSelectedObservable>>();
        }

        public IObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
        }

        // Todo double check the Task change.
        protected virtual async Task ListUpdatedHandler(MessageBase<IList<TDomain>> messageBase)
        {
            // todo Might need to add Task.Run/Startnews arround the dispatcher threads
            IsLoading = true;

            await _semaphore.WaitAsync((30 * 1000)); // 30 sec

            var objects = await Resolver.Resolve<IListBusiness<TId, TDomain>>().GetAllAsync().ConfigureAwait(false);

            var deleteIds = 0;
            try
            {
                deleteIds = Observables.Select(x => x.Id).Except(objects.Select(x => x.Id)).Count();
            }
            catch (Exception)
            {
            }

            var count = objects.Count + deleteIds;
            VerifyAndResetCountdown(count);

            var dispatcher = Resolver.Resolve<IExMainThreadDispatcher>();

            foreach (var observable in Observables.Reverse())
            {
                if (!objects.Select(x => x.Id).Contains(observable.Id))
                {
                    TObservable tmpObservable = observable;
                    dispatcher.InvokeOnMainThread(() =>
                    {
                        Observables.Remove(tmpObservable);
                        SignalCde();
                    });
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
                    dispatcher.InvokeOnMainThread(() =>
                    {
                        Observables.Add(observable);
                        SignalCde();
                    });
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
        }

        protected virtual bool ObservablesContainsId(TId id)
        {
            return Observables.FirstOrDefault(x => x.Id.Equals(id)) != null;
        }

        public virtual void SetSelectedObservable(TId observableId)
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
                        var result = Resolver.Resolve<IListBusiness<TId, TDomain>>().GetByIdAsync(observableId).Result; // Todo make method async?
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

        public virtual TObservable GetObservable(TId observableId)
        {
            if (Observables.Any() && ObservablesContainsId(observableId))
            {
                return Observables.First(x => x.Id.Equals(observableId));
            }

            var result = Resolver.Resolve<IListBusiness<TId, TDomain>>().GetByIdAsync(observableId).Result; // Todo make method async?
            if (result != null)
            {
                return DomainObservableMapper.Map(result);
            }

            return default(TObservable);
        }

        ~BasePresentation()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.Unsubscribe<TDomain>();
            }
        }
    }
}
