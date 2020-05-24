using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Model
{
    public class ReactTemplate
    {
        public string Name { get; set; }
        public string ProjectFolder { get; set; }
        public string ReactAppCmd { get; set; }
        public string ReactAppFlags { get; set; }
        public bool RunNPMAuditFix { get; set; }
        public bool CreateVisualStudioSolution { get; set; }
        public List<NpmPackage> NpmPackageList { get; set; }
        public List<ProjFolder> ProjectFolderList { get; set; }
        public List<BoilerPlateFile> BoilerPlateFileList { get; set; }

        public ReactTemplate()
        {
            NpmPackageList      = new List<NpmPackage>();
            ProjectFolderList   = new List<ProjFolder>();
            BoilerPlateFileList = new List<BoilerPlateFile>();
        }

        internal string GetProjectFolder()
        {
            return $"{ProjectFolder}\\{Name.ToLower()}";
        }
    }
}
