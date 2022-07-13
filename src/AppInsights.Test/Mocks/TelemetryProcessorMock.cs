using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;

namespace AppInsights.Test
{
    public class TelemetryProcessorMock : ITelemetryProcessor
    {
        public TraceTelemetry TraceTelemetry;

        public DependencyTelemetry DependencyTelemetry;

        public EventTelemetry EventTelemetry;

        public AvailabilityTelemetry AvailabilityTelemetry;

        public ExceptionTelemetry ExceptionTelemetry;

        public MetricTelemetry MetricTelemetry;

        public RequestTelemetry RequestTelemetry;

        public void TrackAvailability(AvailabilityTelemetry telemetry)
        {
            AvailabilityTelemetry = telemetry;
        }

        public void TrackDependency(DependencyTelemetry telemetry)
        {
            DependencyTelemetry = telemetry;
        }

        public void TrackEvent(EventTelemetry telemetry)
        {
            EventTelemetry = telemetry;
        }

        public void TrackException(ExceptionTelemetry telemetry)
        {
            ExceptionTelemetry = telemetry;
        }

        public void TrackMetric(MetricTelemetry telemetry)
        {
            MetricTelemetry = telemetry;
        }

        public void TrackRequest(RequestTelemetry telemetry)
        {
            RequestTelemetry = telemetry;
        }

        public void TrackTrace(TraceTelemetry telemetry)
        {
            TraceTelemetry = telemetry;
        }
    }
}
