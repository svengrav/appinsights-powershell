using AppInsights.Exceptions;
using System;

namespace AppInsights.Telemetry
{
    internal class TelemetryInstrumentationKey
    {
#pragma warning disable IDE1006 // Naming Styles
        private const string INSTRUMENTATION_KEY_VARIABLE = "AI_INSTRUMENTATION_KEY";
#pragma warning restore IDE1006 // Naming Styles

        private readonly Guid _instrumentationKey;

        internal TelemetryInstrumentationKey(Guid instrumentationKey)
        {
            if (HasValue(instrumentationKey))
                _instrumentationKey = instrumentationKey;
            else
                _instrumentationKey = TryGetEnviromentInstrumentationKey();
        }

        private Guid TryGetEnviromentInstrumentationKey()
        {
            if (Guid.TryParse(Environment.GetEnvironmentVariable(INSTRUMENTATION_KEY_VARIABLE), out var instrumentationKey))
                return instrumentationKey;

            throw new InvalidInstrumentationKeyException();
        }

        internal Guid GetKey()
            => _instrumentationKey;

        private static bool HasValue(Guid instrumentationKey)
            => instrumentationKey != Guid.Empty;
    }
}
