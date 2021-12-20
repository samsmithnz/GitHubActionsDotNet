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
        string workflow_name = "Workflow generator for functions";
        string branch_name = "main";
        string azure_resource_name = "myazurefunction";
        string package_path = "function/function.zip";
        string dotnet_version = "3.1.x";
        string project_root = "src/";
        string platform = "windows";

        //Arrange
        GitHubActionsRoot root = new();
        root.name = workflow_name;
        root.on = TriggerHelper.AddStandardPushTrigger(branch_name);
        root.env = new()
        {
            { "AZURE_FUNCTIONAPP_NAME", azure_resource_name },
            { "AZURE_FUNCTIONAPP_PACKAGE_PATH", package_path },
            { "CONFIGURATION", "Release" },
            { "DOTNET_CORE_VERSION", dotnet_version },
            { "WORKING_DIRECTORY", project_root },
            { "DOTNET_CLI_TELEMETRY_OPTOUT", "1" },
            { "DOTNET_SKIP_FIRST_TIME_EXPERIENCE", "1" },
            { "DOTNET_NOLOGO", "true" },
            { "DOTNET_GENERATE_ASPNET_CERTIFICATE", "false" },
            { "DOTNET_ADD_GLOBAL_TOOLS_TO_PATH", "false" },
            { "DOTNET_MULTILEVEL_LOOKUP", "0" }
        };
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            DotNetStepHelper.AddDotNetSetupStep("Setup .NET Core","${{ env.DOTNET_CORE_VERSION }}"),
            DotNetStepHelper.AddDotNetRestoreStep("Restore","${{ env.WORKING_DIRECTORY }}"),
            DotNetStepHelper.AddDotNetBuildStep("Build","${{ env.WORKING_DIRECTORY }}","${{ env.CONFIGURATION }}","--no-restore"),
            DotNetStepHelper.AddDotNetTestStep("Test"),
            DotNetStepHelper.AddDotNetPublishStep("Publish","${{ env.WORKING_DIRECTORY }}", "${{ env.CONFIGURATION }}", "${{ env.AZURE_FUNCTIONAPP_PACKAGE_PATH }}", "--no-build"),
            AzureStepHelper.AddAzureFunctionDeployStep("Deploy to Azure Function App","${{ env.AZURE_FUNCTIONAPP_NAME }}", "${{ env.AZURE_FUNCTIONAPP_PACKAGE_PATH }}")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            platform + "-latest",
            buildSteps,
            null,
            null,
            0);
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
name: Workflow generator for functions
on:
  push:
    branches:
    - main
env:
  AZURE_FUNCTIONAPP_NAME: myazurefunction
  AZURE_FUNCTIONAPP_PACKAGE_PATH: function/function.zip
  CONFIGURATION: Release
  DOTNET_CORE_VERSION: 3.1.x
  WORKING_DIRECTORY: src/
  DOTNET_CLI_TELEMETRY_OPTOUT: 1
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: 1
  DOTNET_NOLOGO: true
  DOTNET_GENERATE_ASPNET_CERTIFICATE: false
  DOTNET_ADD_GLOBAL_TOOLS_TO_PATH: false
  DOTNET_MULTILEVEL_LOOKUP: 0
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: ${{ env.DOTNET_CORE_VERSION }}
    - name: Restore
      run: dotnet restore ${{ env.WORKING_DIRECTORY }}
    - name: Build
      run: dotnet build ${{ env.WORKING_DIRECTORY }} --configuration ${{ env.CONFIGURATION }} --no-restore
    - name: Test
      run: dotnet test
    - name: Publish
      run: dotnet publish ${{ env.WORKING_DIRECTORY }} --configuration ${{ env.CONFIGURATION }} --output ${{ env.AZURE_FUNCTIONAPP_PACKAGE_PATH }} --no-build
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
        //variables
        string workflow_name = "Workflow generator for webapps";
        string branch_name = "main";
        string azure_resource_name = "myazurewebapp";
        string package_path = "webapp/webapp.zip";
        string dotnet_version = "3.1.x";
        string project_root = "src/";
        string platform = "windows";

        //Arrange
        GitHubActionsRoot root = new();
        root.name = workflow_name;
        root.on = TriggerHelper.AddStandardPushTrigger(branch_name);
        root.env = new()
        {
            { "AZURE_WEBAPP_NAME", azure_resource_name },
            { "AZURE_WEBAPP_PACKAGE_PATH", package_path },
            { "CONFIGURATION", "Release" },
            { "DOTNET_CORE_VERSION", dotnet_version },
            { "WORKING_DIRECTORY", project_root },
            { "DOTNET_CLI_TELEMETRY_OPTOUT", "1" },
            { "DOTNET_SKIP_FIRST_TIME_EXPERIENCE", "1" },
            { "DOTNET_NOLOGO", "true" },
            { "DOTNET_GENERATE_ASPNET_CERTIFICATE", "false" },
            { "DOTNET_ADD_GLOBAL_TOOLS_TO_PATH", "false" },
            { "DOTNET_MULTILEVEL_LOOKUP", "0" }
        };
        Step[] buildSteps = new Step[] {
            CommonStepHelper.AddCheckoutStep(),
            DotNetStepHelper.AddDotNetSetupStep("Setup .NET Core","${{ env.DOTNET_CORE_VERSION }}"),
            DotNetStepHelper.AddDotNetRestoreStep("Restore","${{ env.WORKING_DIRECTORY }}"),
            DotNetStepHelper.AddDotNetBuildStep("Build","${{ env.WORKING_DIRECTORY }}","${{ env.CONFIGURATION }}","--no-restore"),
            DotNetStepHelper.AddDotNetTestStep("Test"),
            DotNetStepHelper.AddDotNetPublishStep("Publish","${{ env.WORKING_DIRECTORY }}", "${{ env.CONFIGURATION }}", "${{ env.AZURE_WEBAPP_PACKAGE_PATH }}", "-r win-x86 --self-contained true"),
            AzureStepHelper.AddAzureWebappDeployStep("Deploy to Azure Web App","${{ env.AZURE_WEBAPP_NAME }}", "${{ env.AZURE_WEBAPP_PACKAGE_PATH }}")
        };
        root.jobs = new();
        Job buildJob = JobHelper.AddJob(
            "Build job",
            platform + "-latest",
            buildSteps,
            null,
            null,
            0);
        root.jobs.Add("build", buildJob);

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
name: Workflow generator for webapps
on:
  push:
    branches:
    - main
env:
  AZURE_WEBAPP_NAME: myazurewebapp
  AZURE_WEBAPP_PACKAGE_PATH: webapp/webapp.zip
  CONFIGURATION: Release
  DOTNET_CORE_VERSION: 3.1.x
  WORKING_DIRECTORY: src/
  DOTNET_CLI_TELEMETRY_OPTOUT: 1
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: 1
  DOTNET_NOLOGO: true
  DOTNET_GENERATE_ASPNET_CERTIFICATE: false
  DOTNET_ADD_GLOBAL_TOOLS_TO_PATH: false
  DOTNET_MULTILEVEL_LOOKUP: 0
jobs:
  build:
    name: Build job
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: ${{ env.DOTNET_CORE_VERSION }}
    - name: Restore
      run: dotnet restore ${{ env.WORKING_DIRECTORY }}
    - name: Build
      run: dotnet build ${{ env.WORKING_DIRECTORY }} --configuration ${{ env.CONFIGURATION }} --no-restore
    - name: Test
      run: dotnet test
    - name: Publish
      run: dotnet publish ${{ env.WORKING_DIRECTORY }} --configuration ${{ env.CONFIGURATION }} --output ${{ env.AZURE_WEBAPP_PACKAGE_PATH }} -r win-x86 --self-contained true
    - name: Deploy to Azure Web App
      uses: Azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.{PUBLISH_PROFILE} }}
        package: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }
}
