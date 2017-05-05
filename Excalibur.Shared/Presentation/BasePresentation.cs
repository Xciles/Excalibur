using System;
using System.Collections.Generic;
using Excalibur.Shared.Collections;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using PubSub;

namespace Excalibur.Shared.Presentation
{
    public class BasePresentation<TDomain, TObservable, TSelectedObservable> : ObservableBase
        where TObservable : ObservableBase, new()
        where TSelectedObservable : ObservableBase, new()
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        protected IObjectMapper<TDomain, TObservable> Mapper { get; set; }
        protected IObjectMapper<TDomain, TSelectedObservable> SelectedMapper { get; set; }
        private bool _isLoading = true;

        protected BasePresentation()
        {
            // retrieve mappers
            this.Subscribe<IList<TDomain>>(ListUpdatedHandler);
        }

        protected virtual void ListUpdatedHandler(IList<TDomain> objects)
        {

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
