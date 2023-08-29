using GitHubActionsDotNet.Models.Dependabot;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestCategory("Dependabot")]
[TestClass]
public class DependabotDeserializationTests
{
    [TestMethod]
    public void AllowDeserializationTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    allow:
      # Allow updates for Lodash
      - dependency-name: ""lodash""
      # Allow updates for React and any packages starting ""react""
      - dependency-name: ""react*""

  - package-ecosystem: ""composer""
    directory: ""/""
    schedule:
      interval: ""daily""
    allow:
      # Allow both direct and indirect updates for all packages
      - dependency-type: ""all""

  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    allow:
      # Allow only direct updates for
      # Django and any packages starting ""django""
      - dependency-name: ""django*""
        dependency-type: ""direct""
      # Allow only production updates for Sphinx
      - dependency-name: ""sphinx""
        dependency-type: ""production""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void AssigneesTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Add assignees
    assignees:
      - ""octocat""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void CommitMessageTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    commit-message:
      # Prefix all commit messages with ""npm""
      prefix: ""npm""

  - package-ecosystem: ""composer""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Prefix all commit messages with ""Composer""
    # include a list of updated dependencies
    commit-message:
      prefix: ""Composer""
      include: ""scope""

  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Include a list of updated dependencies
    # with a prefix determined by the dependency group
    commit-message:
      prefix: ""pip prod""
      prefix-development: ""pip dev""
      include: ""scope""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void IgnoreTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    ignore:
      - dependency-name: ""express""
        # For Express, ignore all updates for version 4 and 5
        versions: [""4.x"", ""5.x""]
        # For Lodash, ignore all updates
      - dependency-name: ""lodash""
        # For AWS SDK, ignore all patch updates
      - dependency-name: ""aws-sdk""
        update-types: [""version-update:semver-patch""]
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void InsecureCodeExecutionTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  ruby-github:
    type: rubygems-server
    url: https://rubygems.pkg.github.com/octocat/github_api
    token: ${{secrets.MY_GITHUB_PERSONAL_TOKEN}}
updates:
  - package-ecosystem: ""bundler""
    directory: ""/rubygems-server""
    insecure-external-code-execution: allow
    registries: ""*""
    schedule:
      interval: ""monthly""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void LabelsTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Specify labels for npm pull requests
    labels:
      - ""npm""
      - ""dependencies""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void MilestoneTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Associate pull requests with milestone ""4""
    milestone: 4
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void OpenPRTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Disable version updates for npm dependencies
    open-pull-requests-limit: 0

  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Allow up to 10 open pull requests for pip dependencies
    open-pull-requests-limit: 10
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void PRBranchNameSeparatorTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    pull-request-branch-name:
      # Separate sections of the branch name with a hyphen
      # for example, `dependabot-npm_and_yarn-next_js-acorn-6.4.1`
      separator: ""-""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void RebaseStrategyTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Disable rebasing for npm pull requests
    rebase-strategy: ""disabled""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    //TODO: Need to refactor deserialization to work with registries, that can be string or string[]
    //    [TestMethod]
    //    public void RegistriesTest()
    //    {
    //        //Arrange
    //        string yaml = @"version: 2
    //registries:
    //  maven-github:
    //    type: maven-repository
    //    url: https://maven.pkg.github.com/octocat
    //    username: octocat
    //    password: ${{secrets.MY_ARTIFACTORY_PASSWORD}}
    //  npm-npmjs:
    //    type: npm-registry
    //    url: https://registry.npmjs.org
    //    username: octocat
    //    password: ${{secrets.MY_NPM_PASSWORD}}
    //updates:
    //  - package-ecosystem: ""gitsubmodule""
    //    directory: ""/""
    //    registries:
    //      - maven-github
    //    schedule:
    //      interval: ""monthly""
    //";

    //        //Act
    //        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

    //        //Assert
    //        Assert.IsNotNull(dependabot);
    //    }

    [TestMethod]
    public void ReviewersTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Add reviewers
    reviewers:
      - ""octocat""
      - ""my-username""
      - ""my-org/python-team""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void ScheduleDayTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""weekly""
      # Check for npm updates on Sundays
      day: ""sunday""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void ScheduleTimeTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
      # Check for npm updates at 9am UTC
      time: ""09:00""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void ScheduleTimezoneTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
      time: ""09:00""
      # Use Japan Standard Time (UTC +09:00)
      timezone: ""Asia/Tokyo""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void TargetBranchTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Raise pull requests for version updates
    # to pip against the `develop` branch
    target-branch: ""develop""
    # Labels on pull requests for version updates only
    labels:
      - ""pip dependencies""

  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""weekly""
      # Check for npm updates on Sundays
      day: ""sunday""
    # Labels on pull requests for security and version updates
    labels:
      - ""npm dependencies""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void VendorTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""bundler""
    # Raise pull requests to update vendored dependencies that are checked in to the repository
    vendor: true
    directory: ""/""
    schedule:
      interval: ""weekly""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void VersioningStrategyTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
  - package-ecosystem: ""npm""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Update the npm manifest file to relax
    # the version requirements
    versioning-strategy: widen

  - package-ecosystem: ""composer""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Increase the version requirements for Composer
    # only when required
    versioning-strategy: increase-if-necessary

  - package-ecosystem: ""pip""
    directory: ""/""
    schedule:
      interval: ""daily""
    # Only allow updates to the lockfile for pip and
    # ignore any version updates that affect the manifest
    versioning-strategy: lockfile-only
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void InceptionTest()
    {
        //Arrange
        string yaml = @"version: 2
updates:
- package-ecosystem: nuget
  directory: ""/src/GitHubActionsDotNet""
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  open-pull-requests-limit: 10
  assignees:
  - ""MyWebsite""
# Maintain dependencies for GitHub Actions
- package-ecosystem: ""github-actions""
  directory: ""/""
  schedule:
    interval: ""daily""
  assignees:
  - ""MyWebsite""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

    [TestMethod]
    public void MyWebsiteTest()
    {
        //Arrange
        string yaml = @"version: 2
registries:
  nuget-github:
    type: nuget-feed
    url: https://nuget.pkg.github.com/MyWebsite-dotcom/index.json
    username: myemail@gmail.com
    password: ${{ secrets.PACKAGE_PAT_TOKEN }}
  nuget-org:
    type: nuget-feed
    url: https://api.nuget.org/v3/index.json
updates:
- package-ecosystem: nuget
  directory: ""/MyWebsite/MyWebsite.Service""
  registries:
    - nuget-github
    - nuget-org
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  open-pull-requests-limit: 10
  assignees:
    - ""MyWebsite""
- package-ecosystem: nuget
  directory: ""/MyWebsite/MyWebsite.Web""
  registries:
    - nuget-github
    - nuget-org
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  open-pull-requests-limit: 10
  assignees:
    - ""MyWebsite""
- package-ecosystem: nuget
  directory: ""/MyWebsite/MyWebsite.Tests""
  registries:
    - nuget-github
    - nuget-org
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  open-pull-requests-limit: 10
  assignees:
    - ""MyWebsite""
- package-ecosystem: nuget
  directory: ""/MyWebsite/MyWebsite.FunctionalTests""
  registries:
    - nuget-github
    - nuget-org
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  open-pull-requests-limit: 10
  assignees:
    - ""MyWebsite""
# Maintain dependencies for GitHub Actions
- package-ecosystem: ""github-actions""
  directory: ""/""
  schedule:
    interval: ""daily""
  assignees:
  - ""MyWebsite""
";

        //Act
        DependabotRoot dependabot = DependabotSerialization.Deserialize(yaml);

        //Assert
        Assert.IsNotNull(dependabot);
    }

}
