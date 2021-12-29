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

        public static string GetPackageEcoSystemFromFileName(string fileName)
        {
            string packageEcosystem = "";
            if (fileName == "pom.xml")
            {
                packageEcosystem = "maven";
            }
            else if (fileName == "package.json")
            {
                packageEcosystem = "npm";
            }
            else if (fileName == "nuget.config" ||
                fileName.EndsWith(".csproj") ||
                fileName.EndsWith(".vbproj"))
            {
                packageEcosystem = "nuget";
            }
            else if (fileName == "Gemfile")
            {
                packageEcosystem = "bundler";
            }
            else if (fileName == "requirements.txt")
            {
                packageEcosystem = "pip";
            }
            return packageEcosystem;
        }
    }
}
