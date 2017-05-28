using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;
using XLabs.Ioc;

namespace Excalibur.Cross.ViewModels
{
    public abstract class DetailViewModel<TId, TObservable, TSelectedObservable, TPresentation> : BaseViewModel
        where TObservable : ObservableBase<TId>, new()
        where TPresentation : class, IPresentation<TId, TObservable, TSelectedObservable>
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();

        protected DetailViewModel()
        {
            var presentation = Resolver.Resolve<TPresentation>();
            SelectedObservable = presentation.SelectedObservable;
        }

        public TSelectedObservable SelectedObservable
        {
            get { return _selectedObservable; }
            set { SetProperty(ref _selectedObservable, value); }
        }
    }
}