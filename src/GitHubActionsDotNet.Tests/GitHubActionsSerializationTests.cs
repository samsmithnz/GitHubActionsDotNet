using GitHubActionsDotNet.Models;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class GitHubActionsSerializationTests
{
    [TestMethod]
    public void DeserializeSimpleWorkflowTest()
    {
        // Arrange
        string yaml = @"name: Test Workflow
on:
  push:
    branches: [ main ]
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4";

        // Act
        GitHubActionsRoot result = GitHubActionsSerialization.Deserialize(yaml);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Workflow", result.name);
        Assert.IsNotNull(result.on);
        Assert.IsNotNull(result.jobs);
    }

    [TestMethod]
    public void SerializeJobTest()
    {
        // Arrange
        Job job = new Job
        {
            name = "Test Job",
            runs_on = "ubuntu-latest",
            steps = new Step[]
            {
                new Step { uses = "actions/checkout@v4" }
            }
        };

        // Act
        string yaml = GitHubActionsSerialization.SerializeJob(job);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("name: Test Job"));
        Assert.IsTrue(yaml.Contains("runs-on: ubuntu-latest"));
        Assert.IsTrue(yaml.Contains("uses: actions/checkout@v4"));
    }

    [TestMethod]
    public void SerializeJobWithVariablesTest()
    {
        // Arrange
        Job job = new Job
        {
            name = "Test Job",
            runs_on = "ubuntu-latest",
            steps = new Step[]
            {
                new Step { run = "echo $(TestVar)" }
            }
        };
        List<string> variableList = new List<string> { "TestVar" };

        // Act
        string yaml = GitHubActionsSerialization.SerializeJob(job, variableList);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("${{ env.TestVar }}"));
    }

    [TestMethod]
    public void SerializeStepTest()
    {
        // Arrange
        Step step = new Step
        {
            name = "Test Step",
            uses = "actions/checkout@v4",
            with = new Dictionary<string, string> { { "fetch-depth", "0" } }
        };

        // Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("name: Test Step"));
        Assert.IsTrue(yaml.Contains("uses: actions/checkout@v4"));
        Assert.IsTrue(yaml.Contains("fetch-depth: 0"));
    }

    [TestMethod]
    public void SerializeStepWithVariablesTest()
    {
        // Arrange
        Step step = new Step
        {
            name = "Test Step",
            run = "echo $(MyVar)"
        };
        List<string> variableList = new List<string> { "MyVar" };

        // Act
        string yaml = GitHubActionsSerialization.SerializeStep(step, variableList);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("${{ env.MyVar }}"));
    }

    [TestMethod]
    public void SerializeWorkflowWithCronTest()
    {
        // Arrange
        GitHubActionsRoot root = new GitHubActionsRoot
        {
            name = "Scheduled Workflow",
            on = new Trigger
            {
                schedule = new string[]
                {
                    "cron: 0 2 * * *"
                }
            },
            jobs = new Dictionary<string, Job>
            {
                { "build", new Job { runs_on = "ubuntu-latest" } }
            }
        };

        // Act
        string yaml = GitHubActionsSerialization.Serialize(root);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("0 2 * * *"));
        Assert.IsFalse(yaml.Contains("\"cron\""));
    }

    [TestMethod]
    public void SerializeWorkflowWithMatrixVariableTest()
    {
        // Arrange
        GitHubActionsRoot root = new GitHubActionsRoot
        {
            name = "Matrix Workflow",
            jobs = new Dictionary<string, Job>
            {
                { 
                    "build", 
                    new Job 
                    { 
                        runs_on = "ubuntu-latest",
                        steps = new Step[]
                        {
                            new Step { run = "echo $(os)" }
                        }
                    } 
                }
            }
        };
        List<string> variableList = new List<string> { "os" };

        // Act
        string yaml = GitHubActionsSerialization.Serialize(root, variableList, "os");

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("${{ matrix.os }}"));
    }

    [TestMethod]
    public void SerializeWorkflowDispatchTest()
    {
        // Arrange
        GitHubActionsRoot root = new GitHubActionsRoot
        {
            name = "Manual Workflow",
            on = new Trigger
            {
                workflow_dispatch = ""
            },
            jobs = new Dictionary<string, Job>
            {
                { "build", new Job { runs_on = "ubuntu-latest" } }
            }
        };

        // Act
        string yaml = GitHubActionsSerialization.Serialize(root);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("workflow_dispatch:"));
        Assert.IsFalse(yaml.Contains("workflow_dispatch: ''"));
    }

    [TestMethod]
    public void SerializeWorkflowWithScriptTest()
    {
        // Arrange
        GitHubActionsRoot root = new GitHubActionsRoot
        {
            name = "Script Workflow",
            jobs = new Dictionary<string, Job>
            {
                { 
                    "build", 
                    new Job 
                    { 
                        runs_on = "ubuntu-latest",
                        steps = new Step[]
                        {
                            new Step { run = "echo 'line1'\necho 'line2'" }
                        }
                    } 
                }
            }
        };

        // Act
        string yaml = GitHubActionsSerialization.Serialize(root);

        // Assert
        Assert.IsNotNull(yaml);
        Assert.IsTrue(yaml.Contains("echo 'line1'"));
        Assert.IsTrue(yaml.Contains("echo 'line2'"));
    }
}