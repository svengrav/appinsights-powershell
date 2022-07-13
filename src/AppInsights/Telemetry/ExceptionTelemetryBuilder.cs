using AppInsights.Context;
using AppInsights.Utils;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class ExceptionTelemetryBuilder
    {
        private readonly ExceptionTelemetry _telemetry;
        private readonly CustomDimensions _customDimensions = new CustomDimensions();

        private ExceptionTelemetryBuilder(Exception exception)
        {
            _telemetry = new ExceptionTelemetry(exception);
            _telemetry.Extension = _customDimensions;
        }

        internal static ExceptionTelemetryBuilder Create(Exception exception)
            => new ExceptionTelemetryBuilder(exception);

        internal ExceptionTelemetry Build()
             => _telemetry;

        internal ExceptionTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal ExceptionTelemetryBuilder AddHostContext(HostContext hostContext)
        {
            _customDimensions.AddHostContext(hostContext);
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
