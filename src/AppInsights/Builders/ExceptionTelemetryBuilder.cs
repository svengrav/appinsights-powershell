using AppInsights.Context;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Builders
{
    public class ExceptionTelemetryBuilder
    {
        private readonly ExceptionTelemetry _telemetry;
        private readonly TelemetryCustomDimensions _customDimensions = new TelemetryCustomDimensions();

        private ExceptionTelemetryBuilder(Exception exception)
        {
            _telemetry = new ExceptionTelemetry(exception);
            _telemetry.Extension = _customDimensions;
        }

        internal static ExceptionTelemetryBuilder Create(Exception exception)
            => new ExceptionTelemetryBuilder(exception);

        internal ExceptionTelemetry Build()
             => _telemetry;

        internal ExceptionTelemetryBuilder AddPowerShellContext(PowerShellHostContext hostContext, PowerShellCommandContext commandContext)
        {
            _customDimensions.AddHostContext(hostContext);
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal ExceptionTelemetryBuilder AddMessage(string message)
        {
            _telemetry.Message = message;
            return this;
        }

        internal ExceptionTelemetryBuilder AddProblemId(string problemId)
        {
            _telemetry.ProblemId = problemId;
            return this;
        }

        internal ExceptionTelemetryBuilder AddTimestamp(DateTimeOffset timestamp)
        {
            _telemetry.Timestamp = timestamp;
            return this;
        }

        internal ExceptionTelemetryBuilder AddSeverity(SeverityLevel severity)
        {
            _telemetry.SeverityLevel = severity;
            return this;
        }

        internal ExceptionTelemetryBuilder AddProperties(Hashtable properties)
        {
            _customDimensions.AddProperties(properties);
            return this;
        }

        internal ExceptionTelemetryBuilder AddMetrics(Hashtable metrics)
        {
            _customDimensions.AddMetrics(metrics);
            return this;
        }
    }
}
