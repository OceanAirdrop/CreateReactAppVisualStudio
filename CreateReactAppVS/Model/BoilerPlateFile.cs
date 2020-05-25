using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Model
{
    public class BoilerPlateFile
    {
        public bool Included { get; set; }
        public string FileName { get; set; }
        public string SourceDir { get; set; }
        public string DestDir { get; set; }
    }
}
