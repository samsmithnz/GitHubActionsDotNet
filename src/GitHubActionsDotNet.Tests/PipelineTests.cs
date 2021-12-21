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
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        root.jobs = new();
        root.on = new()
        {
            push = new()
            {
                branches = new string[]
                {
            "main"
                }
            }
        };
        Job buildJob = jobHelper.AddJob(
            null,
            "windows-latest",
            new Step[]
            {
                CommonStepHelper.AddScriptStep("Hello world", "echo 'hello world'")
            });
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
jobs:
  build:
    runs-on: windows-latest
    steps:
    - name: Hello world
      run: echo 'hello world'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
