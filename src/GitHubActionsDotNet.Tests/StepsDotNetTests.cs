using GitHubActionsDotNet.Models;
using GitHubActionsDotNet.Serialization;
using GitHubActionsDotNet.Templates.Steps;
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
        Step step = DotNetSteps.CreateDotNetUseStep();

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
        Step step = DotNetSteps.CreateDotNetBuildStep(".NET build",
            "MyWebApp.csproj", 
            "Release",
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
        Step step = DotNetSteps.CreateDotNetBuildStep(".NET build",
            "MyWebApp.csproj",
            "Release",
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
        Step step = DotNetSteps.CreateDotNetRestoreStep(null,
            "MyWebApp.csproj");

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
        Step step = DotNetSteps.CreateDotNetNuGetPushStep(null,
            "${{ github.workspace }}/*.nupkg",
            "github"
            );

        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Push NuGet package to GitHub Packages
  run: dotnet nuget push ${{ github.workspace }}/*.nupkg --source github
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIBuildIndividualStepTest()
    {
        //Arrange
        Step step = new();


        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Build
  run: dotnet build MyProject/MyProject.Models/MyProject.Models.csproj --configuration ${{ env.BuildConfiguration }}
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIPublishIndividualStepTest()
    {
        //Arrange
        Step step = new();


        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Publish
  run: dotnet publish MyProject/MyProject.Models/MyProject.Models.csproj --configuration ${{ env.BuildConfiguration }} --output ${{ github.workspace }}
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIPublishMultiLineIndividualStepTest()
    {
        //Arrange
        Step step = new();


        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: Publish dotnet projects
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
        Step step = new();


        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- name: dotnet pack
  run: dotnet pack MyProject/MyProject.Models/MyProject.Models.csproj
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void DotNetCoreCLIInvalidIndividualStepTest()
    {
        //Arrange
        Step step = new();


        //Act
        string yaml = GitHubActionsSerialization.SerializeStep(step);

        //Assert
        string expected = @"
- # This DotNetCoreCLI task is misconfigured, inputs are required
  name: dotnet build but no inputs
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void MSBuildStepTest()
    {
        //Arrange
        Step step = new();


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
        Step step = new();


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
