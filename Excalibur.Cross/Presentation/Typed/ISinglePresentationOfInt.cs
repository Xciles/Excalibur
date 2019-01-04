using Excalibur.Cross.Observable;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.Presentation
{
    /// <inheritdoc />
    public interface ISinglePresentationOfInt<TSelectedObservable> : ISinglePresentation<int, TSelectedObservable>
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}