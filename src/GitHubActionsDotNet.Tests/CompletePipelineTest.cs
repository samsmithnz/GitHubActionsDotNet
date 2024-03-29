﻿using GitHubActionsDotNet.Helpers;
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
}

