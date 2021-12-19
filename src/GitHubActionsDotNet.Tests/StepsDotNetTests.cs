using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class StepsDotNetTests
{

    [TestMethod]
    public void UseDotNetIndividualStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetSetupStep();

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Use .NET sdk
  uses: actions/setup-dotnet@v1
  with:
    dotnet-version: 6.x
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetBuildIndividualStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetBuildStep(".NET build",
            "MyWebApp.csproj",
            "Release",
            null,
            false);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: .NET build
  run: dotnet build MyWebApp.csproj --configuration Release
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetBuildIndividualShortParametersStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetBuildStep(".NET build",
            "MyWebApp.csproj",
            "Release",
            null,
            true);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: .NET build
  run: dotnet build MyWebApp.csproj -c Release
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIRestoreIndividualStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetRestoreStep(null,
            "MyWebApp.csproj",
            null);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: .NET restore
  run: dotnet restore MyWebApp.csproj
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLINuGetPushIndividualStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetNuGetPushStep(null,
            "${{ github.workspace }}/*.nupkg",
            "github",
            null,
            false);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Push NuGet package
  run: dotnet nuget push ${{ github.workspace }}/*.nupkg --source github
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIPublishIndividualStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetPublishStep(".NET publish",
            "MyProject.Models/MyProject.Models.csproj",
            "${{ env.BuildConfiguration }}",
            "${{ github.workspace }}",
            null,
            false);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: .NET publish
  run: dotnet publish MyProject.Models/MyProject.Models.csproj --configuration ${{ env.BuildConfiguration }} --output ${{ github.workspace }}
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIPublishMultiLineIndividualStepTest()
    {
        //Arrange
        string script = @"
dotnet publish src/Project.Service/Project.Service.csproj --configuration Release --output ${{ github.workspace }} --runtime win-x64 
dotnet publish src/Project.Web/Project.Web.csproj --configuration Release --output ${{ github.workspace }} --runtime win-x64
";
        Step step = CommonStepsHelper.AddScriptStep("Publish multiple .NET projects", script);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Publish multiple .NET projects
  run: |
    dotnet publish src/Project.Service/Project.Service.csproj --configuration Release --output ${{ github.workspace }} --runtime win-x64 
    dotnet publish src/Project.Web/Project.Web.csproj --configuration Release --output ${{ github.workspace }} --runtime win-x64
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIPackIndividualStepTest()
    {
        //Arrange
        Step step = DotNetStepsHelper.AddDotNetPackStep(".NET pack",
            "MyProject.Models.csproj",
            null,
            null,
            false);

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: .NET pack
  run: dotnet pack MyProject.Models.csproj
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void MSBuildStepTest()
    {
        //Arrange
        Step step = CommonStepsHelper.AddScriptStep(null, @"msbuild '${{ env.solution }}' /p:configuration='${{ env.buildConfiguration }}' /p:platform='${{ env.buildPlatform }}' /p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:DesktopBuildPackageLocation=""${{ github.workspace }}\WebApp.zip"" /p:DeployIisAppPath=""Default Web Site""");

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- run: msbuild '${{ env.solution }}' /p:configuration='${{ env.buildConfiguration }}' /p:platform='${{ env.buildPlatform }}' /p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:DesktopBuildPackageLocation=""${{ github.workspace }}\WebApp.zip"" /p:DeployIisAppPath=""Default Web Site""";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void MSBuild2StepTest()
    {
        //Arrange
        Step step = CommonStepsHelper.AddScriptStep(null, @"msbuild '**/*.sln' /p:configuration='Release' /p:platform='Any CPU' /t:Publish /p:PublishUrl=""publish""");

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- run: msbuild '**/*.sln' /p:configuration='Release' /p:platform='Any CPU' /t:Publish /p:PublishUrl=""publish""
";

        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }


}
