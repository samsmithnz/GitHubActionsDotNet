using GitHubActionsDotNet.Models.Dependabot;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Serialization
{
    public static class DependabotPackageSerialization<T>
    {
        public static Package<T> CreatePackage(string filePath,
           string packageEcoSystem,
           string interval = null,
           string time = null,
           string timezone = null,
           List<string> assignees = null,
           int openPRLimit = 0)
        {
            Package<T> package = new Package<T>()
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
