using GitHubActionsDotNet.Models.Dependabot;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class DependabotDeserializationTests
{
    [TestMethod]
    public void AllowDeserializationTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    allow:
      # Allow updates for Lodash
      - dependency-name: ""lodash""
      # Allow updates for React and any packages starting ""react""
      - dependency-name: ""react*""

  - package-ecosystem: ""composer""
    directory: ""/""
    schedule:
      interval: ""daily""
    allow:
      # Allow both direct and indirect updates for all packages
      - dependency-type: ""all""

  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    allow:
      # Allow only direct updates for
      # Django and any packages starting ""django""
      - dependency-name: ""django*""
        dependency-type: ""direct""
      # Allow only production updates for Sphinx
      - dependency-name: ""sphinx""
        dependency-type: ""production""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void AssigneesTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Add assignees
    assignees:
      - ""octocat""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void CommitMessageTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    commit-message:
      # Prefix all commit messages with ""npm""
      prefix: ""npm""

  - package-ecosystem: ""composer""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Prefix all commit messages with ""Composer""
    # include a list of updated dependencies
    commit-message:
      prefix: ""Composer""
      include: ""scope""

  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Include a list of updated dependencies
    # with a prefix determined by the dependency group
    commit-message:
      prefix: ""pip prod""
      prefix-development: ""pip dev""
      include: ""scope""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

}
