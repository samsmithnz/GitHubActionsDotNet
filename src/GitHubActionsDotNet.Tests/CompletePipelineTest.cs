using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class CompletePipelineTest
{
    [TestMethod]
    public void CompleteHelloWorldPipelineTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new()
        {
            name = "CI/CD",
            on = TriggerHelper.AddStandardPushAndPullTrigger(),
            jobs = new()
        };
        //Build job
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null,
                @"echo ""hello world""",
                "cmd")
        };
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps);
        root.jobs.Add("build", buildJob);
        //Release job
        Step[] releaseSteps = new Step[] {
            CommonStepHelper.AddScriptStep(null,
                @"echo ""hello world""")
        };
        Job releaseJob = jobHelper.AddJob(
            "Release job",
            "ubuntu-latest",
            releaseSteps,
            new string[] { "build" });
        root.jobs.Add("release", releaseJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
name: CI/CD
on:
  push:
    branches:
    - main
  pull_request:
    branches:
    - main
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v4
    - run: echo ""hello world""
      shell: cmd
  release:
    name: Release job
    runs-on: ubuntu-latest
    needs:
    - build
    steps:
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void WorkflowDispatchWithInputsExampleTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        root.jobs = new();
        root.on = new()
        { 
            push = new() 
            { 
                branches= new string[]
                {
                    "main"
                }
            },
            workflow_dispatch = new()
            {
                inputs = new System.Collections.Generic.Dictionary<string, WorkflowDispatchInput>
                {
                    { "environment", new WorkflowDispatchInput
                        {
                            description = "Select the environment",
                            required = true,
                            _default = "staging",
                            type = "choice",
                            options = new string[] { "staging", "production" }
                        }
                    },
                    { "version", new WorkflowDispatchInput
                        {
                            description = "Version to deploy",
                            required = true,
                            type = "string"
                        }
                    }
                }
            }
        };
        Job buildJob = jobHelper.AddJob(
            null,
            "ubuntu-latest",
            new Step[]
            {
                CommonStepHelper.AddScriptStep("Deploy", 
                    "echo 'Deploying version ${{ github.event.inputs.version }} to ${{ github.event.inputs.environment }}'")
            });
        root.jobs.Add("deploy", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
  workflow_dispatch:
    inputs:
      environment:
        description: Select the environment
        required: true
        default: staging
        type: choice
        options:
        - staging
        - production
      version:
        description: Version to deploy
        required: true
        type: string
jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - name: Deploy
      run: echo 'Deploying version ${{ github.event.inputs.version }} to ${{ github.event.inputs.environment }}'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}

