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
    public void Packages2DeserializeStringTest()
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
    public void Packages2DeserializeStringArrayTest()
    {
        //Arrange
        string yaml = @"name: test1
packages:
- name: package1
  registries:
  - abc
  - xyz
";

        //Act
        Root2 root = Serialization2.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(root);
        Assert.AreEqual("test1", root.name);
        Assert.AreEqual("package1", root.packages[0].name);
        Assert.AreEqual("abc", root.packages[0].registries); 
        Assert.AreEqual(2, ((string[])root.packages[0].registries).Length);
        Assert.AreEqual("abc", ((string[])root.packages[0].registries)[0]);
        Assert.AreEqual("xyz", ((string[])root.packages[0].registries)[1]);
    }

    [TestMethod]
    public void Packages2DeserializeStringAndStringArrayTest()
    {
        //Arrange
        string yaml = @"name: test1
packages:
- name: package1
  registries: abc
- name: package2
  registries:
  - abc
  - xyz
";

        //Act
        Root2 root = Serialization2.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(root);
        Assert.AreEqual("test1", root.name);
        Assert.AreEqual("package1", root.packages[0].name);
        Assert.AreEqual("package2", root.packages[1].name);
        Assert.AreEqual(2, ((string[])root.packages[1].registries).Length);
        Assert.AreEqual("abc", ((string[])root.packages[1].registries)[0]);
        Assert.AreEqual("xyz", ((string[])root.packages[1].registries)[1]);
    }

    [TestMethod]
    public void Packages2SerializeStringTest()
    {
        //Arrange
        Root2 root = new()
        {
            name = "test1",
            packages = new List<IPackage2>()
            {
                new Package2<string>()
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

    [TestMethod]
    public void Packages2SerializeStringArrayTest()
    {
        //Arrange
        Root2 root = new()
        {
            name = "test1",
            packages = new List<IPackage2>()
            {
                new Package2<string[]>()
                {
                    name = "package1",
                    registries = new string [] {"abc","xyz" }
                }
            }
        };

        //Act
        string yaml = Serialization2.Serialize(root);

        //Assert
        string expected = @"name: test1
packages:
- name: package1
  registries:
  - abc
  - xyz
";

        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void Packages2SerializeStringAndStringArrayTest()
    {
        //Arrange
        Root2 root = new()
        {
            name = "test1",
            packages = new List<IPackage2>()
            {
                new Package2<string>()
                {
                    name = "package1",
                    registries = "abc"
                },
                new Package2<string[]>()
                {
                    name = "package2",
                    registries = new string [] {"abc","xyz" }
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
- name: package2
  registries:
  - abc
  - xyz
";

        Assert.AreEqual(expected, yaml);
    }


}
