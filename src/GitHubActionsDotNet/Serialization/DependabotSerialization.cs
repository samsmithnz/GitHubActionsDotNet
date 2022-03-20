using GitHubActionsDotNet.Common;
using GitHubActionsDotNet.Models.Dependabot;
using System.Collections.Generic;
using System.IO;

namespace GitHubActionsDotNet.Serialization
{
    public static class DependabotSerialization
    {
        public static string Serialize(string startingDirectory,
            List<string> files,
            string interval = null,
            string time = null,
            string timezone = null,
            List<string> assignees = null,
            int openPRLimit = 0,
            bool includeActions = true)
        {
            if (startingDirectory == null)
            {
                return "";
            }

            DependabotRoot root = new DependabotRoot();
            List<Package> packages = new List<Package>();
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                string cleanedFilePath = file.Replace(startingDirectory + "/", "");
                cleanedFilePath = cleanedFilePath.Replace(startingDirectory + "\\", "");
                cleanedFilePath = cleanedFilePath.Replace(fileInfo.Name, "");
                cleanedFilePath = "/" + cleanedFilePath.Replace("\\", "/");
                string packageEcoSystem = DependabotCommon.GetPackageEcoSystemFromFileName(fileInfo.Name);
                Package package = CreatePackage(cleanedFilePath, packageEcoSystem, interval, time, timezone, assignees, openPRLimit);
                packages.Add(package);
            }
            //Add actions
            if (includeActions == true)
            {
                Package actionsPackage = CreatePackage("/", "github-actions", interval, time, timezone, assignees, openPRLimit);
                packages.Add(actionsPackage);
            }
            root.updates = packages;

            //Serialize the object into YAML
            string yaml = YamlSerialization.SerializeYaml(root);

            //I can't use - in variable names, so replace _ with -
            yaml = yaml.Replace("package_ecosystem", "package-ecosystem");
            yaml = yaml.Replace("replaces_base", "replaces-base");

            return yaml;
        }

        public static DependabotRoot Deserialize(string yaml)
        {
            yaml = yaml.Replace("package-ecosystem", "package_ecosystem");
            yaml = yaml.Replace("open-pull-requests-limit", "open_pull_requests_limit");
            yaml = yaml.Replace("replaces-base", "replaces_base");

            DependabotRoot root = YamlSerialization.DeserializeYaml<DependabotRoot>(yaml);
            return root;
        }

        private static Package CreatePackage(string filePath,
            string packageEcoSystem,
            string interval = null,
            string time = null,
            string timezone = null,
            List<string> assignees = null,
            int openPRLimit = 0)
        {
            Package package = new Package()
            {
                package_ecosystem = packageEcoSystem,
                directory = filePath,
                assignees = assignees
            };
            if (interval != null ||
                time != null ||
                timezone != null)
            {
                package.schedule = new Schedule();
                if (interval != null)
                {
                    package.schedule.interval = interval;
                }
                if (time != null)
                {
                    package.schedule.time = time;
                }
                if (timezone != null)
                {
                    package.schedule.timezone = timezone;
                }
            }
            if (openPRLimit > 0)
            {
                package.open_pull_requests_limit = openPRLimit.ToString();
            }
            return package;
        }
    }
}
