using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Excalibur.Base.Observable;
using Excalibur.Cross.Observable;

namespace Excalibur.Cross.Collections
{
    /// <summary>
    /// A base observable collection
    /// </summary>
    /// <typeparam name="T">The type used within the collection</typeparam>
    public class ExObservableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
        where T : ObservableObjectBase
    {
        public EventHandler<PropertyChangedEventArgs> ItemPropertyChanged { get; set; }

        /// <inheritdoc />
        public ExObservableCollection(IList<T> source)
            : base(source)
        {
            foreach (var item in Items)
            {
                item.PropertyChanged += ChildItemPropertyChanged;
            }
        }

        /// <summary>
        /// Object that will be used to lock() on.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// GetEnumerator implementation for thread safety
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            // We have to wrap the unsafe enumerator into a thread-safe class
            // And then return it
            var enumerator = new SafeEnumerator<T>(_lock);

            enumerator.SetList(Items.GetEnumerator());

            return enumerator;
        }

        /// <inheritdoc />
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public new void Add(T item)
        {
            lock (_lock)
            {
                base.Add(item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public new bool Remove(T item)
        {
            lock (_lock)
            {
                return base.Remove(item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public new T this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return base[index];
                }
            }
            set
            {
                lock (_lock)
                {
                    base[index] = value;
                }
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (e.OldItems != null)
            {
                foreach (ObservableObjectBase item in e.OldItems)
                {
                    if (e.NewItems == null || !e.NewItems.Contains(item))
                    {
                        item.PropertyChanged -= ChildItemPropertyChanged;
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (ObservableObjectBase item in e.NewItems)
                {
                    if (e.OldItems == null || !e.OldItems.Contains(item))
                    {
                        item.PropertyChanged += ChildItemPropertyChanged;
                    }
                }
            }
        }

        protected override void ClearItems()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged -= ChildItemPropertyChanged;
            }

            lock (_lock)
            {
                base.ClearItems();
            }
        }

        private void ChildItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var handler = ItemPropertyChanged;
            handler?.Invoke(sender, e);
        }
    }
}
