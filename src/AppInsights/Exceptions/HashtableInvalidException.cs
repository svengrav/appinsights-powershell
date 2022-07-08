using System;

namespace AppInsights.Exceptions
{
    /// <summary>
    /// Exception is thrown if a invalid or missing key is used.
    /// </summary>
    public class HashtableInvalidException : Exception
    {
        public const int ERROR_CODE = 40150;

        public HashtableInvalidException(string message) : base(message)
        {

        }
    }
}
