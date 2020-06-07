using CreateReactAppVS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Controller
{
    public static class GenerateBatchCmds
    {
        public static void GenerateBatchFiles(string location, ReactTemplate template)
        {
            try
            {
                CreateReactApp(location, template);
                BuildForProduction(location);
                DeleteNodeModulesFolder(location);
                NpmAuditFix(location);
                RestoreNodeModules(location);
                ServeBuild(location);
                StartDevWebServer(location);
            }
            catch (Exception)
            {
            }
        }

        private static void BuildForProduction(string location)
        {
            var file = Path.Combine(location, "_BuildForProduction.bat");

            var contents = @"
call npm run build
pause";

            System.IO.File.WriteAllText(file, contents);
        }

        private static void CreateReactApp(string location, ReactTemplate template)
        {
            var file = Path.Combine(location, "_CreateReactApp.bat");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@echo off");
            sb.AppendLine("");
            sb.AppendLine("cd /D \"%~dp0\"");
            sb.AppendLine("cd ..");
            sb.AppendLine("echo %cd%");
            sb.AppendLine(" ");
            sb.AppendLine(":: step 01: ask the user for an app name");
            sb.AppendLine("set /p appname=Enter a name for your app[lowercase only, no spaces]:");
            sb.AppendLine("::please enter a name [lowercase only, no spaces]:");
            sb.AppendLine("echo %appname%");
            sb.AppendLine("");
            sb.AppendLine(":: step 02: create the app");
            sb.AppendLine("echo calling npx create-react-app %appname% --template typescript");
            sb.AppendLine("call npx create-react-app %appname% --template typescript ");
            sb.AppendLine("");
            sb.AppendLine(":: step 03: cd into the new directory");
            sb.AppendLine("cd %appname%");
            sb.AppendLine("echo %cd%");
            sb.AppendLine("");

            foreach (var item in template.NpmPackageList)
            {
                if ( item.Included == true)
                    sb.AppendLine($"call {item.Name}");
            }
            sb.AppendLine("");
            sb.AppendLine(":: step 04: create some standard folders");
            sb.AppendLine("cd src");
            sb.AppendLine("echo %cd%");

            foreach (var item in template.ProjectFolderList)
            {
                if (item.Included == false)
                    continue;

                var folder = Path.Combine(template.GetProjectFolder(), item.Name);

                sb.AppendLine($"mkdir {folder}");
            }

            sb.AppendLine("");
            sb.AppendLine(":: step 06: finished!");
            sb.AppendLine("echo Finished");
            sb.AppendLine("set /p done=Press enter to finish");

            System.IO.File.WriteAllText(file, sb.ToString());
        }

        private static void DeleteNodeModulesFolder( string location )
        {
            var file = Path.Combine(location, "_Delete Node Modules Folder.bat");

            var contents = @"
@Echo Off

:: First make sure rimraf is installed
npm install rimraf -g

:: Then use it do delete the node_modules folder
rimraf node_modules

Echo Finished deleting node_modules fodler

pause";

            System.IO.File.WriteAllText(file, contents);
        }

        private static void NpmAuditFix(string location)
        {
            var file = Path.Combine(location, "_Npm Audit Fix.bat");

            var contents = @"
npm audit fix";

            System.IO.File.WriteAllText(file, contents);
        }

        private static void RestoreNodeModules(string location)
        {
            var file = Path.Combine(location, "_Restore Node Modules.bat");

            var contents = @"
@ECHO OFF

echo Step 01 - Deleteing node_modules

(if exist node_modules rmdir node_modules /q /s)

echo Step 02 - Now re-installing node modules

call npm install

pause
";

            System.IO.File.WriteAllText(file, contents);
        }

        private static void ServeBuild(string location)
        {
            var file = Path.Combine(location, "_Serve Release Build Web Server.bat");

            var contents = @"
:: This will serve the build directory out on port :5000 so you can run it.

serve -s build";

            System.IO.File.WriteAllText(file, contents);
        }

        private static void StartDevWebServer(string location)
        {
            var file = Path.Combine(location, "_Start Dev Web Server.bat");

            var contents = @"
call npm start
pause";

            System.IO.File.WriteAllText(file, contents);
        }
    }
}
