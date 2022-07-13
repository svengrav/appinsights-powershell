using System;

namespace AppInsights.Exceptions
{
    /// <summary>
    /// Exception is thrown if a invalid or missing key is used.
    /// </summary>
    public class InvalidInstrumentationKeyException : Exception
    {
        public const int ERROR_CODE = 40100;

        private const string KEY_IS_INVALID = "Instrumentation Key is missing or not correct. Set the instrumentation key as an environment variable or add it as a parameter when calling the command.";

        public InvalidInstrumentationKeyException() : base(KEY_IS_INVALID)
        {

        }
    }
}
