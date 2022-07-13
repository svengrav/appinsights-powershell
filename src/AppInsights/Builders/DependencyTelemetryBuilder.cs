using AppInsights.Context;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Builders
{
    public class DependencyTelemetryBuilder
    {
        private readonly DependencyTelemetry _telemetry;
        private readonly TelemetryCustomDimensions _customDimensions = new TelemetryCustomDimensions();

        private DependencyTelemetryBuilder(string dependencyTypeName, string target, string dependencyName, string data)
        {
            _telemetry = new DependencyTelemetry(dependencyTypeName, target, dependencyName, data);
            _telemetry.Extension = _customDimensions;
        }

        internal static DependencyTelemetryBuilder Create(string dependencyTypeName, string target, string dependencyName, string data)
            => new DependencyTelemetryBuilder(dependencyTypeName, target, dependencyName, data);

        internal DependencyTelemetry Build()
             => _telemetry;

        internal DependencyTelemetryBuilder AddPowerShellContext(PowerShellHostContext hostContext, PowerShellCommandContext commandContext)
        {
            _customDimensions.AddHostContext(hostContext);
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal DependencyTelemetryBuilder AddStartTime(DateTimeOffset timestamp)
        {
            _telemetry.Timestamp = timestamp;
            return this;
        }

        internal DependencyTelemetryBuilder AddResultCode(string resultCode)
        {
            _telemetry.ResultCode = resultCode;
            return this;
        }

        internal DependencyTelemetryBuilder AddSuccess(bool success)
        {
            _telemetry.Success = success;
            return this;
        }

        internal DependencyTelemetryBuilder AddDuration(TimeSpan duration)
        {
            _telemetry.Duration = duration;
            return this;
        }

        internal DependencyTelemetryBuilder AddProperties(Hashtable properties)
        {
            _customDimensions.AddProperties(properties);
            return this;
        }

        internal DependencyTelemetryBuilder AddMetrics(Hashtable metrics)
        {
            _customDimensions.AddMetrics(metrics);
            return this;
        }
    }
}
