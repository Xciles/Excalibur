using System.Collections.Generic;

namespace Excalibur.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate(this Dictionary<string, int> dictionary, string key, int value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = dictionary[key] + value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
