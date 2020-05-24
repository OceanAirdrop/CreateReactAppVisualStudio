using CreateReactAppVS.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Controller
{
    public static class GenVisualStudioSolution
    {
        public static string Generate(string name, string location )
        {
            // Need to replace these in the solution.sln
            var projectFile  = EmbeddedResourceUtils.GetAppResource("project.njsproj");
            var solutionFile = EmbeddedResourceUtils.GetAppResource("solution.sln");

            // {{app_name}}
            // {{project_location}}

            solutionFile = solutionFile.Replace("{{app_name}}", name);
            solutionFile = solutionFile.Replace("{{project_location}}", $"{location}\\{name}.njsproj");


            // {{folder_group}}
            // {{file_group}}


            /* 
             
            For the FOLDER group, we need to loop around all the folders in the project and add them to the folder group
            like this.  
             
            <ItemGroup>
                <Folder Include="src\" />
                <Folder Include="src\api\" />
                <Folder Include="src\components\" />
                <Folder Include="src\components\example\" />
                <Folder Include="src\components\header\" />
                <Folder Include="src\controllers\" />
                <Folder Include="src\models\" />
                <Folder Include="src\pages\" />
                <Folder Include="src\utils\" />
            </ItemGroup> 

            */

            string[] directoryList = Directory.GetDirectories(location, "*", SearchOption.AllDirectories);

            var folderGrp = new StringBuilder();

            folderGrp.AppendLine("<ItemGroup>");

            // Display the names of the directories.
            foreach (var dir in directoryList)
            {
                if (dir.Contains(".git") || dir.Contains("node_modules") || dir.Contains(".vs"))
                    continue;

                var targetDir = StrUtils.RemoveLeadingSlash(dir.Replace(location, ""));

                folderGrp.AppendLine($"<Folder Include='{targetDir}\' />");
            }
                

            folderGrp.AppendLine("</ItemGroup>");
            folderGrp.Replace("'", "\"");

            projectFile = projectFile.Replace("{{folder_group}}", folderGrp.ToString());


            /* 

           For the FILE group, we need to loop around all the FILES in each FOLDER in the project and add them like this.  

            <ItemGroup>
                <Content Include="src\api\AppManager.ts" />
                <Content Include="src\App.test.tsx" />
                <Content Include="src\App.tsx" />
                <Content Include="src\components\example\example.tsx" />
                <Content Include="src\components\header\header.tsx" />
                <Content Include="src\components\HookExample.tsx" />
                <Content Include="src\constants.ts" />
                <Content Include="src\index.tsx" />
                <Content Include="src\models\GlobalDataStore.ts" />
                <Content Include="src\models\SiteData.ts" />
                <Content Include="src\pages\ContactPage.tsx" />
                <Content Include="src\pages\HomePage.tsx" />
                <Content Include="src\pages\UserPage.tsx" />
                <Content Include="src\react-app-env.d.ts" />
                <Content Include="src\serviceWorker.ts" />
             </ItemGroup>

           */

            string[] fileList = Directory.GetFiles(location, "*.*", SearchOption.AllDirectories);

            var fileGrp = new StringBuilder();

            fileGrp.AppendLine("<ItemGroup>");

            // Display the names of the directories.
            foreach (var dir in fileList)
            {
                if (dir.Contains(".git") || dir.Contains("node_modules") || dir.Contains(".vs"))
                    continue;

                var targetDir = StrUtils.RemoveLeadingSlash(dir.Replace(location, ""));

                fileGrp.AppendLine($"<Content Include='{targetDir}\' />");
            }
                

            fileGrp.AppendLine("</ItemGroup>");
            fileGrp.Replace("'", "\"");

            projectFile = projectFile.Replace("{{file_group}}", fileGrp.ToString());


            // Generate path for files
            var solutionPath = Path.Combine(location, $"{name}.sln");
            var projPath = Path.Combine(location, $"{name}.njsproj");

            // Finally write files out to disk.
            File.WriteAllText(solutionPath, solutionFile);
            File.WriteAllText(projPath, projectFile);

            return solutionPath;
        }
    }
}
