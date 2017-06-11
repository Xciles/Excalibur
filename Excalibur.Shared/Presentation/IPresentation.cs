using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    public interface IPresentation<TId, TObservable, TSelectedObservable> : ISinglePresentation<TId, TSelectedObservable>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        IObservableCollection<TObservable> Observables { get; set; }

        void SetSelectedObservable(TId observableId);
        TObservable GetObservable(TId observableId);
    }
}
