using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;

namespace Excalibur.Cross.ViewModels
{
    public abstract class DetailViewModelOfInt<TSelectedObservable, TPresentation> : DetailViewModel<int, TSelectedObservable, TPresentation>
        where TSelectedObservable : ObservableBaseOfInt, new()
        where TPresentation : class, ISinglePresentationOfInt<TSelectedObservable>
    {
    }
}