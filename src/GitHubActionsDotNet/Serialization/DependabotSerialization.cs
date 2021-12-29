using GitHubActionsDotNet.Common;
using GitHubActionsDotNet.Models.Dependabot;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Environment = System.Environment;

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

            Root root = new Root();
            List<Package> packages = new List<Package>();
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                string cleanedFile = file.Replace(startingDirectory + "/", "");
                cleanedFile = cleanedFile.Replace(startingDirectory + "\\", "");
                cleanedFile = cleanedFile.Replace(fileInfo.Name, "");
                cleanedFile = "/" + cleanedFile.Replace("\\", "/");
                Package package = new Package()
                {
                    package_ecosystem = DependabotCommon.GetPackageEcoSystemFromFileName(fileInfo),
                    directory = cleanedFile,
                    schedule = new Schedule()
                    {
                        interval = interval,
                        time = time,
                        timezone = timezone
                    },
                    assignees = assignees
                };
                if (openPRLimit > 0)
                {
                    package.open_pull_requests_limit = openPRLimit.ToString();
                }
                packages.Add(package);
            }
            //Add actions
            if (includeActions == true)
            {
                Package actionsPackage = new Package();
                actionsPackage.package_ecosystem = "github-actions";
                actionsPackage.directory = "/";
                actionsPackage.schedule = new Schedule()
                {
                    interval = interval
                };
                if (time != null)
                {
                    actionsPackage.schedule.time = time;
                }
                if (timezone != null)
                {
                    actionsPackage.schedule.timezone = timezone;
                }
                actionsPackage.assignees = assignees;
                if (openPRLimit > 0)
                {
                    actionsPackage.open_pull_requests_limit = openPRLimit.ToString();
                }
                packages.Add(actionsPackage);
            }
            root.updates = packages;

            //Serialize the object into YAML
            string yaml = YamlSerialization.SerializeYaml(root);

            //I can't use - in variable names, so replace _ with -
            yaml = yaml.Replace("package_ecosystem", "package-ecosystem");
            yaml = yaml.Replace("open_pull_requests_limit", "open-pull-requests-limit");

            return yaml;
        }
    }
}
