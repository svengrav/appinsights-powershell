using System.Collections.Generic;
using System.Linq;

namespace AppInsights.Extensions
{
    public static class DictionaryExtensions
    {
        public static void MergeDictionary<K, V>(this IDictionary<K, V> targetDictionary, IDictionary<K, V> sourceDictionary)
        {
            sourceDictionary.ToList()
                .ForEach(keyValuePair => 
                    targetDictionary.Add(keyValuePair.Key, keyValuePair.Value));
        }
    }
}
