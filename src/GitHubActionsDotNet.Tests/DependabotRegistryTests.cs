using GitHubActionsDotNet.Models.Dependabot;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestCategory("Dependabot")]
[TestClass]
public class DependabotRegistryTests
{
    [TestMethod]
    public void ComposerRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  composer:
    type: composer-repository
    url: https://repo.packagist.com/example-company/
    username: octocat
    password: ${{secrets.MY_PACKAGIST_PASSWORD}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void DockerRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  dockerhub:
    type: docker-registry
    url: https://registry.hub.docker.com
    username: octocat
    password: ${{secrets.MY_DOCKERHUB_PASSWORD}}
  ecr-docker:
    type: docker-registry
    url: https://1234567890.dkr.ecr.us-east-1.amazonaws.com
    username: ${{secrets.ECR_AWS_ACCESS_KEY_ID}}
    password: ${{secrets.ECR_AWS_SECRET_ACCESS_KEY}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void GitRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  github-octocat:
    type: git
    url: https://github.com
    username: x-access-token
    password: ${{secrets.MY_GITHUB_PERSONAL_TOKEN}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void HexOrganizationRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  github-hex-org:
    type: hex-organization
    organization: github
    key: ${{secrets.MY_HEX_ORGANIZATION_KEY}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void MavenRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  maven-artifactory:
    type: maven-repository
    url: https://artifactory.example.com
    username: octocat
    password: ${{secrets.MY_ARTIFACTORY_PASSWORD}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void NPMRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  npm-npmjs:
    type: npm-registry
    url: https://registry.npmjs.org
    username: octocat
    password: ${{secrets.MY_NPM_PASSWORD}}  # Must be an unencoded password
  npm-github:
    type: npm-registry
    url: https://npm.pkg.github.com
    token: ${{secrets.MY_GITHUB_PERSONAL_TOKEN}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void NuGetRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  nuget-example:
    type: nuget-feed
    url: https://nuget.example.com/v3/index.json
    username: octocat@example.com
    password: ${{secrets.MY_NUGET_PASSWORD}}
  nuget-azure-devops:
    type: nuget-feed
    url: https://pkgs.dev.azure.com/.../_packaging/My_Feed/nuget/v3/index.json
    token: ${{secrets.MY_AZURE_DEVOPS_TOKEN}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void PythonRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  python-example:
    type: python-index
    url: https://example.com/_packaging/my-feed/pypi/example
    username: octocat
    password: ${{secrets.MY_BASIC_AUTH_PASSWORD}}
    replaces-base: true
  python-azure:
    type: python-index
    url: https://pkgs.dev.azure.com/octocat/_packaging/my-feed/pypi/example
    token: ${{secrets.MY_AZURE_DEVOPS_TOKEN}}
    replaces-base: true
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void RubygemsRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  ruby-example:
    type: rubygems-server
    url: https://rubygems.example.com
    username: octocat@example.com
    password: ${{secrets.MY_RUBYGEMS_PASSWORD}}
  ruby-github:
    type: rubygems-server
    url: https://rubygems.pkg.github.com/octocat/github_api
    token: ${{secrets.MY_GITHUB_PERSONAL_TOKEN}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void TerraformRegistryTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  terraform-example:
    type: terraform-registry
    url: https://terraform.example.com
    token: ${{secrets.MY_TERRAFORM_API_TOKEN}}
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }


}
