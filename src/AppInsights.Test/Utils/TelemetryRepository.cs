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
            { "PropertyKey1", "Property Value 1"},
            { "PropertyKey2", "Property Value 2"}
        };

        public static Hashtable MetricsHashtable = new Hashtable()
        {
            { "PropertyKey1", 1},
            { "PropertyKey2", 2}
        };

        public static Dictionary<string, string> Properties = new Dictionary<string, string>()
        {
            { "PropertyKey1", "Property Value 1"},
            { "PropertyKey2", "Property Value 2"}
        };

        public static Dictionary<string, double> Metrics = new Dictionary<string, double>()
        {
            { "PropertyKey1", 1},
            { "PropertyKey2", 2}
        };

        public static SeverityLevel SeverityLevel = SeverityLevel.Warning;

        public static TraceTelemetry CreateTraceTelemetry()
        {
            var telemetry = new TraceTelemetry()
            {
                Message = "TraceMessage",
                SeverityLevel = SeverityLevel
            };

            foreach (var property in Properties)
                telemetry.Properties.Add(property);

            return telemetry;
        }

        public static EventTelemetry CreateEventTelemetry()
        {
            var telemetry = new EventTelemetry()
            {
                Name = "EventName1"
            };
            
            foreach(var property in Properties)
                telemetry.Properties.Add(property);

            foreach (var metric in Metrics)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static ExceptionTelemetry CreateExceptionTelemetry()
        {
            var telemetry = new ExceptionTelemetry()
            {
                Exception = new Exception("Exception1"),
                SeverityLevel = SeverityLevel
            };

            foreach (var property in Properties)
                telemetry.Properties.Add(property);

            foreach (var metric in Metrics)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static RequestTelemetry CreateRequestTelemetry()
        {
            var telemetry = new RequestTelemetry()
            {
                
                
            };

            foreach (var property in Properties)
                telemetry.Properties.Add(property);

            foreach (var metric in Metrics)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

        public static DependencyTelemetry CreateDependencyTelemetry()
        {
            var telemetry = new DependencyTelemetry()
            {
                

            };

            foreach (var property in Properties)
                telemetry.Properties.Add(property);

            foreach (var metric in Metrics)
                telemetry.Metrics.Add(metric);

            return telemetry;
        }

    }
}
