using System.ComponentModel;

namespace Excalibur.Shared.Collections
{
    public interface ISortedObservableCollection<T> : IObservableCollection<T>, INotifyPropertyChanged
    {
        void InsertItem(T item);
    }
}
