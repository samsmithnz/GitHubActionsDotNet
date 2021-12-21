# GitHubActionsDotNet
A project that contains models and helper templates to create GitHub Actions with .NET, and then convert/serialize it to a yaml file

[![CI/ CD](https://github.com/samsmithnz/GitHubActionsDotNet/actions/workflows/CICD.yml/badge.svg)](https://github.com/samsmithnz/GitHubActionsDotNet/actions/workflows/CICD.yml)
[![Latest NuGet package](https://img.shields.io/nuget/v/GitHubActionsDotNet)](https://www.nuget.org/packages/GitHubActionsDotNet/)
![Current Release](https://img.shields.io/github/release/samsmithnz/GitHubActionsDotNet/all.svg)

Work in progress. 
- Currently contains most GitHub Actions models.
- Tests current include code to create:
    - Pipelines and triggers
    - Basic jobs
    - Common .NET build steps
    - Basic GitHub steps
    - Basic Azure steps

## How to use

- The models are in the [src/GitHubActionsDotNet/Models](https://github.com/samsmithnz/GitHubActionsDotNet/tree/main/src/GitHubActionsDotNet/Models) folder. You can create a model from scratch by instantating the model, or using the [src/GitHubActionsDotNet/Helpers](https://github.com/samsmithnz/GitHubActionsDotNet/tree/main/src/GitHubActionsDotNet/Helpers) with many premade templates
- Use the `GitHubActionsSerialization` class to convert the object into a YAML doc. There are variations of the serialization class to convert individual steps and jobs too

**Example C# to build a simple pipeline with GitHubActionsDotNet:**
```
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
    "windows-latest");
root.jobs.Add("build", buildJob);
```

**To serialize:**
```
string yaml = Serialization.GitHubActionsSerialization.Serialize(root);
```

**The resultant yaml, will look like this:**
```
on:
  push:
    branches:
    - main
jobs:
  build:
    runs-on: windows-latest
```


## Architecture
- Class library project/logic: .NET Standard 2.0
- MSTest/unit tests: .NET 6
