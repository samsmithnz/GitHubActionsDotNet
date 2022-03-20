using GitHubActionsDotNet.Models.Dependabot;
using GitHubActionsDotNet.Models.DependabotV2POC;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestCategory("Dependabot")]
[TestClass]
public class DependabotDeserialization2Tests
{
    [TestMethod]
    public void Packages2DeserializeTest()
    {
        //Arrange
        string yaml = @"name: test1
packages:
- name: package1
  registries: abc
";

        //Act
        Root2 root = Serialization2.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(root);
        Assert.AreEqual("test1", root.name);
        Assert.AreEqual("package1", root.packages[0].name);
        Assert.AreEqual("abc", root.packages[0].registries);

    }

    [TestMethod]
    public void Packages2SerializeTest()
    {
        //Arrange
        Root2 root = new()
        {
            name = "test1",
            packages = new List<Package2>()
            {
                new Package2()
                {
                    name = "package1",
                    registries = "abc"
                }
            }
        };

        //Act
        string yaml = Serialization2.Serialize(root);

        //Assert
        string expected = @"name: test1
packages:
- name: package1
  registries: abc
";

        Assert.AreEqual(expected, yaml);
    }


}
