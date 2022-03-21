using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class DependabotRoot
    {
        public DependabotRoot()
        {
            version = "2";
        }

        public string version { get; set; }
        public IDictionary<string, Registry> registries { get; set; }
        public List<IPackage> updates { get; set; }
    }
}
