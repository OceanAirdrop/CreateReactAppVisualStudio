using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Controller
{
    public static class CommandLine
    {
        public static string ExecuteCommandSync(string command)
        {
            var result = "";

            try
            {
                Log.Information($"Cmd: {command}");

                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.

                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;

                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;

                // Now we create a process, assign its ProcessStartInfo and start it
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                result = proc.StandardOutput.ReadToEnd();

                // Display the command output.
                Console.WriteLine(result);

                Log.Information($"Result: {result}");
            }
            catch (Exception objException)
            {
                // Log the exception
            }

            return result;
        }
    }
}
