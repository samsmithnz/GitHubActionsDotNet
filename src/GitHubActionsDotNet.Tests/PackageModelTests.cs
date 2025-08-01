using GitHubActionsDotNet.Models.Dependabot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class PackageModelTests
{
    [TestMethod]
    public void PackageStringRegistriesTest()
    {
        // Arrange
        PackageString package = new PackageString();
        string expectedRegistry = "my-registry";

        // Act
        package.registries = expectedRegistry;

        // Assert
        Assert.AreEqual(expectedRegistry, package.registries);
    }

    [TestMethod]
    public void PackageStringArrayRegistriesTest()
    {
        // Arrange
        PackageStringArray package = new PackageStringArray();
        List<object> registryList = new List<object> { "registry1", "registry2" };

        // Act
        package.registries = registryList;

        // Assert
        Assert.IsNotNull(package.registries);
        string[] registries = package.registries;
        Assert.AreEqual(2, registries.Length);
        Assert.AreEqual("registry1", registries[0]);
        Assert.AreEqual("registry2", registries[1]);
    }

    [TestMethod]
    public void PackageStringArrayRegistriesNullTest()
    {
        // Arrange
        PackageStringArray package = new PackageStringArray();

        // Act
        package.registries = null;

        // Assert
        Assert.IsNull(package.registries);
    }

    [TestMethod]
    public void PackageBasePropertiesTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            package_ecosystem = "npm",
            directory = "/",
            schedule = new Schedule { interval = "daily" },
            assignees = new List<string> { "user1", "user2" },
            open_pull_requests_limit = "10",
            insecure_external_code_execution = "allow",
            labels = new string[] { "dependencies" },
            milestone = 1,
            target_branch = "main",
            vendor = true,
            versioning_strategy = "auto"
        };

        // Assert
        Assert.AreEqual("npm", package.package_ecosystem);
        Assert.AreEqual("/", package.directory);
        Assert.IsNotNull(package.schedule);
        Assert.AreEqual("daily", package.schedule.interval);
        Assert.IsNotNull(package.assignees);
        Assert.AreEqual(2, package.assignees.Count);
        Assert.AreEqual("user1", package.assignees[0]);
        Assert.AreEqual("10", package.open_pull_requests_limit);
        Assert.AreEqual("allow", package.insecure_external_code_execution);
        Assert.IsNotNull(package.labels);
        Assert.AreEqual("dependencies", package.labels[0]);
        Assert.AreEqual(1, package.milestone);
        Assert.AreEqual("main", package.target_branch);
        Assert.AreEqual(true, package.vendor);
        Assert.AreEqual("auto", package.versioning_strategy);
    }

    [TestMethod]
    public void PackageWithAllowIgnoreTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            allow = new Allow[]
            {
                new Allow { dependency_type = "direct" }
            },
            ignore = new Ignore[]
            {
                new Ignore { dependency_name = "lodash" }
            }
        };

        // Assert
        Assert.IsNotNull(package.allow);
        Assert.AreEqual(1, package.allow.Length);
        Assert.AreEqual("direct", package.allow[0].dependency_type);
        Assert.IsNotNull(package.ignore);
        Assert.AreEqual(1, package.ignore.Length);
        Assert.AreEqual("lodash", package.ignore[0].dependency_name);
    }

    [TestMethod]
    public void PackageWithCommitMessageTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            commit_message = new CommitMessage 
            { 
                prefix = "deps",
                include = "scope"
            }
        };

        // Assert
        Assert.IsNotNull(package.commit_message);
        Assert.AreEqual("deps", package.commit_message.prefix);
        Assert.AreEqual("scope", package.commit_message.include);
    }

    [TestMethod]
    public void PackageWithPullRequestBranchNameTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            pull_request_branch_name = new PullRequestBranchName 
            { 
                separator = "-"
            }
        };

        // Assert
        Assert.IsNotNull(package.pull_request_branch_name);
        Assert.AreEqual("-", package.pull_request_branch_name.separator);
    }

    [TestMethod]
    public void PackageWithGroupsTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            groups = new Dictionary<string, Group>
            {
                { "test-group", new Group { patterns = new string[] { "*" } } }
            }
        };

        // Assert
        Assert.IsNotNull(package.groups);
        Assert.AreEqual(1, package.groups.Count);
        Assert.IsTrue(package.groups.ContainsKey("test-group"));
        Assert.IsNotNull(package.groups["test-group"].patterns);
        Assert.AreEqual("*", package.groups["test-group"].patterns[0]);
    }

    [TestMethod]
    public void PackageReviewersTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            reviewers = new string[] { "reviewer1", "reviewer2" }
        };

        // Assert
        Assert.IsNotNull(package.reviewers);
        Assert.AreEqual(2, package.reviewers.Length);
        Assert.AreEqual("reviewer1", package.reviewers[0]);
        Assert.AreEqual("reviewer2", package.reviewers[1]);
    }

    [TestMethod]
    public void PackageRebaseStrategyTest()
    {
        // Arrange & Act
        Package package = new Package
        {
            rebase_strategy = "disabled"
        };

        // Assert
        Assert.AreEqual("disabled", package.rebase_strategy);
    }
}