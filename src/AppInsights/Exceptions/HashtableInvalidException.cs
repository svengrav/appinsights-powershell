using System;

namespace AppInsights.Exceptions
{
    /// <summary>
    /// Exception is thrown if a invalid or missing key is used.
    /// </summary>
    public class HashtableInvalidException : Exception
    {
        public HashtableInvalidException(string message) : base(message)
        {

        }
    }
}
