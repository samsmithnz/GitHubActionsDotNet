using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

//Testing the workflow that this project builds and deploys with
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class InceptionTemplateTests
{
    [TestMethod]
    public void GitHubActionsDotNetCICDTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        root.name = "CI/CD";
        root.on = TriggerHelper.AddStandardPushAndPullTrigger("main");
        
        string displayGitVersionScript = @"
echo ""Version: ${{ steps.gitversion.outputs.SemVer }}""
echo ""CommitsSinceVersionSource: ${{ steps.gitversion.outputs.CommitsSinceVersionSource }}""";

        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(null,null,"0"),
            GitVersionStepHelper.AddGitVersionSetupStep(),
            GitVersionStepHelper.AddGitVersionDetermineVersionStep(),
            CommonStepHelper.AddScriptStep("Display GitVersion outputs", displayGitVersionScript),
            DotNetStepHelper.AddDotNetSetupStep("Setup .NET","6.x"),
            DotNetStepHelper.AddDotNetTestStep(".NET test","src/GitHubActionsDotNet.Tests/GitHubActionsDotNet.Tests.csproj","Release",null,true),
            DotNetStepHelper.AddDotNetPackStep(".NET pack","src/GitHubActionsDotNet/GitHubActionsDotNet.csproj","Release",null,"--include-symbols -p:Version='${{ steps.gitversion.outputs.SemVer }}'", true),
            CommonStepHelper.AddUploadArtifactStep("Upload nuget package back to GitHub","nugetPackage","src/GitHubActionsDotNet/bin/Release","runner.OS == 'Linux'")
            //DotNetStepHelper.AddDotNetRestoreStep("Restore","${{ env.WORKING_DIRECTORY }}"),
            //DotNetStepHelper.AddDotNetBuildStep("Build","${{ env.WORKING_DIRECTORY }}","${{ env.CONFIGURATION }}","--no-restore"),
            //DotNetStepHelper.AddDotNetPublishStep("Publish","${{ env.WORKING_DIRECTORY }}", "${{ env.CONFIGURATION }}", "${{ env.AZURE_WEBAPP_PACKAGE_PATH }}", "-r win-x86 --self-contained true"),
            //AzureStepHelper.AddAzureWebappDeployStep("Deploy to Azure Web App","${{ env.AZURE_WEBAPP_NAME }}", "${{ env.AZURE_WEBAPP_PACKAGE_PATH }}")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            "${{matrix.os}}",
            buildSteps,
            null,
            null,
            0);
        //Add the strategy
        buildJob.strategy = new()
        {
            matrix = new()
            {
                { "os", new string[] { "ubuntu-latest", "windows-latest" } }
            }
        };
        buildJob.outputs = new()
        {
            { "Version", "${{ steps.gitversion.outputs.SemVer }}" },
            { "CommitsSinceVersionSource", "${{ steps.gitversion.outputs.CommitsSinceVersionSource }}" }
        };
        root.jobs.Add("build", buildJob);

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
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest]
    runs-on: ${{matrix.os}}
    outputs:
      Version: ${{ steps.gitversion.outputs.SemVer }}
      CommitsSinceVersionSource: ${{ steps.gitversion.outputs.CommitsSinceVersionSource }}
    steps:
    - uses: actions/checkout@v2
      with:
        fetch-depth: 0
    - name: Install GitVersion
      uses: gittools/actions/gitversion/setup@v0.9.11
      with:
        versionSpec: 5.x
    - name: Determine Version
      uses: gittools/actions/gitversion/execute@v0.9.11
      id: gitversion
    - name: Display GitVersion outputs
      run: |
        echo ""Version: ${{ steps.gitversion.outputs.SemVer }}""
        echo ""CommitsSinceVersionSource: ${{ steps.gitversion.outputs.CommitsSinceVersionSource }}""
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 6.x
    - name: .NET test
      run: dotnet test src/GitHubActionsDotNet.Tests/GitHubActionsDotNet.Tests.csproj -c Release
    - name: .NET pack
      run: dotnet pack src/GitHubActionsDotNet/GitHubActionsDotNet.csproj -c Release --include-symbols -p:Version='${{ steps.gitversion.outputs.SemVer }}'
    - name: Upload nuget package back to GitHub
      uses: actions/upload-artifact@v2
      if: runner.OS == 'Linux'
      with:
        name: nugetPackage
        path: src/GitHubActionsDotNet/bin/Release
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
