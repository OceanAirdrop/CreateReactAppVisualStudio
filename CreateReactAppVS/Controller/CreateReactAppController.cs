using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Controller
{
    public static class CreateReactAppController
    {
        public static void CreateReactApp(string appName = "abcdefghikl", string folder = @"C:\OceanAirdrop\SetupReactApp\SetupReactApp\bin\Debug")
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "npx.cmd";
            startInfo.Arguments = $"create-react-app {appName} --template typescript";
            startInfo.WorkingDirectory = folder;

            Log.Information($"{startInfo.FileName} {startInfo.Arguments}");

            var proc = new System.Diagnostics.Process();
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
        }

        public static void NpmAuditFix(string folder = @"C:\OceanAirdrop\SetupReactApp\SetupReactApp\bin\Debug")
        {
            Log.Information("npm audit fix");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "npm.cmd";
            startInfo.Arguments = $"audit fix";
            startInfo.WorkingDirectory = folder;

            var proc = new System.Diagnostics.Process();
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
        }

        public static void NpmInstallPackage(string cmd, string arguments, string folder = @"C:\OceanAirdrop\SetupReactApp\SetupReactApp\bin\Debug")
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "npm.cmd";
            startInfo.Arguments = arguments;
            startInfo.WorkingDirectory = folder;

            Log.Information($"{startInfo.FileName} {startInfo.Arguments}");

            var proc = new System.Diagnostics.Process();
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
        }
    }
}
