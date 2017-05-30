using System;
using System.Threading;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    public abstract class BPresentation<TId, TDomain, TSelectedObservable> : ObservableObjectBase, ISinglePresentation<TId, TSelectedObservable>
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
}