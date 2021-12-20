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

        Step[] nugetPushSteps = new Step[] {
            CommonStepHelper.AddScriptStep("Display GitVersion outputs", displayGitVersionScript),
            CommonStepHelper.AddDownloadArtifactStep("Download nuget package artifact","nugetPackage","nugetPackage"),
            DotNetStepHelper.AddDotNetSetupStep("Setup .NET"),
            GitHubHelper.AddCreateReleaseStep("Create Release",
                "${{ needs.build.outputs.Version }}",
                "Release ${{ needs.build.outputs.Version }}",
                "needs.build.outputs.CommitsSinceVersionSource > 0"),
            DotNetStepHelper.AddPublishNuGetPackage("Publish nuget package to nuget.org", 
                "nugetPackage\\*.nupkg", 
                @"""${{ secrets.GHPackagesToken }}""",
                @"""https://api.nuget.org/v3/index.json""",
                "needs.build.outputs.CommitsSinceVersionSource > 0")
        };
        Job nugetPushJob = JobHelper.AddJob(
            "Push to NuGet",
            "${{matrix.os}}",
            nugetPushSteps,
            null,
            new string[] { "build" },
            0,
            null,
            "github.ref == 'refs/heads/main'");
        root.jobs.Add("NuGetPush", nugetPushJob);


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
    strategy:
      matrix:
        os:
        - ubuntu-latest
        - windows-latest
    runs-on: ${{matrix.os}}
    outputs:
      Version: ${{ steps.gitversion.outputs.SemVer }}
      CommitsSinceVersionSource: ${{ steps.gitversion.outputs.CommitsSinceVersionSource }}
    steps:
    - uses: actions/checkout@v2
      with:
        fetch-depth: 0
    - name: Setup GitVersion
      uses: gittools/actions/gitversion/setup@v0.9.11
      with:
        versionSpec: 5.x
    - name: Determine Version
      id: gitversion
      uses: gittools/actions/gitversion/execute@v0.9.11
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
      with:
        name: nugetPackage
        path: src/GitHubActionsDotNet/bin/Release
      if: runner.OS == 'Linux'
  NuGetPush:
    name: Push to NuGet
    runs-on: ubuntu-latest
    needs: 
    - build
    if: github.ref == 'refs/heads/main'
    steps:
    - name: Display GitVersion outputs
      run: |
        echo ""Version: ${{ needs.build.outputs.Version }}""
        echo ""CommitsSinceVersionSource: ${{ needs.build.outputs.CommitsSinceVersionSource }}""
    - name: Download nuget package artifact
      uses: actions/download-artifact@v2.1.0
      with:
        name: nugetPackage
        path: nugetPackage
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 6.x
    - name: Create Release
      uses: actions/create-release@v1
      if: needs.build.outputs.CommitsSinceVersionSource > 0
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      with:
        tag_name: ${{ needs.build.outputs.Version }}
        release_name: Release ${{ needs.build.outputs.Version }}
    - name: Publish nuget package to nuget.org
      if: needs.build.outputs.CommitsSinceVersionSource > 0
      run: dotnet nuget push nugetPackage\*.nupkg --api-key ""${{ secrets.GHPackagesToken }}"" --source ""https://api.nuget.org/v3/index.json""
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
