using Excalibur.Cross.Observable.Typed;
using Excalibur.Cross.Presentation.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.ViewModels.Typed
{
    /// <inheritdoc />
    public abstract class DetailViewModelOfInt<TSelectedObservable, TPresentation> : DetailViewModel<int, TSelectedObservable, TPresentation>
        where TSelectedObservable : ObservableBaseOfInt, new()
        where TPresentation : class, ISinglePresentationOfInt<TSelectedObservable>
    {
        protected DetailViewModelOfInt(TPresentation presentation) : base(presentation)
        {
        }
    }
}