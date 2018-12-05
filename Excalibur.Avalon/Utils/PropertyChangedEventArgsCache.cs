using System.Collections.Generic;
using System.ComponentModel;

namespace Excalibur.Avalon.Utils
{
    /// <summary>
    /// https://github.com/StephenCleary/Mvvm.Core/blob/master/src/Nito.Mvvm.Core/PropertyChangedEventArgsCache.cs
    /// The MIT License (MIT)
    ///
    /// Copyright(c) 2015 Stephen Cleary
    /// 
    /// Provides a cache for <see cref="PropertyChangedEventArgs"/> instances.
    /// </summary>
    public sealed class PropertyChangedEventArgsCache
    {
        /// <summary>
        /// The underlying dictionary. This instance is its own mutex.
        /// </summary>
        private readonly Dictionary<string, PropertyChangedEventArgs> _cache = new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// Private constructor to prevent other instances.
        /// </summary>
        private PropertyChangedEventArgsCache()
        {
        }

        /// <summary>
        /// The global instance of the cache.
        /// </summary>
        public static PropertyChangedEventArgsCache Instance { get; } = new PropertyChangedEventArgsCache();

        /// <summary>
        /// Retrieves a <see cref="PropertyChangedEventArgs"/> instance for the specified property, creating it and adding it to the cache if necessary.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        public PropertyChangedEventArgs Get(string propertyName)
        {
            lock (_cache)
            {
                if (_cache.TryGetValue(propertyName, out var result))
                {
                    return result;
                }

                result = new PropertyChangedEventArgs(propertyName);
                _cache.Add(propertyName, result);
                return result;
            }
        }
    }
}