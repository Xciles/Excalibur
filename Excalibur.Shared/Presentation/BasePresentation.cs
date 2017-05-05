using System;
using System.Collections.Generic;
using System.Linq;
using Excalibur.Shared.Business;
using Excalibur.Shared.Collections;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    public class BasePresentation<TId, TDomain, TObservable, TSelectedObservable> : ObservableObjectBase
        where TDomain : StorageDomain<TId>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        protected IObjectMapper<TDomain, TObservable> DomainObservableMapper { get; set; }
        protected IObjectMapper<TDomain, TSelectedObservable> DomainSelectedMapper { get; set; }
        protected IObjectMapper<TObservable, TSelectedObservable> ObservableSelectedMapper { get; set; }
        private bool _isLoading = true;

        protected BasePresentation()
        {
            // retrieve mappers
            this.Subscribe<IList<TDomain>>(ListUpdatedHandler);
        }

        public IObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
        }

        protected virtual void ListUpdatedHandler(IList<TDomain> objects)
        {
            // Might need to add new threads and main thread requests.
            IsLoading = true;

            foreach (var observable in Observables.Reverse())
            {
                if (!objects.Select(x => x.Id).Contains(observable.Id))
                {
                    Observables.Remove(observable);
                }
            }

            foreach (var domainObject in objects)
            {
                if (ObservablesContainsId(domainObject.Id))
                {
                    var observable = Observables.First(x => x.Id.Equals(domainObject.Id));
                    DomainObservableMapper.UpdateDestination(domainObject, observable);
                }
                else
                {
                    var observable = DomainObservableMapper.Map(domainObject);
                    Observables.Add(observable);
                }
            }

            if (SelectedObservable.IsTransient() && Observables.Any())
            {
                ObservableSelectedMapper.UpdateDestination(Observables.First(), SelectedObservable);
            }

            IsLoading = false;
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


    }
}
