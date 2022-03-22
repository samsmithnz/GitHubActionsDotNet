using GitHubActionsDotNet.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public static class Serialization2
    {
        public static string Serialize(Root2 root)
        {
            return YamlSerialization.SerializeYaml(root);
        }
        public static Root2 Deserialize(string yaml)
        {
            //convert the yaml into json, it's easier to parse
            JsonElement jsonObject = new JsonElement();
            if (yaml != null)
            {
                jsonObject = JsonSerialization.DeserializeStringToJsonElement(yaml);
            }

            //Build up the GitHub object piece by piece
            Root2 root = new Root2();

            if (jsonObject.ValueKind != JsonValueKind.Undefined)
            {
                //Name
                if (jsonObject.TryGetProperty("name", out JsonElement jsonElement))
                {
                    string nameYaml = jsonElement.ToString();
                    root.name = ProcessName(nameYaml);
                }

                //Packages
                if (jsonObject.TryGetProperty("packages", out jsonElement))
                {
                    foreach (JsonElement packagesItem in jsonElement.EnumerateArray())
                    {
                        string packageYaml = packagesItem.ToString();
                        if (root.packages == null)
                        {
                            root.packages = new List<IPackage2>();
                        }
                        root.packages.Add(ProcessPackage(packageYaml));
                    }
                }
            }

            return root;
        }

        public static string ProcessName(string nameYaml)
        {
            return nameYaml.Replace("name:", "").Replace(System.Environment.NewLine, "").Trim();
        }

        public static IPackage2 ProcessPackage(string packageYaml)
        {
            IPackage2 package = null;
            if (packageYaml != null)
            {
                try
                {
                    package = YamlSerialization.DeserializeYaml<Package2String>(packageYaml);
                }
                catch
                {
                    package = YamlSerialization.DeserializeYaml<Package2StringArray>(packageYaml);
                }
            }
            return package;
        }
    }
}
