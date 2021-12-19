using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        Step[] buildSteps = new Step[2];
        buildSteps[0] = CommonStepsHelper.AddCheckoutStep();
        buildSteps[1] = CommonStepsHelper.AddScriptStep(null, @"echo ""hello world""", "cmd");
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "windows-latest",
            null,
            30,
            buildSteps);
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

    //    [TestMethod]
    //    public void SimpleVariablesJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //jobs:
    //- job: Build
    //  displayName: 'Build job'
    //  pool:
    //    vmImage: windows-latest
    //  variables:
    //    Variable1: 'new variable'
    //  steps:
    //  - task: CmdLine@2
    //    inputs:
    //      script: echo your commands here $(Variable1)
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  Build:
    //    name: Build job
    //    runs-on: windows-latest
    //    env:
    //      Variable1: new variable
    //    steps:
    //    - uses: actions/checkout@v2
    //    - run: echo your commands here ${{ env.Variable1 }}
    //      shell: cmd
    //";

    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }



    //    [TestMethod]
    //    public void ComplexVariablesWithComplexDependsOnJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //  jobs:
    //  - job: Build
    //    displayName: 'Build job'
    //    pool:
    //        vmImage: 'windows-latest'
    //    dependsOn: 
    //    - AnotherJob
    //    variables:
    //    - group: Active Login   # Contains codesigningCertPassword: Password for code signing cert
    //    - name: sourceArtifactName
    //      value: 'nuget-windows'
    //    - name: targetArtifactName
    //      value: 'nuget-windows-signed'
    //    - name: pathToNugetPackages
    //      value: '**/*.nupkg'

    //    steps:
    //    - task: CmdLine@2
    //      inputs:
    //        script: echo your commands here 
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  Build:
    //    name: Build job
    //    runs-on: windows-latest
    //    needs:
    //    - AnotherJob
    //    env:
    //      group: Active Login
    //      sourceArtifactName: nuget-windows
    //      targetArtifactName: nuget-windows-signed
    //      pathToNugetPackages: '**/*.nupkg'
    //    steps:
    //    - uses: actions/checkout@v2
    //    - run: echo your commands here
    //      shell: cmd
    //";
    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }


    //    [TestMethod]
    //    public void ComplexVariablesWithSimpleDependsOnJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //  jobs:
    //  - job: Build
    //    displayName: 'Build job'
    //    pool:
    //        vmImage: 'windows-latest'
    //    dependsOn: AnotherJob
    //    variables:
    //    - group: Active Login   # Contains codesigningCertPassword: Password for code signing cert
    //    - name: sourceArtifactName
    //      value: 'nuget-windows'
    //    - name: targetArtifactName
    //      value: 'nuget-windows-signed'
    //    - name: pathToNugetPackages
    //      value: '**/*.nupkg'

    //    steps:
    //    - task: CmdLine@2
    //      inputs:
    //        script: echo your commands here 
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  Build:
    //    name: Build job
    //    runs-on: windows-latest
    //    needs:
    //    - AnotherJob
    //    env:
    //      group: Active Login
    //      sourceArtifactName: nuget-windows
    //      targetArtifactName: nuget-windows-signed
    //      pathToNugetPackages: '**/*.nupkg'
    //    steps:
    //    - uses: actions/checkout@v2
    //    - run: echo your commands here
    //      shell: cmd
    //";
    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }



    //    [TestMethod]
    //    public void SimpleVariablesWithSimpleDependsOnJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //jobs:
    //- job: Build
    //  displayName: 'Build job'
    //  pool:
    //    vmImage: windows-latest
    //  dependsOn: AnotherJob
    //  variables:
    //    Variable1: 'new variable'
    //  steps:
    //  - task: CmdLine@2
    //    inputs:
    //      script: echo your commands here $(Variable1)
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  Build:
    //    name: Build job
    //    runs-on: windows-latest
    //    needs:
    //    - AnotherJob
    //    env:
    //      Variable1: new variable
    //    steps:
    //    - uses: actions/checkout@v2
    //    - run: echo your commands here ${{ env.Variable1 }}
    //      shell: cmd
    //";

    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }



    //    [TestMethod]
    //    public void SimpleVariablesWithComplexDependsOnJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //jobs:
    //- job: Build
    //  displayName: 'Build job'
    //  pool:
    //    vmImage: windows-latest
    //  dependsOn: 
    //  - AnotherJob
    //  variables:
    //    Variable1: 'new variable'
    //  steps:
    //  - task: CmdLine@2
    //    inputs:
    //      script: echo your commands here $(Variable1)
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  Build:
    //    name: Build job
    //    runs-on: windows-latest
    //    needs:
    //    - AnotherJob
    //    env:
    //      Variable1: new variable
    //    steps:
    //    - uses: actions/checkout@v2
    //    - run: echo your commands here ${{ env.Variable1 }}
    //      shell: cmd
    //";

    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }

    //    [TestMethod]
    //    public void CheckoutJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //jobs:
    //- job: provisionProd
    //  pool:
    //    vmImage: ubuntu-latest
    //  steps:
    //  - checkout: self
    //  - checkout: git://MyProject/MyRepo
    //  - checkout: MyGitHubRepo # Repo declared in a repository resource
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  provisionProd:
    //    runs-on: ubuntu-latest
    //    steps:
    //    - uses: actions/checkout@v2
    //    - uses: actions/checkout@v2
    //      with:
    //        repository: git://MyProject/MyRepo
    //    - uses: actions/checkout@v2
    //      with:
    //        repository: MyGitHubRepo
    //";

    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }

    //    [TestMethod]
    //    public void CheckoutSimpleJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //jobs:
    //- job: Build
    //  pool:
    //    vmImage: ubuntu-latest
    //  steps:
    //  - checkout: self
    //    submodules: true
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  Build:
    //    runs-on: ubuntu-latest
    //    steps:
    //    - uses: actions/checkout@v2
    //";

    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }

    //    [TestMethod]
    //    public void EnvironmentJobTest()
    //    {
    //        //Arrange
    //        Conversion conversion = new Conversion();
    //        string yaml = @"
    //jobs:
    //- job: provisionProd
    //  displayName: 'Provision Prod'
    //  pool:
    //    vmImage: ubuntu-latest
    //  dependsOn: 
    //  - functionalTestsStaging
    //  environment: 
    //    name: abelNodeDemoAppEnv.prod
    //  steps:
    //  - task: CmdLine@2
    //    inputs:
    //      script: echo hello world
    //";

    //        //Act
    //        ConversionResponse gitHubOutput = conversion.ConvertAzurePipelineToGitHubAction(yaml);

    //        //Assert
    //        string expected = @"
    //jobs:
    //  provisionProd:
    //    name: Provision Prod
    //    runs-on: ubuntu-latest
    //    needs:
    //    - functionalTestsStaging
    //    environment:
    //      name: abelNodeDemoAppEnv.prod
    //    steps:
    //    - uses: actions/checkout@v2
    //    - run: echo hello world
    //      shell: cmd
    //";
    //        //url: https://abel-node-gh-accelerator.azurewebsites.net

    //        expected = UtilityTests.TrimNewLines(expected);
    //        Assert.AreEqual(expected, gitHubOutput.actionsYaml);

    //    }

}
