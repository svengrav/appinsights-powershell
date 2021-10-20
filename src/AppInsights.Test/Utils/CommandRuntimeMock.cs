using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights.Test
{
    public class CommandRuntimeMock : ICommandRuntime
    {
        private readonly CommandResult _commandResult;

        public CommandRuntimeMock(CommandResult commandResult)
        {
            _commandResult = commandResult;
        }

        public bool ShouldContinue(string query, string caption)
        {
            return true;
        }

        public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll)
        {
            return true;
        }

        public bool ShouldProcess(string target)
        {
            return true;
        }

        public bool ShouldProcess(string target, string action)
        {
            return true;
        }

        public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption)
        {
            return true;
        }

        public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out ShouldProcessReason shouldProcessReason)
        {
            shouldProcessReason = ShouldProcessReason.None;
            return true;
        }

        public void ThrowTerminatingError(ErrorRecord errorRecord)
        {
            _commandResult.Errors.Add(errorRecord);
            throw errorRecord.Exception;
        }

        public bool TransactionAvailable()
        {
            return false;
        }

        public void WriteCommandDetail(string text)
        {
        }

        public void WriteDebug(string text)
        {
            _commandResult.Debugs.Add(text);
        }

        public void WriteError(ErrorRecord errorRecord)
        {
            _commandResult.Errors.Add(errorRecord);
        }

        public void WriteObject(object sendToPipeline)
        {
            _commandResult.Output.Add(sendToPipeline);
        }

        public void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
            if (!enumerateCollection)
            {
                _commandResult.Output.Add(sendToPipeline);
            }
            else
            {
                IEnumerator enumerator = LanguagePrimitives.GetEnumerator(sendToPipeline);
                if (enumerator != null)
                {
                    while (enumerator.MoveNext())
                    {
                        _commandResult.Output.Add(enumerator.Current);
                    }
                }
                else
                {
                    _commandResult.Output.Add(sendToPipeline);
                }
            }
        }

        public void WriteProgress(ProgressRecord progressRecord)
        {
            _commandResult.Progresses.Add(progressRecord);
        }

        public void WriteProgress(long sourceId, ProgressRecord progressRecord)
        {
            _commandResult.Progresses.Add(progressRecord);
        }

        public void WriteVerbose(string text)
        {
            _commandResult.Verboses.Add(text);
        }

        public void WriteWarning(string text)
        {
            _commandResult.Warnings.Add(text);
        }

        public PSTransactionContext CurrentPSTransaction
        {
            get { throw new NotImplementedException(); }
        }

        public System.Management.Automation.Host.PSHost Host
        {
            get { throw new NotImplementedException(); }
        }
    }


}