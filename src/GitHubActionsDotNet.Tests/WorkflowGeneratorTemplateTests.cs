using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class WorkflowGeneratorTemplateTests
{
    [TestMethod]
    public void FunctionTest()
    {
        //variables
        string WORKFLOW_NAME = "";
        string BRANCH_NAME = "";
        string AZURE_RESOURCE_NAME = "";
        string PACKAGE_PATH = "";
        string DOTNET_VERSION = "";
        string PROJECT_ROOT = "";
        string PLATFORM = "";

        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepsHelper.AddCheckoutStep(),
            DotNetStepsHelper.AddDotNetSetupStep(),
            CommonStepsHelper.AddScriptStep(null, @"echo ""hello world""", "cmd")
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
name: {WORKFLOW_NAME}
on:
  push:
    branches:
    - {BRANCH_NAME}
env:
  AZURE_FUNCTIONAPP_NAME: {AZURE_RESOURCE_NAME}
  AZURE_FUNCTIONAPP_PACKAGE_PATH: {PACKAGE_PATH}
  CONFIGURATION: Release
  DOTNET_CORE_VERSION: {DOTNET_VERSION}
  WORKING_DIRECTORY: {PROJECT_ROOT}
  DOTNET_CLI_TELEMETRY_OPTOUT: 1
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: 1
  DOTNET_NOLOGO: true
  DOTNET_GENERATE_ASPNET_CERTIFICATE: false
  DOTNET_ADD_GLOBAL_TOOLS_TO_PATH: false
  DOTNET_MULTILEVEL_LOOKUP: 0
jobs:
  build:
    runs-on: {PLATFORM}-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1.8.0
      with:
        dotnet-version: ${{ env.DOTNET_CORE_VERSION }}
    - name: Restore
      run: dotnet restore ""${{ env.WORKING_DIRECTORY }}""
    - name: Build
      run: dotnet build ""${{ env.WORKING_DIRECTORY }}"" --configuration ${{ env.CONFIGURATION }} --no-restore
    - name: Test
      run: dotnet test
    - name: Publish
      run: dotnet publish ""${{ env.WORKING_DIRECTORY }}"" --configuration ${{ env.CONFIGURATION }} --no-build --output ""${{ env.AZURE_FUNCTIONAPP_PACKAGE_PATH }}""
    - name: Deploy to Azure Function App
      uses: Azure/functions-action@v1
      with:
        app-name: ${{ env.AZURE_FUNCTIONAPP_NAME }}
        publish-profile: ${{ secrets.{PUBLISH_PROFILE} }}
        package: ${{ env.AZURE_FUNCTIONAPP_PACKAGE_PATH }}
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void WebappTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Step[] buildSteps = new Step[] {
            CommonStepsHelper.AddCheckoutStep(),
            CommonStepsHelper.AddScriptStep(null, @"echo ""hello world""", "cmd")
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
name: {WORKFLOW_NAME}
on:
  push:
    branches:
    - {BRANCH_NAME}
env:
  AZURE_WEBAPP_NAME: {AZURE_RESOURCE_NAME}
  AZURE_WEBAPP_PACKAGE_PATH: {PACKAGE_PATH}
  CONFIGURATION: Release
  DOTNET_CORE_VERSION: {DOTNET_VERSION}
  WORKING_DIRECTORY: {PROJECT_ROOT}
  DOTNET_CLI_TELEMETRY_OPTOUT: 1
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: 1
  DOTNET_NOLOGO: true
  DOTNET_GENERATE_ASPNET_CERTIFICATE: false
  DOTNET_ADD_GLOBAL_TOOLS_TO_PATH: false
  DOTNET_MULTILEVEL_LOOKUP: 0
jobs:
  build:
    runs-on: {PLATFORM}-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1.8.0
      with:
        dotnet-version: ${{ env.DOTNET_CORE_VERSION }}
    - name: Restore
      run: dotnet restore ""${{ env.WORKING_DIRECTORY }}""
    - name: Build
      run: dotnet build ""${{ env.WORKING_DIRECTORY }}"" --configuration ${{ env.CONFIGURATION }} --no-restore
    - name: Test
      run: dotnet test
    - name: Publish
      run: dotnet publish ""${{ env.WORKING_DIRECTORY }}"" --configuration ${{ env.CONFIGURATION }} -r win-x86 --self-contained true --output ""${{ env.AZURE_WEBAPP_PACKAGE_PATH }}""
    - name: 'Run Azure webapp deploy action using publish profile credentials'
      uses: azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.{PUBLISH_PROFILE} }}
        package: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
