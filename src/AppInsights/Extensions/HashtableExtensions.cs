using AppInsights.Exceptions;
using System.Collections;
using System.Collections.Generic;

namespace AppInsights.Extensions
{
    public static class HashtableExtensions
    {
        /// <summary>
        /// Creates a dictionary for metrics from a hashtable.
        /// </summary>
        public static IDictionary<string, double> ToMetricDictionary(this Hashtable hashtable)
        {
            var dictionary = new Dictionary<string, double>();
            foreach (DictionaryEntry entry in hashtable)
            {
                ThrowIfKeyNotString(entry);

                if (!double.TryParse(entry.Value.ToString(), out double value))
                    throw new HashtableInvalidException("Value has to be from type double or int.");

                dictionary.Add(entry.Key.ToString(), value);
            }

            return dictionary;
        }

        /// <summary>
        /// Creates a dictionary for custom parameters from a hashtable.
        /// </summary>
        public static IDictionary<string, string> ToPropertyDictionary(this Hashtable hashtable)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in hashtable)
            {
                ThrowIfKeyNotString(entry);

                dictionary.Add(entry.Key.ToString(), entry.Value.ToString());
            }

            return dictionary;
        }

        private static void ThrowIfKeyNotString(DictionaryEntry entry)
        {
            if (entry.Key.GetType() != typeof(string))
                throw new HashtableInvalidException("Key has to be from type string.");
        }
    }
}
