using GitHubActionsDotNet.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public static class Serialization2
    {
        public static Root2 Deserialize(string yaml)
        {
            Root2 root = YamlSerialization.DeserializeYaml<Root2>(yaml);
            return root;
        }

        public static string Serialize(Root2 root)
        {
            string yaml = YamlSerialization.SerializeYaml(root);
            return yaml;
        }
    }
}
