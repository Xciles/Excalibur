using System.Collections.Generic;

namespace Excalibur.Avalon.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Method that will either add or update a given key in a dictionary
        /// </summary>
        /// <typeparam name="TKey">The type for the Key</typeparam>
        /// <typeparam name="TValue">The type for the Value</typeparam>
        /// <param name="dictionary">The dictionary that you want to use</param>
        /// <param name="key">The key that will be used to add/update the value for</param>
        /// <param name="value">The value you want to add/update</param>
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
