using GitHubActionsDotNet.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GitHubActionsDotNet.Helpers
{
    public class FileSearch
    {
        public static List<string> GetFilesForDirectory(string startingDirectory)
        {
            if (startingDirectory == null)
            {
                return new List<string>();
            }
            List<string> fileTypes = DependabotCommon.GetFileTypesToSearch();
            List<string> sortedFiles = new List<string>();
            foreach (string fileType in fileTypes)
            {
                string[] files = Directory.GetFiles(startingDirectory, fileType, SearchOption.AllDirectories);
                //Order files alphabetically - there is some variation on different OS's on order
                sortedFiles.AddRange(files.ToList<string>());
            }
            sortedFiles.Sort();
            return sortedFiles;
        }
    }
}
