using System;
using System.Threading;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Providers;

namespace Excalibur.Cross.Presentation
{
    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This base provides an implementation for the <see cref="BaseListPresentation{TId,TDomain,TObservable,TSelectedObservable}"/> and <see cref="BaseSortedPresentation{TId,TDomain,TObservable,TSelectedObservable,TComparer}"/>.
    /// The <see cref="BaseSinglePresentation{TId,TDomain,TObservable}"/> will have a different base, this is because of various additions in this class are not needed for just the one object.
    /// 
    /// Single presentation will manage the one object. 
    /// The selected observable will be used by the List implementation <see cref="IListPresentation{TId,TObservable,TSelectedObservable}"/> as the reference
    /// that will be updated when selecting a Observable.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    public abstract class BasePresentation<TId, TDomain, TSelectedObservable> : ObservableObjectBase, ISinglePresentation<TId, TSelectedObservable>
        where TDomain : ProviderDomain<TId>
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        private bool _isLoading = true;

        /// <summary>
        /// Object mapper that can be used for mapping from TDomain to a TSelectedObservable or vice versa.
        /// </summary>
        protected IObjectMapper<TDomain, TSelectedObservable> DomainSelectedMapper { get; set; }

        /// <summary>
        /// A <see cref="CountdownEvent"/> can be used to wait before continuing the flow of events. 
        /// Using a CDE actual UpdateHandlers (subscribe actions) can be used to wait on when wanting to set additional information
        /// based on the data returned.
        /// </summary>
        protected CountdownEvent Cde { get; private set; }

        /// <summary>
        /// Initializes a new BasePresentation
        /// Resolves the Domain to Selected mapper.
        /// </summary>
        protected BasePresentation(IObjectMapper<TDomain, TSelectedObservable> domainSelectedMapper)
        {
            DomainSelectedMapper = domainSelectedMapper;
        }

        /// <inheritdoc />
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        /// <inheritdoc />
        public TSelectedObservable SelectedObservable
        {
            get => _selectedObservable;
            set => SetProperty(ref _selectedObservable, value);
        }

        /// <inheritdoc />
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Method verifies and resets the <see cref="Cde"/> count to the requested count.
        /// </summary>
        /// <param name="count">The number of signals required to set the <see cref="CountdownEvent"/></param>
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

        /// <summary>
        /// Method used to signal the <see cref="Cde"/>.
        /// </summary>
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

        /// <summary>
        /// Clears the selected observable
        /// </summary>
        public virtual void Clear()
        {
            SelectedObservable = new TSelectedObservable();
            RaisePropertyChanged(() => SelectedObservable);
        }
    }
}