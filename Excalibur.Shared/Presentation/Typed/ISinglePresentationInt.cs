using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    public interface ISinglePresentationInt<TSelectedObservable> : ISinglePresentation<int, TSelectedObservable>
        where TSelectedObservable : ObservableBaseInt, new()
    {
    }
}