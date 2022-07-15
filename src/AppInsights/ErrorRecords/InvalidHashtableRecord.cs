using System;
using System.Management.Automation;

namespace AppInsights.ErrorRecords
{
    public class InvalidHashtableRecord : ErrorRecord
    {
        private const ErrorCategory _errorCategory = ErrorCategory.AuthenticationError;
        private const string _errorCode = "40200";

        public InvalidHashtableRecord(Exception exception, object targetObject) : 
            base(exception, _errorCode, _errorCategory, targetObject)
        {

        }
    }
}
