using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace GitHubActionsDotNet.Models
{
    public class GitHubActionsRoot
    {
        public string name { get; set; }
        public Trigger on { get; set; }
        public IDictionary<string,string> env { get; set; } //While technically this should be a Dictionary<string,string> type, there are a few situations with Azure DevOps migrations that we want to translate over completely
        public Dictionary<string, Job> jobs { get; set; }

        //This is used for tracking errors, so we don't want it to convert to YAML
        [YamlIgnore]
        public List<string> messages { get; set; }

        public GitHubActionsRoot()
        {
            messages = new List<string>();
        }
    }
}