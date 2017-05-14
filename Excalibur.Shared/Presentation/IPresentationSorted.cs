using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    public interface IPresentationSorted<TId, TObservable, TSelectedObservable> : IPresentation<TId, TSelectedObservable>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        ISortedObservableCollection<TObservable> Observables { get; set; }

        void SetSelectedObservable(TId observableId);
    }
}