# GitHubActionsDotNet
A project that contains models and helper templates to create GitHub Actions with .NET, and then convert/serialize it to a yaml file. Also supports creation of Dependabot configuration yaml.

[![CI/ CD](https://github.com/samsmithnz/GitHubActionsDotNet/actions/workflows/CICD.yml/badge.svg)](https://github.com/samsmithnz/GitHubActionsDotNet/actions/workflows/CICD.yml)
[![Coverage Status](https://coveralls.io/repos/github/samsmithnz/GitHubActionsDotNet/badge.svg?branch=main)](https://coveralls.io/github/samsmithnz/GitHubActionsDotNet?branch=main)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=samsmithnz_GitHubActionsDotNet&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=samsmithnz_GitHubActionsDotNet)
[![Latest NuGet package](https://img.shields.io/nuget/v/GitHubActionsDotNet)](https://www.nuget.org/packages/GitHubActionsDotNet/)
[![Current Release](https://img.shields.io/github/release/samsmithnz/GitHubActionsDotNet/all.svg)](https://github.com/samsmithnz/GitHubActionsDotNet/releases)

Work in progress. 
- Currently contains models for most GitHub Actions 
    - Helpers to create triggers and common steps. (Could definitely support more steps - [add an issue](https://github.com/samsmithnz/GitHubActionsDotNet/issues/new) if there is one you'd like to see!)
    - Also includes models to create Dependabot configurations 
- Tests current include code to create Actions:
    - Pipelines and triggers
    - Basic jobs
    - Common .NET build steps
    - Basic GitHub steps
    - Basic Azure steps
- Also includes tests to create Dependabot configurations for: 
    - NuGet
    - JavaScript
    - Java (Maven)
    - Ruby
    - Python
    - GitHub Actions  

## How to use

- The models are in the [src/GitHubActionsDotNet/Models](https://github.com/samsmithnz/GitHubActionsDotNet/tree/main/src/GitHubActionsDotNet/Models) folder. You can create a model from scratch by instantiating the model, or using the [src/GitHubActionsDotNet/Helpers](https://github.com/samsmithnz/GitHubActionsDotNet/tree/main/src/GitHubActionsDotNet/Helpers) with many premade templates
- Use the `GitHubActionsSerialization` class to convert the object into a YAML doc. There are variations of the serialization class to convert individual steps and jobs too

**Example C# to build a simple Actions pipeline with GitHubActionsDotNet:**
```C#
GitHubActionsRoot root = new();
root.jobs = new();
root.on = new()
{ 
    push = new() 
    { 
        branches= new string[]
        {
            "main"
        }
    }
};
Job buildJob = JobHelper.AddJob(
    null,
    "windows-latest",
    new Step[]
    {
        CommonStepHelper.AddScriptStep("Hello world", "echo 'hello world'")
    });
root.jobs.Add("build", buildJob);
```

**To serialize:**
```C#
string yaml = Serialization.GitHubActionsSerialization.Serialize(root);
```

**The resultant yaml, looks like this:**
```YAML
on:
  push:
    branches:
    - main
jobs:
  build:
    runs-on: windows-latest
    steps:
    - name: Hello world
      run: echo 'hello world'
```


## Architecture
- Class library project/logic: .NET Standard 2.0
- MSTest/unit tests: .NET 6
