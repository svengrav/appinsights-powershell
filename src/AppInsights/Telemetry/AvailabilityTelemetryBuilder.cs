using AppInsights.Context;
using AppInsights.Utils;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class AvailabilityTelemetryBuilder
    {
        private readonly AvailabilityTelemetry _telemetry;
        private readonly CustomDimensions _customDimensions = new CustomDimensions();

        private AvailabilityTelemetryBuilder(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation)
        {
            _telemetry = new AvailabilityTelemetry(name, timeStamp, duration, runLocation, true);
            _telemetry.Extension = _customDimensions;
        }

        internal static AvailabilityTelemetryBuilder Create(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation)
            => new AvailabilityTelemetryBuilder(name, timeStamp, duration, runLocation);

        internal AvailabilityTelemetry Build()
             => _telemetry;

        internal AvailabilityTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal AvailabilityTelemetryBuilder AddHostContext(HostContext hostContext)
        {
            _customDimensions.AddHostContext(hostContext);
            return this;
        }

        internal AvailabilityTelemetryBuilder AddTimestamp(DateTimeOffset timestamp)
        {
            _telemetry.Timestamp = timestamp;
            return this;
        }

        internal AvailabilityTelemetryBuilder AddMessage(string message)
        {
            _telemetry.Message = message;
            return this;
        }

        internal AvailabilityTelemetryBuilder AddSuccess(bool success)
        {
            _telemetry.Success = success;
            return this;
        }


        internal AvailabilityTelemetryBuilder AddDuration(TimeSpan duration)
        {
            _telemetry.Duration = duration;
            return this;
        }

        internal AvailabilityTelemetryBuilder AddId(string id)
        {
            _telemetry.Id = id;
            return this;
        }

        internal AvailabilityTelemetryBuilder AddProperties(Hashtable properties)
        {
            _customDimensions.AddProperties(properties);
            return this;
        }

        internal AvailabilityTelemetryBuilder AddMetrics(Hashtable metrics)
        {
            _customDimensions.AddMetrics(metrics);
            return this;
        }
    }
}
