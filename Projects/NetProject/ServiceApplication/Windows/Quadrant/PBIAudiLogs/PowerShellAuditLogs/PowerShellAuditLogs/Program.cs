using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAuditLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            var sessionName = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssZ")+ "pbiauditlog";


            string password = "Hyderabad65!";
            string userName = "ganesh@quadrantresource.com";
            System.Uri uri = new Uri("https://outlook.office365.com/powershell-liveid/");
            System.Security.SecureString securePassword = String2SecureString(password);

            PSCredential creds = new PSCredential(userName, securePassword);

            Runspace runspace = RunspaceFactory.CreateRunspace();

            PowerShell powershell = PowerShell.Create();
            PSCommand command = new PSCommand();
            command.AddCommand("Get-PSSession");
            command.AddParameter("ConfigurationName", "Microsoft.Exchange");
            command.AddParameter("ConnectionUri", uri);
            command.AddParameter("Credential", creds);
            command.AddParameter("Authentication", "Basic");
            command.AddParameter("AllowRedirection", true);

            PSSessionOption sessionOption = new PSSessionOption();
            sessionOption.SkipCACheck = true;
            sessionOption.SkipCNCheck = true;
            sessionOption.SkipRevocationCheck = true;
            command.AddParameter("SessionOption", sessionOption);

            powershell.Commands = command;

            try
            {
                // open the remote runspace
                runspace.Open();

                // associate the runspace with powershell
                powershell.Runspace = runspace;

                // invoke the powershell to obtain the results
                Collection<PSSession> result = powershell.Invoke<PSSession>();

                foreach (ErrorRecord current in powershell.Streams.Error)
                {
                    Console.WriteLine("Exception: " + current.Exception.ToString());
                    Console.WriteLine("Inner Exception: " + current.Exception.InnerException);
                }

                if (result.Count != 1)
                    throw new Exception("Unexpected number of Remote Runspace connections returned.");

                // Set the runspace as a local variable on the runspace
                powershell = PowerShell.Create();
                command = new PSCommand();
                command.AddCommand("Set-Variable");
                command.AddParameter("Name", "ra");
                command.AddParameter("Value", result[0]);
                powershell.Commands = command;
                powershell.Runspace = runspace;

                powershell.Invoke();


                // First import the cmdlets in the current runspace (using Import-PSSession)
                powershell = PowerShell.Create();
                command = new PSCommand();
                command.AddScript("Import-PSSession -Session $ra");
                powershell.Commands = command;
                powershell.Runspace = runspace;
                powershell.Invoke();


                // Now run get-ExchangeServer
                powershell = PowerShell.Create();
                command = new PSCommand();
                command.AddScript("Search-UnifiedAuditLog -StartDate 01/01/2019 -EndDate 03/31/2019 -SessionId" + sessionName + " -SessionCommand ReturnLargeSet -ResultSize 1000 -RecordType PowerBI");
                powershell.Commands = command;
                powershell.Runspace = runspace;

                Collection<PSObject> results = new Collection<PSObject>();
                results = powershell.Invoke();

                foreach (PSObject PSresult in results)
                {
                    Console.WriteLine(PSresult.Properties["RecordType"].Value.ToString());
                }

            }
            finally
            {
                // dispose the runspace and enable garbage collection
                runspace.Dispose();
                runspace = null;

                // Finally dispose the powershell and set all variables to null to free
                // up any resources.
                powershell.Dispose();
                powershell = null;
            }
        }

        private static SecureString String2SecureString(string password)
        {
            SecureString remotePassword = new SecureString();
            for (int i = 0; i < password.Length; i++)
                remotePassword.AppendChar(password[i]);

            return remotePassword;
        }

    }
}
