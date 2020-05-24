using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Utilities
{
    class FileIO
    {
        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static bool CreateDirectory(string directoryName)
        {
            bool dirCreated = false;

            DirectoryInfo dirInfo = null;
            if (!Directory.Exists(directoryName))
            {
                dirInfo = Directory.CreateDirectory(directoryName);
                dirCreated = true;
            }

            return dirCreated;
        }

        public static string MoveFile(string currentLocation, string newLocation)
        {
            StringBuilder tmpNewLoc = new StringBuilder();

            FileInfo fileInfo = new FileInfo(currentLocation);
            if (File.Exists(currentLocation) == true)
            {
                // Move file
                tmpNewLoc.Append(newLocation);
                tmpNewLoc.Append("\\");
                tmpNewLoc.Append(fileInfo.Name);

                // Check that there isnt a file with the same name in the dest dir
                if (File.Exists(tmpNewLoc.ToString()) == true)
                {
                    tmpNewLoc.Append("_FileNameModifedAlreadyExists_");
                    string currTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                    tmpNewLoc.Append(currTime);
                    tmpNewLoc.Append(".txt");
                }

                fileInfo.MoveTo(tmpNewLoc.ToString());
            }

            // return new file location!
            return tmpNewLoc.ToString();
        }

        public static void CopyFile(string currentLocation, string newLocation, bool bOverwriteIfExists)
        {
            if (File.Exists(currentLocation))
            {
                File.Copy(currentLocation, newLocation, bOverwriteIfExists);
            }
        }
    }
}
