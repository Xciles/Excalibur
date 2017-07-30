using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    public interface IListPresentationInt<TObservable> : IListPresentationInt<TObservable, TObservable>
        where TObservable : ObservableBaseInt, new()
    {
    }
    
    public interface IListPresentationInt<TObservable, TSelectedObservable> : IListPresentation<int, TObservable, TSelectedObservable>
        where TObservable : ObservableBaseInt, new()
        where TSelectedObservable : ObservableBaseInt, new()
    {
    }
}