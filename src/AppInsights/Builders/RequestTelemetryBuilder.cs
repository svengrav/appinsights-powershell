using AppInsights.Context;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Builders
{
    public class RequestTelemetryBuilder
    {
        private readonly RequestTelemetry _telemetry;
        private readonly TelemetryCustomDimensions _customDimensions = new TelemetryCustomDimensions();

        private RequestTelemetryBuilder(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            _telemetry = new RequestTelemetry(name, startTime, duration, responseCode, success);
            _telemetry.Extension = _customDimensions.GetFormatter();
        }

        internal static RequestTelemetryBuilder Create(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
            => new RequestTelemetryBuilder(name, startTime, duration, responseCode, success);

        internal RequestTelemetry Build()
             => _telemetry;

        internal RequestTelemetryBuilder AddPowerShellContext(PowerShellHostContext hostContext, PowerShellCommandContext commandContext)
        {
            _customDimensions.AddHostContext(hostContext);
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal RequestTelemetryBuilder AddTimestamp(DateTimeOffset timestamp)
        {
            _telemetry.Timestamp = timestamp;
            return this;
        }

        internal RequestTelemetryBuilder AddDuration(TimeSpan duration)
        {
            _telemetry.Duration = duration;
            return this;
        }

        internal RequestTelemetryBuilder AddId(string id)
        {
            _telemetry.Id = id;
            return this;
        }

        internal RequestTelemetryBuilder AddSource(string source)
        {
            _telemetry.Source = source;
            return this;
        }

        internal RequestTelemetryBuilder AddResponseCode(string responseCode)
        {
            _telemetry.ResponseCode = responseCode;
            return this;
        }

        internal RequestTelemetryBuilder AddSuccess(bool success)
        {
            _telemetry.Success = success;
            return this;
        }

        internal RequestTelemetryBuilder AddProperties(Hashtable properties)
        {
            _customDimensions.AddProperties(properties);
            return this;
        }

        internal RequestTelemetryBuilder AddMetrics(Hashtable metrics)
        {
            _customDimensions.AddMetrics(metrics);
            return this;
        }

        internal RequestTelemetryBuilder AddUrl(string uri)
        {
            _telemetry.Url = new Uri(uri);
            return this;
        }
    }
}
