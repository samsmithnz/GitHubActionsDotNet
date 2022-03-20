using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class DependabotRoot
    {
        public DependabotRoot()
        {
            version = "2";
            registries = new Dictionary<string, Registry>();
            updates = new List<Package>();
        }

        public string version { get; set; }
        public Dictionary<string, Registry> registries { get; set; }
        public List<Package> updates { get; set; }
    }
}
