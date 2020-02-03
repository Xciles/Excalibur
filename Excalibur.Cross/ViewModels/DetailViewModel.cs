﻿using Excalibur.Cross.Observable;
using Excalibur.Cross.Presentation;

namespace Excalibur.Cross.ViewModels
{
    /// <summary>
    /// Base detail view implementation. 
    /// This will try to bind <see cref="SelectedObservable"/> to the Presentations SelectedObservable.
    /// 
    /// The base will provide some default methods that can be used in views.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    /// <typeparam name="TPresentation">The type that should be used to resolve Observables</typeparam>
    public abstract class DetailViewModel<TId, TSelectedObservable, TPresentation> : BaseViewModel
        where TSelectedObservable : ObservableBase<TId>, new()
        where TPresentation : class, ISinglePresentation<TId, TSelectedObservable>
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();

        /// <summary>
        /// Initializes an instance of DetailViewModel. 
        /// This will try to bind the <see cref="SelectedObservable"/> to the presentations SelectedObservable
        /// </summary>
        protected DetailViewModel(TPresentation presentation)
        {
            SelectedObservable = presentation.SelectedObservable;
        }

        /// <summary>
        /// The selected observable
        /// </summary>
        public TSelectedObservable SelectedObservable
        {
            get => _selectedObservable;
            set => SetProperty(ref _selectedObservable, value);
        }
    }
}