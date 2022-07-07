using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AppInsights.Test
{
    public class TelemetryRepository
    {
        public static Hashtable PropertiesHashtable = new Hashtable()
        {
            { "Fruit", "Apple"},
            { "Type", "Granny Smith"}
        };

        public static Hashtable MetricsHashtable = new Hashtable()
        {
            { "Weight", 12},
            { "Size", 3}
        };

        public static Dictionary<string, string> PropertiesDictionary = new Dictionary<string, string>()
        {
            { "Fruit", "Apple"},
            { "Type", "Granny Smith"}
        };

        public static Dictionary<string, double> MetricsDictionary = new Dictionary<string, double>()
        {
            { "Weight", 12},
            { "Size", 3}
        };

        public static SeverityLevel SeverityLevel = SeverityLevel.Warning;

        public static TraceTelemetry CreateTraceTelemetry()
        {
            var telemetry = new TraceTelemetry()
            {
                Message = "TraceMessage",
                SeverityLevel = SeverityLevel
            };

            foreach (var property in PropertiesDictionary)
                telemetry.Properties.Add(property);

            return telemetry;
        }

        public static EventTelemetry CreateEventTelemetry()
        {
            var telemetry = new EventTelemetry()
            {
                Name = "AppleOrdered",
                Timestamp = DateTime.MaxValue
            };
            
            foreach(var property in PropertiesDictionary)
                telemetry.Properties.Add(property);

            foreach (var metric in MetricsDictionary)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static ExceptionTelemetry CreateExceptionTelemetry()
        {
            var telemetry = new ExceptionTelemetry()
            {
                Exception = new Exception("Apple is rotten."),
                SeverityLevel = SeverityLevel.Error,
                ProblemId = "4000",
                Timestamp = DateTime.MaxValue,
                Message = "Apple is rotten exception was thrown."
            };

            foreach (var property in PropertiesDictionary)
                telemetry.Properties.Add(property);

            foreach (var metric in MetricsDictionary)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static RequestTelemetry CreateRequestTelemetry()
        {
            var telemetry = new RequestTelemetry()
            {
                Name = "GET APPLE",
                Duration = new TimeSpan(0, 1, 0),
                ResponseCode = "200",
                Success = true,
                Timestamp = DateTime.MaxValue,
                Source = "Appletree",
                Url = new Uri("https://www.tree.com/apple")
            };

            foreach (var property in PropertiesDictionary)
                telemetry.Properties.Add(property);

            foreach (var metric in MetricsDictionary)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static DependencyTelemetry CreateDependencyTelemetry()
        {
            var telemetry = new DependencyTelemetry()
            {
                Id = "b6c12a41-d4dc-4b32-af46-8d918562bd2b",
                Data = "Apple has an dependency on a tree.",
                Duration = new TimeSpan(0, 1, 0),
                ResultCode = "200",
                Timestamp = DateTime.MaxValue,
                Success = true,
                Target = "Appletree"
            };

            foreach (var property in PropertiesDictionary)
                telemetry.Properties.Add(property);

            foreach (var metric in MetricsDictionary)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static AvailabilityTelemetry CreateAvailabilityTelemetry()
        {
            var telemetry = new AvailabilityTelemetry()
            {
                Name = "Apple Shop Availability Test",
                Id = "AppleId",
                RunLocation = "AppleShopServer",
                Message = "Apple Shop Server test was successfull.",
                Success  = true,
                Timestamp = DateTimeOffset.MaxValue,
                Duration = new TimeSpan(0, 1, 0),
            };

            foreach (var property in PropertiesDictionary)
                telemetry.Properties.Add(property);

            foreach (var metric in MetricsDictionary)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

    }
}
