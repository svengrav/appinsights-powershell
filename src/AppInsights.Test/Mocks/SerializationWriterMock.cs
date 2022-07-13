using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;

namespace AppInsights.Test.Mocks
{
    internal class SerializationWriterMock : ISerializationWriter
    {
        internal Dictionary<string, string> StringProperties { get; set; } = new Dictionary<string, string>();
        internal Dictionary<string, int?> IntegerProperties { get; set; } = new Dictionary<string, int?>();
        internal Dictionary<string, double?> DoubleProperties { get; set; } = new Dictionary<string, double?>();
        internal Dictionary<string, bool?> BooleanProperties { get; set; } = new Dictionary<string, bool?>();
        internal Dictionary<string, TimeSpan?> TimespanProperties { get; set; } = new Dictionary<string, TimeSpan?>();
        internal Dictionary<string, DateTimeOffset?> DateTimeOffsetProperties { get; set; } = new Dictionary<string, DateTimeOffset?>();

        public void WriteEndObject()
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(string name, string value)
        {
            StringProperties.Add(name, value);
        }

        public void WriteProperty(string name, double? value)
        {
            DoubleProperties.Add(name, value);
        }

        public void WriteProperty(string name, int? value)
        {
            IntegerProperties.Add(name, value);
        }

        public void WriteProperty(string name, bool? value)
        {
            BooleanProperties.Add(name, value);
        }

        public void WriteProperty(string name, TimeSpan? value)
        {
            TimespanProperties.Add(name, value);
        }

        public void WriteProperty(string name, DateTimeOffset? value)
        {
            DateTimeOffsetProperties.Add(name, value);
        }

        public void WriteProperty(string name, ISerializableWithWriter value)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(ISerializableWithWriter value)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(string name, IList<string> items)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(string name, IList<ISerializableWithWriter> items)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(string name, IDictionary<string, string> items)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(string name, IDictionary<string, double> items)
        {
            throw new NotImplementedException();
        }

        public void WriteStartObject(string name)
        {
            throw new NotImplementedException();
        }

        public void WriteStartObject()
        {
            throw new NotImplementedException();
        }
    }
}
