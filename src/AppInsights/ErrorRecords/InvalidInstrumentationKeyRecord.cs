using System;
using System.Management.Automation;

namespace AppInsights.ErrorRecords
{
    public class InvalidInstrumentationKeyRecord : ErrorRecord
    {
        private const ErrorCategory _errorCategory = ErrorCategory.AuthenticationError;
        private const string _errorCode = "40100";

        public InvalidInstrumentationKeyRecord(Exception exception, object targetObject) : 
            base(exception, _errorCode, _errorCategory, targetObject)
        {

        }
    }
}
