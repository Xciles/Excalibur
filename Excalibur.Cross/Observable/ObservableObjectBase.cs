using System;
using System.Runtime.CompilerServices;
using MvvmCross.ViewModels;

namespace Excalibur.Cross.Observable
{
    /// <summary>
    /// Base implementation for an object that needs to be an observable. This class implements <see cref="MvxNotifyPropertyChanged"/> and provides <see cref="SetProperty{T}"/> 
    /// for setting and notifying.
    /// </summary>
    public abstract class ObservableObjectBase : MvxNotifyPropertyChanged
    {
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="onChanged">On changed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            var result = base.SetProperty(ref backingStore, value, propertyName);

            onChanged?.Invoke();
            return result;
        }
    }
}
