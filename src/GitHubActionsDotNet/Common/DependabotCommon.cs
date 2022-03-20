using System.Collections.Generic;

namespace GitHubActionsDotNet.Common
{
    public class DependabotCommon
    {
        public static List<string> GetFileTypesToSearch()
        {
            List<string> files = new List<string>
            {
                "pom.xml",
                "package.json",
                "nuget.config",
                "*.csproj",
                "*.vbproj",
                "Gemfile",
                "requirements.txt"
            };
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
