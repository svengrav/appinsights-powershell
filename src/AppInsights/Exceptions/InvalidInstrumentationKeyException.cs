using System;

namespace AppInsights.Exceptions
{
    /// <summary>
    /// Exception is thrown if the instrumentation is invalid or missing.
    /// </summary>
    public class InvalidInstrumentationKeyException : Exception
    {
#pragma warning disable IDE1006 // Naming Styles
        private const string KEY_IS_INVALID = "Instrumentation Key is missing or not correct. Set the instrumentation key as an environment variable or add it as a parameter when calling the command.";
#pragma warning restore IDE1006 // Naming Styles

        public const int ERROR_CODE = 40100;

        public InvalidInstrumentationKeyException() : base(KEY_IS_INVALID)
        {

        }
    }
}
