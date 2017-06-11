using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TSelectedObservable"></typeparam>
    public interface ISinglePresentation<TId, TSelectedObservable>
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        bool IsLoading { get; set; }
        TSelectedObservable SelectedObservable { get; set; }

        void Initialize();
    }
}