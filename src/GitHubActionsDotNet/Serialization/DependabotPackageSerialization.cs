using GitHubActionsDotNet.Models.Dependabot;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Serialization
{
    public static class DependabotPackageSerialization
    {
        public static IPackage CreatePackage(string filePath,
           string packageEcoSystem,
           string interval = null,
           string time = null,
           string timezone = null,
           List<string> assignees = null,
           int openPRLimit = 0,
           string registryString = null,
           string[] registryStringArray = null)
        {
            IPackage package;
            if (registryString != null)
            {
                package = new PackageString
                {
                    registries = registryString
                };
            }
            else
            {
                package = new PackageStringArray
                {
                    registries = registryStringArray
                };
            }
            package.package_ecosystem = packageEcoSystem;
            package.directory = filePath;
            package.assignees = assignees;
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
