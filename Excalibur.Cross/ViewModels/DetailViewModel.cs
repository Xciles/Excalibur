using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;
using XLabs.Ioc;

namespace Excalibur.Cross.ViewModels
{
    public abstract class DetailViewModel<TId, TSelectedObservable, TPresentation> : BaseViewModel
        where TSelectedObservable : ObservableBase<TId>, new()
        where TPresentation : class, ISinglePresentation<TId, TSelectedObservable>
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