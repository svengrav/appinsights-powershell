using Microsoft.ApplicationInsights.DataContracts;

namespace AppInsights.Telemetry
{
    public interface ITelemetryProcessor
    {
        void TrackEvent(EventTelemetry telemetry);
        void TrackTrace(TraceTelemetry telemetry);
        void TrackMetric(MetricTelemetry telemetry);
        void TrackException(ExceptionTelemetry telemetry);
        void TrackDependency(DependencyTelemetry telemetry);
        void TrackAvailability(AvailabilityTelemetry telemetry);
        void TrackRequest(RequestTelemetry request);
    }
}
