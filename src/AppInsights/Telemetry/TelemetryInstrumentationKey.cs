using AppInsights.Exceptions;
using System;

namespace AppInsights.Telemetry
{
    internal class TelemetryInstrumentationKey
    {
        private const string INSTRUMENTATION_KEY_VARIABLE = "AI_INSTRUMENTATION_KEY";

        private readonly Guid _instrumentationKey;

        internal TelemetryInstrumentationKey(Guid instrumentationKey)
        {
            if (IsEmpty(instrumentationKey))
                _instrumentationKey = TryGetEnviromentInstrumentationKey();
            else
                _instrumentationKey = instrumentationKey;
        }

        private Guid TryGetEnviromentInstrumentationKey()
        {
            if (Guid.TryParse(Environment.GetEnvironmentVariable(INSTRUMENTATION_KEY_VARIABLE), out var instrumentationKey))
                return instrumentationKey;

            throw new InvalidInstrumentationKeyException();
        }

        internal Guid GetKey()
            => _instrumentationKey;

        private static bool IsEmpty(Guid instrumentationKey)
            => instrumentationKey == Guid.Empty;
    }
}
