using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace AppInsights.Telemetry
{
    internal class TelemetryProcessor : ITelemetryProcessor
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetryProcessor(TelemetryInstrumentationKey instrumentationKey, bool developerMode)
        {
            _telemetryClient = CreateTelemetryClient(instrumentationKey, developerMode);
        }

        public TelemetryProcessor SetRoleName(string roleName)
        {
            _telemetryClient.Context.Cloud.RoleName = roleName;
            return this;
        }

        public TelemetryProcessor SetRoleInstance(string roleInstance)
        {
            _telemetryClient.Context.Cloud.RoleInstance = roleInstance;
            return this;
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

        private TelemetryClient CreateTelemetryClient(TelemetryInstrumentationKey instrumentationKey, bool developerMode)
        {
            var telemetryClient = new TelemetryClient(CreateTelemetryConfiguration(instrumentationKey, developerMode));
            AssignTelemetryClientRoles(telemetryClient);

            return telemetryClient;
        }

        private void AssignTelemetryClientRoles(TelemetryClient telemetryClient)
        {
            telemetryClient.Context.Cloud.RoleName = Environment.MachineName;
            telemetryClient.Context.Cloud.RoleInstance = Environment.MachineName;
        }

        private TelemetryConfiguration CreateTelemetryConfiguration(TelemetryInstrumentationKey instrumentationKey, bool developerMode)
        {
            var telemetryConfiguration = new TelemetryConfiguration();
            telemetryConfiguration.TelemetryChannel.DeveloperMode = developerMode;
            telemetryConfiguration.ConnectionString = $"InstrumentationKey={instrumentationKey.GetKey()}";
            return telemetryConfiguration;
        }
    }
}
