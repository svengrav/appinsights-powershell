using AppInsights.Exceptions;
using Newtonsoft.Json;
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
            foreach (DictionaryEntry dictionaryEntry in hashtable)
            {
                KeyIsOfTypeString(dictionaryEntry.Key);

                if (!double.TryParse(dictionaryEntry.Value.ToString(), out double value))
                    throw new HashtableInvalidException("Value has to be from type double or int.");

                dictionary.Add(CreateMeticKeyString(dictionaryEntry.Key), value);
            }

            return dictionary;
        }

        /// <summary>
        /// Creates a dictionary for custom parameters from a hashtable.
        /// </summary>
        public static IDictionary<string, string> ToPropertyDictionary(this Hashtable hashtable)
        {
            var dictionary = new Dictionary<string, string>()
            {
                { "customProperties", ConvertToJson(hashtable) }
            };
            //foreach (DictionaryEntry entry in hashtable)
            //    if (KeyIsTypeString(entry))
            //        dictionary.Add(CreatePropertyKeyString(entry.Key), CreatePropertyValueString(entry.Value));

            return dictionary;
        }

        private static string CreatePropertyValueString(object value)
            => value.ToString();

        private static string CreateMeticKeyString(object key)
            => key.ToString().ToLower();

        private static string CreatePropertyKeyString(object key)
            => key.ToString().ToLower();

        private static bool KeyIsOfTypeString(object key)
        {
            if (key.GetType() != typeof(string))
                throw new HashtableInvalidException("Key has to be from type string.");

            return true;
        }

        private static string ConvertToJson(object objectToConvert)
            => JsonConvert.SerializeObject(objectToConvert);
    }
}
