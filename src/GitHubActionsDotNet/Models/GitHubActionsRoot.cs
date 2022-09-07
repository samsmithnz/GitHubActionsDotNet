using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace GitHubActionsDotNet.Models
{
    public class GitHubActionsRoot
    {
        public string name { get; set; }
        public Trigger on { get; set; }
        public IDictionary<string, string> env { get; set; } //While technically this could be a Dictionary<string,string> type, there are a few situations with Azure DevOps migrations that we want to support multiply keys with the same name (template) under variables. Only adding this to the root of level for now. We use a List<KeyValuePair<string,string>> to pass into the IDictonary, which seems to work.
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