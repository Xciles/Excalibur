using System;
using System.Collections.Generic;
using System.Linq;
using Excalibur.Shared.Business;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Comparers;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    public abstract class BaseSortedPresentation<TId, TDomain, TObservable, TSelectedObservable, TComparer> : BPresentation<TId, TDomain, TSelectedObservable>, IPresentationSorted<TId, TObservable, TSelectedObservable>
        where TDomain : StorageDomain<TId>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
        where TComparer : BaseComparer<TObservable>, new()
    {
        private ISortedObservableCollection<TObservable> _observables = new ExSortedObservableCollection<TObservable>(new TComparer());
        protected IObjectMapper<TDomain, TObservable> DomainObservableMapper { get; set; }
        protected IObjectMapper<TObservable, TSelectedObservable> ObservableSelectedMapper { get; set; }

        protected BaseSortedPresentation()
        {
            // retrieve mappers
            this.Subscribe<IList<TDomain>>(ListUpdatedHandler);
            this.Subscribe<TDomain>(ItemUpdatedHandler);

            DomainObservableMapper = Resolver.Resolve<IObjectMapper<TDomain, TObservable>>();
            ObservableSelectedMapper = Resolver.Resolve<IObjectMapper<TObservable, TSelectedObservable>>();
        }


        private void ListUpdatedHandler(IList<TDomain> objects)
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

        private void ItemUpdatedHandler(TDomain domain)
        {
            // Update item in the list
            var itemInList = Observables.FirstOrDefault(x => x.Id.Equals(domain.Id));
            if (itemInList != null)
            {
                DomainObservableMapper.UpdateDestination(domain, itemInList);
            }

            // Update the selected item
            DomainSelectedMapper.UpdateDestination(domain, SelectedObservable);
        }

        public ISortedObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
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
    }
}
