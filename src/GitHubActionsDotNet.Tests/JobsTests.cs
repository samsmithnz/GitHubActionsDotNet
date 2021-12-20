using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class JobsTests
{

    [TestMethod]
    public void SimpleJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""", "cmd")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            null,
            null,
            30);
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    timeout-minutes: 30
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
      shell: cmd
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void SimpleVariablesJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new()
            {
                { "Variable1", "new variable" }
            });
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    env:
      Variable1: new variable
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }



    [TestMethod]
    public void ComplexVariablesWithComplexNeedsJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new()
            {
                { "group", "Active Login" },
                { "sourceArtifactName", "nuget-windows" },
                { "targetArtifactName", "nuget-windows-signed" },
                { "pathToNugetPackages", "**/*.nupkg" }
            },
            new string[] { "Job", "AnotherJob" });
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    needs:
    - Job
    - AnotherJob
    env:
      group: Active Login
      sourceArtifactName: nuget-windows
      targetArtifactName: nuget-windows-signed
      pathToNugetPackages: '**/*.nupkg'
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }


    [TestMethod]
    public void ComplexVariablesWithSimpleNeedsJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new()
            {
                { "group", "Active Login" },
                { "sourceArtifactName", "nuget-windows" },
                { "targetArtifactName", "nuget-windows-signed" },
                { "pathToNugetPackages", "**/*.nupkg" }
            },
            new string[] { "AnotherJob" });
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    needs:
    - AnotherJob
    env:
      group: Active Login
      sourceArtifactName: nuget-windows
      targetArtifactName: nuget-windows-signed
      pathToNugetPackages: '**/*.nupkg'
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void SimpleVariablesWithSimpleDependsOnJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new()
            {
                { "Variable1", "new variable" }
            },
            new string[] { "AnotherJob" });
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    needs:
    - AnotherJob
    env:
      Variable1: new variable
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }



    [TestMethod]
    public void SimpleVariablesWithComplexDependsOnJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new()
            {
                { "Variable1", "new variable" }
            },
            new string[] { "AnotherJob" });
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    needs:
    - AnotherJob
    env:
      Variable1: new variable
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void CheckoutJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddCheckoutStep(null, "git://MyProject/MyRepo"),
            CommonStepHelper.AddCheckoutStep("GitHub checkout", "MyGitHubRepo")};
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "ubuntu-latest",
            buildSteps);
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  build:
    name: Build job
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    - uses: actions/checkout@v2
      with:
        repository: git://MyProject/MyRepo
    - name: GitHub checkout
      uses: actions/checkout@v2
      with:
        repository: MyGitHubRepo
";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void EnvironmentJobTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job prodJob = JobHelper.AddJob(
            "Prod",
            "ubuntu-latest",
            buildSteps,
            null,
            new string[] { "functionalTests" },
            0,
            new Environment { name = "prod" });
        root.jobs.Add("prod", prodJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
jobs:
  prod:
    name: Prod
    runs-on: ubuntu-latest
    needs:
    - functionalTests
    environment:
      name: prod
    steps:
    - uses: actions/checkout@v2
    - run: echo ""hello world""
";
        //url: https://abel-node-gh-accelerator.azurewebsites.net

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

}
