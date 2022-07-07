using AppInsights.Extensions;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class DependencyTelemetryBuilder
    {
        private readonly DependencyTelemetry _telemetry;

        private DependencyTelemetryBuilder(string dependencyTypeName, string target, string dependencyName, string data)
        {
            _telemetry = new DependencyTelemetry(dependencyTypeName, target, dependencyName, data);
        }

        internal static DependencyTelemetryBuilder Create(string dependencyTypeName, string target, string dependencyName, string data)
            => new DependencyTelemetryBuilder(dependencyTypeName, target, dependencyName, data);

        internal DependencyTelemetry Build()
             => _telemetry;

        internal DependencyTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _telemetry.Extension = commandContext;
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
            _telemetry.Properties.MergeDictionary(properties.ToPropertyDictionary());
            return this;
        }

        internal DependencyTelemetryBuilder AddMetrics (Hashtable metrics)
        {
            _telemetry.Metrics.MergeDictionary(metrics.ToMetricDictionary());
            return this;
        }
    }
}
