# GitHubActionsDotNet
A project that contains models and templates to create GitHub Actions with .NET

[![CI/ CD](https://github.com/samsmithnz/GitHubActionsDotNet/actions/workflows/CICD.yml/badge.svg)](https://github.com/samsmithnz/GitHubActionsDotNet/actions/workflows/CICD.yml)
[![Latest NuGet package](https://img.shields.io/nuget/v/GitHubActionsDotNet)](https://www.nuget.org/packages/GitHubActionsDotNet/)
![Current Release](https://img.shields.io/github/release/samsmithnz/GitHubActionsDotNet/all.svg)

Work in progress. 
- Currently contains most GitHub Actions models.
- Tests to create 
    - very basic pipelines and triggers
    - very basic jobs
    - Common .NET build steps

## How to use

- The models are in the [src/GitHubActionsDotNet/Models](https://github.com/samsmithnz/GitHubActionsDotNet/tree/main/src/GitHubActionsDotNet/Models) folder. You can create a model from scratch by instantating the model, or using the [src/GitHubActionsDotNet/Helpers](https://github.com/samsmithnz/GitHubActionsDotNet/tree/main/src/GitHubActionsDotNet/Helpers) with many premade templates
- Use the `GitHubActionsSerialization` class to convert the object into a YAML doc. There are variations of the serialization class to convert individual steps and jobs too
 
## Architecture
- Class library project/logic: .NET Standard 2.0
- MSTest/unit tests: .NET 6
