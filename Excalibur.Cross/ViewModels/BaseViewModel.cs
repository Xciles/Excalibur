using System.Collections.Generic;
using System.Windows.Input;
using Excalibur.Cross.Language;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Storage;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using XLabs.Ioc;

namespace Excalibur.Cross.ViewModels
{
    /// <summary>
    ///    Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : MvxViewModel
    {
        public IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder(ExTextProvider.GeneralNamespace, GetType().Name); }
        }

        public IMvxLanguageBinder SharedTextSource
        {
            get { return new MvxLanguageBinder(ExTextProvider.GeneralNamespace, ExTextProvider.SharedNamespace); }
        }

        /// <summary>
        /// Gets the almanac go back command.
        /// </summary>
        /// <value>
        /// The almanac go back command.
        /// </value>
        public virtual ICommand GoBackCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }
    }

    public abstract class ListViewModel<TId, TObservable, TSelectedObservable, TDetailViewModel> : BaseViewModel
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
        where TDetailViewModel : IMvxViewModel
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        private bool _isLoading;

        protected ListViewModel()
        {
            var presentation = Resolver.Resolve<IPresentation<TId, TObservable, TSelectedObservable>>();
            Observables = presentation.Observables;
            SelectedObservable = presentation.SelectedObservable;
            IsLoading = presentation.IsLoading;
        }

        public IObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
        }

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

        public virtual ICommand GoToDetailCommand
        {
            get
            {
                return new MvxCommand<TObservable>((selected) =>
                {
                    var presentation = Resolver.Resolve<IPresentation<TId, TObservable, TSelectedObservable>>();
                    presentation.SetSelectedObservable(selected.Id);
                    ShowViewModel<TDetailViewModel>();
                });
            }
        }
    }

    public abstract class DetailViewModel<TId, TObservable, TSelectedObservable> : BaseViewModel
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();

        protected DetailViewModel()
        {
            var presentation = Resolver.Resolve<IPresentation<TId, TObservable, TSelectedObservable>>();
            SelectedObservable = presentation.SelectedObservable;
        }

        public TSelectedObservable SelectedObservable
        {
            get { return _selectedObservable; }
            set { SetProperty(ref _selectedObservable, value); }
        }
    }
}
