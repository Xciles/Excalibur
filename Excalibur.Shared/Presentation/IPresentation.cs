using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    public interface IPresentation<TId, TSelectedObservable>
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        bool IsLoading { get; set; }
        TSelectedObservable SelectedObservable { get; set; }

        void Initialize();
    }

    public interface IPresentation<TId, TObservable, TSelectedObservable> : IPresentation<TId, TSelectedObservable>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        IObservableCollection<TObservable> Observables { get; set; }

        void SetSelectedObservable(TId observableId);
    }

    public interface IPresentationSorted<TId, TObservable, TSelectedObservable> : IPresentation<TId, TSelectedObservable>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        ISortedObservableCollection<TObservable> Observables { get; set; }

        void SetSelectedObservable(TId observableId);
    }
}
