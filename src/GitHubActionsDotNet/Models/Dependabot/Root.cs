using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class Root
    {
        public Root()
        {
            version = "2";
            updates = new List<Package>();
        }

        public string version { get; set; }

        public List<Package> updates { get; set; }
    }
}
