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
        //Using IDictornary allows me to use the .Add functionality
        public IDictionary<string, Registry> registries { get; set; }
        //Using IPackage, allows me to have multiple class implementations in the same list
        public List<Package> updates { get; set; }
    }
}
