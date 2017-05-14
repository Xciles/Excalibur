using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Excalibur.Shared.Business;
using Excalibur.Shared.Collections;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using Excalibur.Utils;
using MvvmCross.Platform.Core;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    public abstract class BPresentation<TId, TDomain, TSelectedObservable> : ObservableObjectBase, IPresentation<TId, TSelectedObservable>
        where TDomain : StorageDomain<TId>
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        protected IObjectMapper<TDomain, TSelectedObservable> DomainSelectedMapper { get; set; }
        private bool _isLoading = true;
        protected CountdownEvent Cde { get; private set; }

        protected BPresentation()
        {
            DomainSelectedMapper = Resolver.Resolve<IObjectMapper<TDomain, TSelectedObservable>>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this object is loading.
        /// </summary>
        /// <value>
        /// true if this object is loading, false if not.
        /// </value>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public TSelectedObservable SelectedObservable
        {
            get { return _selectedObservable; }
            set { SetProperty(ref _selectedObservable, value); }
        }

        public virtual void Initialize()
        {
        }

        protected virtual void VerifyAndResetCountdown(int count)
        {
            if ((Cde != null && Cde.IsSet))
            {
                Cde.Reset(count);
            }
            else
            {
                Cde = new CountdownEvent(count);
            }
        }

        protected void SignalCde()
        {
            try
            {
                Cde.Signal();
            }
            catch (InvalidOperationException)
            {
                // todo Add trace logging
            }
        }
    }

    public class BasePresentation<TId, TDomain, TObservable, TSelectedObservable> : BPresentation<TId, TDomain, TSelectedObservable>, IPresentation<TId, TObservable, TSelectedObservable>
        where TDomain : StorageDomain<TId>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        protected IObjectMapper<TDomain, TObservable> DomainObservableMapper { get; set; }
        protected IObjectMapper<TObservable, TSelectedObservable> ObservableSelectedMapper { get; set; }

        public BasePresentation()
        {
            // retrieve mappers
            this.Subscribe<MessageBase<IList<TDomain>>>(ListUpdatedHandler);
            this.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainObservableMapper = Resolver.Resolve<IObjectMapper<TDomain, TObservable>>();
            ObservableSelectedMapper = Resolver.Resolve<IObjectMapper<TObservable, TSelectedObservable>>();
        }

        public IObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
        }

        protected virtual async void ListUpdatedHandler(MessageBase<IList<TDomain>> messageBase)
        {
            // Might need to add new threads and main thread requests.
            IsLoading = true;

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

                    dispatcher.InvokeOnMainThread(() =>
                    {
                        DomainObservableMapper.UpdateDestination(domainObject, observable);
                        SignalCde();
                    });
                }
                else
                {
                    dispatcher.InvokeOnMainThread(() =>
                    {
                        var observable = DomainObservableMapper.Map(domainObject);
                        Observables.Add(observable);
                        SignalCde();
                    });
                }
            }

            if (SelectedObservable.IsTransient() && Observables.Any())
            {
                ObservableSelectedMapper.UpdateDestination(Observables.First(), SelectedObservable);
            }

            IsLoading = false;
        }

        private void ItemUpdatedHandler(MessageBase<TDomain> messageBase)
        {
            // Update item in the list
            var itemInList = Observables.FirstOrDefault(x => x.Id.Equals(messageBase.Object.Id));
            if (itemInList != null)
            {
                DomainObservableMapper.UpdateDestination(messageBase.Object, itemInList);
            }

            // Update the selected item
            DomainSelectedMapper.UpdateDestination(messageBase.Object, SelectedObservable);
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
            catch (Exception e)
            {
            }
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
