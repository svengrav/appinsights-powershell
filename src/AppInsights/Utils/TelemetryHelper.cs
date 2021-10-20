using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace AppInsights.Utils
{
    public static class TelemetryHelper
    {
        /// <summary>
        /// Creates a new telemetry client.
        /// </summary>
        public static TelemetryClient CreateTelemetryClient(Guid instrumentationKey, string roleName = null, string roleInstance = null)
        {
            if (string.IsNullOrEmpty(roleName))
                roleName = Environment.MachineName;

            if (string.IsNullOrEmpty(roleName))
                roleInstance = Environment.MachineName;

            var options = new TelemetryConfiguration()
            {
                ConnectionString = $"InstrumentationKey={instrumentationKey}",
            };
            options.TelemetryChannel.DeveloperMode = true;

            var telemetryClient = new TelemetryClient(options);

            telemetryClient.Context.Cloud.RoleName = roleName;
            telemetryClient.Context.Cloud.RoleInstance = roleInstance;

            return telemetryClient;
        }
    }
}
