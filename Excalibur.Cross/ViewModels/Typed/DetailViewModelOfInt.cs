using Excalibur.Cross.Observable;
using Excalibur.Cross.Presentation;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.ViewModels
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