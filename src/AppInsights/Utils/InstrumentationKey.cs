using AppInsights.Exceptions;
using System;

namespace AppInsights.Utils
{
    internal class InstrumentationKey
    {
        private const string INSTRUMENTATION_KEY = "AI_INSTRUMENTATION_KEY";
        private readonly Guid _instrumentationKey;

        internal InstrumentationKey(Guid instrumentationKey)
        {
            if (IsEmpty(instrumentationKey))
                _instrumentationKey = TryGetEnviromentInstrumentationKey();
            else
                _instrumentationKey = instrumentationKey;
        }

        private Guid TryGetEnviromentInstrumentationKey()
        {
            if (Guid.TryParse(Environment.GetEnvironmentVariable(INSTRUMENTATION_KEY), out Guid instrumentationKey))
                return instrumentationKey;

            throw new InstrumentationKeyInvalidException();
        }

        internal Guid GetKey() 
            => _instrumentationKey;

        private static bool IsEmpty(Guid instrumentationKey)
            => instrumentationKey == Guid.Empty;
    }
}
