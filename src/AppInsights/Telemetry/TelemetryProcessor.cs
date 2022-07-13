using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace AppInsights.Telemetry
{
    internal class TelemetryProcessor : ITelemetryProcessor
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly string _roleName;
        private readonly string _roleInstance;

        public TelemetryProcessor(TelemetryInstrumentationKey instrumentationKey, string roleName = null, string roleInstance = null)
        {
            _roleName = roleName ?? Environment.MachineName;
            _roleInstance = roleInstance ?? Environment.MachineName;
            _telemetryClient = CreateTelemetryClient(instrumentationKey);
        }

        public void TrackAvailability(AvailabilityTelemetry telemetry)
        {
            _telemetryClient.TrackAvailability(telemetry);
            _telemetryClient.Flush();
        }

        public void TrackDependency(DependencyTelemetry telemetry)
        {
            _telemetryClient.TrackDependency(telemetry);
            _telemetryClient.Flush();
        }

        public void TrackEvent(EventTelemetry telemetry)
        {
            _telemetryClient.TrackEvent(telemetry);
            _telemetryClient.Flush();
        }

        public void TrackException(ExceptionTelemetry telemetry)
        {
            _telemetryClient.TrackException(telemetry);
            _telemetryClient.Flush();
        }

        public void TrackMetric(MetricTelemetry telemetry)
        {
            _telemetryClient.TrackMetric(telemetry);
            _telemetryClient.Flush();
        }

        public void TrackRequest(RequestTelemetry telemetry)
        {
            _telemetryClient.TrackRequest(telemetry);
            _telemetryClient.Flush();
        }

        public void TrackTrace(TraceTelemetry telemetry)
        {
            _telemetryClient.TrackTrace(telemetry);
            _telemetryClient.Flush();
        }

        private TelemetryClient CreateTelemetryClient(TelemetryInstrumentationKey instrumentationKey)
        {
            var telemetryClient = new TelemetryClient(CreateTelemetryConfiguration(instrumentationKey));
            AssignTelemetryClientRoles(telemetryClient);

            return telemetryClient;
        }

        private void AssignTelemetryClientRoles(TelemetryClient telemetryClient)
        {
            telemetryClient.Context.Cloud.RoleName = _roleName;
            telemetryClient.Context.Cloud.RoleInstance = _roleInstance;
        }

        private TelemetryConfiguration CreateTelemetryConfiguration(TelemetryInstrumentationKey instrumentationKey)
        {
            var options = new TelemetryConfiguration()
            {
                ConnectionString = $"InstrumentationKey={instrumentationKey.GetKey()}",
            };
            return options;
        }
    }
}
