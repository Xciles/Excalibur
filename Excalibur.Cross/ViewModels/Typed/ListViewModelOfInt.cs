using Excalibur.Cross.Observable.Typed;
using Excalibur.Cross.Presentation.Typed;
using MvvmCross.ViewModels;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.ViewModels.Typed
{
    /// <inheritdoc />
    public abstract class ListViewModelOfInt<TObservable, TPresentation, TDetailViewModel> : ListViewModelOfInt<TObservable, TObservable, TPresentation, TDetailViewModel>
        where TObservable : ObservableBaseOfInt, new()
        where TPresentation : class, IListPresentationOfInt<TObservable>
        where TDetailViewModel : IMvxViewModel
    {
        protected ListViewModelOfInt(TPresentation presentation) : base(presentation)
        {
        }
    }

    /// <inheritdoc />
    public abstract class ListViewModelOfInt<TObservable, TSelectedObservable, TPresentation, TDetailViewModel> : ListViewModel<int, TObservable, TSelectedObservable, TPresentation, TDetailViewModel>
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
        where TPresentation : class, IListPresentationOfInt<TObservable, TSelectedObservable>
        where TDetailViewModel : IMvxViewModel
    {
        protected ListViewModelOfInt(TPresentation presentation) : base(presentation)
        {
        }
    }
}