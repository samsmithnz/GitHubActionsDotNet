using System.Collections.Generic;
using System.IO;

namespace GitHubActionsDotNet.Common
{
    public class DependabotCommon
    {
        public static List<string> GetFileTypesToSearch()
        {
            List<string> files = new List<string>();
            files.Add("pom.xml");
            files.Add("package.json");
            files.Add("nuget.config");
            files.Add("*.csproj");
            files.Add("*.vbproj");
            files.Add("Gemfile");
            files.Add("requirements.txt");
            return files;
        }

        public static string GetPackageEcoSystemFromFileName(FileInfo fileInfo)
        {
            string packageEcosystem = "";
            if (fileInfo.Name == "pom.xml")
            {
                packageEcosystem = "maven";
            }
            else if (fileInfo.Name == "package.json")
            {
                packageEcosystem = "npm";
            }
            else if (fileInfo.Name == "nuget.config" ||
                fileInfo.Extension == ".csproj" ||
                fileInfo.Extension == ".vbproj")
            {
                packageEcosystem = "nuget";
            }
            else if (fileInfo.Name == "Gemfile")
            {
                packageEcosystem = "bundler";
            }
            else if (fileInfo.Name == "requirements.txt")
            {
                packageEcosystem = "pip";
            }
            return packageEcosystem;
        }
    }
}
