using System;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using Excalibur.Utils;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    public class BaseSinglePresentation<TId, TDomain, TObservable> : ObservableObjectBase, ISinglePresentation<TId, TObservable>
        where TDomain : StorageDomain<TId>
        where TObservable : ObservableBase<TId>, new()
    {
        private bool _isLoading = true;
        private TObservable _selectedObservable = new TObservable();
        protected IObjectMapper<TDomain, TObservable> DomainSelectedMapper { get; set; }

        public BaseSinglePresentation()
        {
            // retrieve mappers
            this.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainSelectedMapper = Resolver.Resolve<IObjectMapper<TDomain, TObservable>>();
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

        public TObservable SelectedObservable
        {
            get { return _selectedObservable; }
            set { SetProperty(ref _selectedObservable, value); }
        }

        public virtual void Initialize()
        {
        }

        protected virtual void ItemUpdatedHandler(MessageBase<TDomain> messageBase)
        {
            DomainSelectedMapper.UpdateDestination(messageBase.Object, SelectedObservable);
        }

        ~BaseSinglePresentation()
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
