using System.Collections.Generic;
using System.Collections.Specialized;

namespace Excalibur.Shared.Collections
{
    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}
