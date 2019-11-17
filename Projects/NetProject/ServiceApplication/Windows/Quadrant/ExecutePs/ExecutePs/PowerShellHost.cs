using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace ExecutePs
{
    public class PowerShellHost
    {
        private Dictionary<string, Runspace> _runspaceCache = new Dictionary<string, Runspace>();

        ~PowerShellHost()
        {
            Clean();
        }

        public Collection<PSObject> ExecuteScriptFile(string scriptFilePath, IEnumerable<object> arguments = null, string machineAddress = null)
        {
            return ExecuteScript(File.ReadAllText(scriptFilePath), arguments, machineAddress);
        }

        public Collection<PSObject> ExecuteScript(string script, IEnumerable<object> arguments = null, string machineAddress = null)
        {

            Runspace runspace = GetOrCreateRunspace(machineAddress);
            using (System.Management.Automation.PowerShell ps = System.Management.Automation.PowerShell.Create())
            {
                ps.Runspace = runspace;

                ps.AddScript(script);

                if (arguments != null)
                {
                    foreach (var argument in arguments)
                    {
                        ps.AddArgument(argument);
                    }
                }

                return ps.Invoke();
            }
        }

        public void Dispose()
        {
            Clean();
            GC.SuppressFinalize(this);
        }

        private Runspace GetOrCreateLocalRunspace()
        {
            if (!_runspaceCache.ContainsKey("localhost"))
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                _runspaceCache.Add("localhost", runspace);
            }

            return _runspaceCache["localhost"];
        }

        private Runspace GetOrCreateRunspace(string machineAddress)
        {
            if (string.IsNullOrWhiteSpace(machineAddress))
            {
                return GetOrCreateLocalRunspace();
            }

            machineAddress = machineAddress.ToLowerInvariant();
            if (!_runspaceCache.ContainsKey(machineAddress))
            {
                var uri = string.Format("http://{0}:5985/wsman", machineAddress);
                WSManConnectionInfo connectionInfo = new WSManConnectionInfo(new Uri(uri));
                connectionInfo.OpenTimeout = 3000;
                connectionInfo.OperationTimeout = 4000;
                Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo);
                runspace.Open();
                _runspaceCache.Add(machineAddress, runspace);
            }

            return _runspaceCache[machineAddress];
        }

        private void Clean()
        {
            foreach (var runspaceEntry in _runspaceCache)
            {
                runspaceEntry.Value.Close();
            }

            _runspaceCache.Clear();
        }
    }
}
