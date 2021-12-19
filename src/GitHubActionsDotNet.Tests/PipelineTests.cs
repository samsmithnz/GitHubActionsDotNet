using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[TestClass]
public class PipelineTests
{
    [TestMethod]
    public void PipelineNameTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        root.name = "test ci pipelines";

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = "name: test ci pipelines";
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void PipelineSimpleStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            null,
            "windows-latest");
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    runs-on: windows-latest
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
