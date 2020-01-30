using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Excalibur.Base.Attributes;
using MvvmCross.ViewModels;

namespace Excalibur.Base.Observable
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

        protected bool SetPropertyAndRaiseDependentOn<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var result = SetProperty(ref storage, value, propertyName);

            // Call raise property changed on dependent properties if the property value has changed
            if (result && propertyName != null)
            {
                // Get the dependent properties specified by the PropertyChangedDependencyAttribute
                RaiseDependentOnProperties(propertyName);
            }
            return result;
        }

        public Task SetPropertyAndRaiseDependentOnChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                RaiseDependentOnProperties(propertyName);
            }
            return base.RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Raise all properties dependent on the property specified by <paramref name="propertyName"/>
        /// </summary>
        protected void RaiseDependentOnProperties(string propertyName)
        {
            // Get the dependent properties specified by the PropertyChangedDependencyAttribute
            var dependentProperties = GetType().GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(PropertyChangedDependentOnAttribute), false)
                    .Any(attr => ((PropertyChangedDependentOnAttribute)attr).DependentOnPropertyNames.Any(prop => prop == propertyName)));

            foreach (var property in dependentProperties)
            {
                RaisePropertyChanged(property.Name);
            }
        }
    }
}
