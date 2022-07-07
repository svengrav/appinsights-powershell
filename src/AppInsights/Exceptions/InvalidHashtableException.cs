using System;

namespace AppInsights.Exceptions
{
    /// <summary>
    /// Exception is thrown if a invalid or missing key is used.
    /// </summary>
    public class InvalidHashtableException : Exception
    {
        public InvalidHashtableException(string message) : base(message)
        {

        }
    }
}
