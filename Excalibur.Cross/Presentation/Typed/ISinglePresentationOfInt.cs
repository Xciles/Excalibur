using Excalibur.Cross.Observable.Typed;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation.Typed
{
    /// <inheritdoc />
    public interface ISinglePresentationOfInt<TSelectedObservable> : ISinglePresentation<int, TSelectedObservable>
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}