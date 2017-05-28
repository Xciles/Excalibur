using System.Collections.Generic;
using System.Windows.Input;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.ViewModels;
using XLabs.Ioc;

namespace Excalibur.Cross.ViewModels
{
    public abstract class ListViewModel<TId, TObservable, TSelectedObservable, TPresentation, TDetailViewModel> : BaseViewModel
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
        where TPresentation : class, IPresentation<TId, TObservable, TSelectedObservable>
        where TDetailViewModel : IMvxViewModel
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        private bool _isLoading;

        protected ListViewModel()
        {
            var presentation = Resolver.Resolve<TPresentation>();
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
                    var presentation = Resolver.Resolve<TPresentation>();
                    presentation.SetSelectedObservable(selected.Id);
                    ShowViewModel<TDetailViewModel>();
                });
            }
        }
    }
}