using System;

namespace AppInsights.Exceptions
{
    /// <summary>
    /// Exception is thrown if a invalid or missing key is used.
    /// </summary>
    public class InvalidHashtableException : Exception
    {
        public const int ERROR_CODE = 40150;

        public InvalidHashtableException(string message) : base(message)
        {

        }
    }
}
