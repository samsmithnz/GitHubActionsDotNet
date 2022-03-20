using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class Schedule
    {
        public string interval { get; set; }
        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string time { get; set; }
        public string timezone { get; set; }
        public string day { get; set; }
    }
}
