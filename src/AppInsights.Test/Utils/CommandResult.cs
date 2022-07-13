using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AppInsights.Test
{
    public class CommandResult
    {
        public readonly List<object> Output = new List<object>();
        public readonly List<string> Warnings = new List<string>();
        public readonly List<string> Debugs = new List<string>();
        public readonly List<string> Verboses = new List<string>();
        public readonly List<ErrorRecord> Errors = new List<ErrorRecord>();
        public readonly List<ProgressRecord> Progresses = new List<ProgressRecord>();

        public IEnumerable<T> Cast<T>()
            => Output.Cast<T>();
    }
}