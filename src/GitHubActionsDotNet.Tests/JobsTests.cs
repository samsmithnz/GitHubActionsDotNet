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
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""", "cmd")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
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
    - uses: actions/checkout@v4
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
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new Dictionary<string, string>()
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
    - uses: actions/checkout@v4
    - run: echo ""hello world""
";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }



    [TestMethod]
    public void ComplexVariablesWithComplexNeedsJobTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new string[] { "Job", "AnotherJob" },
            0,
            null,
            null,
            new()
            {
                { "group", "Active Login" },
                { "sourceArtifactName", "nuget-windows" },
                { "targetArtifactName", "nuget-windows-signed" },
                { "pathToNugetPackages", "**/*.nupkg" }
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
    needs:
    - Job
    - AnotherJob
    env:
      group: Active Login
      sourceArtifactName: nuget-windows
      targetArtifactName: nuget-windows-signed
      pathToNugetPackages: '**/*.nupkg'
    steps:
    - uses: actions/checkout@v4
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }


    [TestMethod]
    public void ComplexVariablesWithSimpleNeedsJobTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new string[] { "AnotherJob" },
            0,
            null,
            null,
            new()
            {
                { "group", "Active Login" },
                { "sourceArtifactName", "nuget-windows" },
                { "targetArtifactName", "nuget-windows-signed" },
                { "pathToNugetPackages", "**/*.nupkg" }
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
    needs:
    - AnotherJob
    env:
      group: Active Login
      sourceArtifactName: nuget-windows
      targetArtifactName: nuget-windows-signed
      pathToNugetPackages: '**/*.nupkg'
    steps:
    - uses: actions/checkout@v4
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void SimpleVariablesWithSimpleDependsOnJobTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new string[] { "AnotherJob" },
            0,
            null,
            null,
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
    needs:
    - AnotherJob
    env:
      Variable1: new variable
    steps:
    - uses: actions/checkout@v4
    - run: echo ""hello world""
";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }



    [TestMethod]
    public void SimpleVariablesWithComplexDependsOnJobTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "windows-latest",
            buildSteps,
            new string[] { "AnotherJob" },
            new Dictionary<string, string>()
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
    needs:
    - AnotherJob
    env:
      Variable1: new variable
    steps:
    - uses: actions/checkout@v4
    - run: echo ""hello world""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void CheckoutJobTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddCheckoutStep(null, "git://MyProject/MyRepo"),
            CommonStepHelper.AddCheckoutStep("GitHub checkout", "MyGitHubRepo")};
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
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
    - uses: actions/checkout@v4
    - uses: actions/checkout@v4
      with:
        repository: git://MyProject/MyRepo
    - name: GitHub checkout
      uses: actions/checkout@v4
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
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            CommonStepHelper.AddScriptStep(null, @"echo ""hello world""")
        };
        root.jobs = new();
        Job prodJob = jobHelper.AddJob(
            "Prod",
            "ubuntu-latest",
            buildSteps,
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
    - uses: actions/checkout@v4
    - run: echo ""hello world""
";
        //url: https://abel-node-gh-accelerator.azurewebsites.net

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

}
