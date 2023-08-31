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
    public void ThisProjectCICDTest()
    {
        //Arrange
        JobHelper jobHelper = new();
        GitHubActionsRoot root = new();
        root.name = "CI/CD";
        root.on = TriggerHelper.AddStandardPushAndPullTrigger("main");

        string displayBuildGitVersionScript = @"
echo ""Version: ${{ steps.gitversion.outputs.SemVer }}""
echo ""CommitsSinceVersionSource: ${{ steps.gitversion.outputs.CommitsSinceVersionSource }}""";

        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(null,null,"0"),
            GitVersionStepHelper.AddGitVersionSetupStep(),
            GitVersionStepHelper.AddGitVersionDetermineVersionStep(),
            CommonStepHelper.AddScriptStep("Display GitVersion outputs", displayBuildGitVersionScript),
            DotNetStepHelper.AddDotNetSetupStep("Setup .NET","7.x"),
            DotNetStepHelper.AddDotNetTestStep(".NET test","src/GitHubActionsDotNet.Tests/GitHubActionsDotNet.Tests.csproj","Release",null,true),
            DotNetStepHelper.AddDotNetPackStep(".NET pack","src/GitHubActionsDotNet/GitHubActionsDotNet.csproj","Release",null,"--include-symbols -p:Version='${{ steps.gitversion.outputs.SemVer }}'", true),
            CommonStepHelper.AddUploadArtifactStep("Upload nuget package back to GitHub","nugetPackage","src/GitHubActionsDotNet/bin/Release","runner.OS == 'Linux'")
        };
        root.jobs = new();
        Job buildJob = jobHelper.AddJob(
            "Build job",
            "${{matrix.os}}",
            buildSteps);
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

        string displayNuGetPushGitVersionScript = @"
echo ""Version: ${{ needs.build.outputs.Version }}""
echo ""CommitsSinceVersionSource: ${{ needs.build.outputs.CommitsSinceVersionSource }}""";

        Step[] nugetPushSteps = new Step[] {
            CommonStepHelper.AddScriptStep("Display GitVersion outputs", displayNuGetPushGitVersionScript),
            CommonStepHelper.AddDownloadArtifactStep("Download nuget package artifact","nugetPackage","nugetPackage"),
            DotNetStepHelper.AddDotNetSetupStep("Setup .NET"),
            GitHubStepHelper.AddCreateReleaseStep("Create Release",
                "v${{ needs.build.outputs.Version }}",
                "v${{ needs.build.outputs.Version }}",
                "needs.build.outputs.CommitsSinceVersionSource > 0"),
            DotNetStepHelper.AddDotNetNuGetPushStep("Publish nuget package to nuget.org",
                "nugetPackage\\*.nupkg",
                @"""https://api.nuget.org/v3/index.json""",
                @"--api-key ""${{ secrets.GHPackagesToken }}""",
                false,
                "needs.build.outputs.CommitsSinceVersionSource > 0")
        };
        Job nugetPushJob = jobHelper.AddJob(
            "Push to NuGet",
            "ubuntu-latest",
            nugetPushSteps,
            new string[] { "build" },
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
    - uses: actions/checkout@v3
      with:
        fetch-depth: 0
    - name: Setup GitVersion
      uses: gittools/actions/gitversion/setup@v0.10.2
      with:
        versionSpec: 5.x
    - name: Determine Version
      id: gitversion
      uses: gittools/actions/gitversion/execute@v0.10.2
    - name: Display GitVersion outputs
      run: |
        echo ""Version: ${{ steps.gitversion.outputs.SemVer }}""
        echo ""CommitsSinceVersionSource: ${{ steps.gitversion.outputs.CommitsSinceVersionSource }}""
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 7.x
    - name: .NET test
      run: dotnet test src/GitHubActionsDotNet.Tests/GitHubActionsDotNet.Tests.csproj -c Release
    - name: .NET pack
      run: dotnet pack src/GitHubActionsDotNet/GitHubActionsDotNet.csproj -c Release --include-symbols -p:Version='${{ steps.gitversion.outputs.SemVer }}'
    - name: Upload nuget package back to GitHub
      uses: actions/upload-artifact@v3
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
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 7.x
    - name: Create Release
      uses: actions/create-release@v1
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      with:
        tag_name: ""v${{ needs.build.outputs.Version }}""
        release_name: ""v${{ needs.build.outputs.Version }}""
      if: needs.build.outputs.CommitsSinceVersionSource > 0
    - name: Publish nuget package to nuget.org
      run: dotnet nuget push nugetPackage\*.nupkg --source ""https://api.nuget.org/v3/index.json"" --api-key ""${{ secrets.GHPackagesToken }}""
      if: needs.build.outputs.CommitsSinceVersionSource > 0
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
