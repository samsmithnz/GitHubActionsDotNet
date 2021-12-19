
using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class TriggerTests
{
    [TestMethod]
    public void TriggerAndPRMainSimpleStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        root.on = TriggerHelper.AddStandardPushAndPullTrigger();

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
  pull-request:
    branches:
    - main
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void TriggerMainSimpleStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        root.on = TriggerHelper.AddStandardPushTrigger();

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void TriggerSimpleStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches = new string[1];
        trigger.push.branches[0] = "main";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void TriggerSimpleWithMultipleBranchesStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches = new string[2];
        trigger.push.branches[0] = "main";
        trigger.push.branches[1] = "develop";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
    - develop
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void TriggerComplexStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches = new string[1];
        trigger.push.branches[0] = "features/*";
        trigger.push.paths = new string[1];
        trigger.push.paths[0] = "README.md";
        trigger.push.tags = new string[2];
        trigger.push.tags[0] = "v1";
        trigger.push.tags[1] = "v1.*";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - features/*
    paths:
    - README.md
    tags:
    - v1
    - v1.*
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void PRComplexWithPRStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.pull_request = new();
        trigger.pull_request.branches = new string[1];
        trigger.pull_request.branches[0] = "features/*";
        trigger.pull_request.paths = new string[1];
        trigger.pull_request.paths[0] = "README.md";
        trigger.pull_request.tags = new string[2];
        trigger.pull_request.tags[0] = "v1";
        trigger.pull_request.tags[1] = "v1.*";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"on:
  pull-request:
    branches:
    - features/*
    paths:
    - README.md
    tags:
    - v1
    - v1.*
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void TriggerComplexWithIgnoresStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches_ignore = new string[1];
        trigger.push.branches_ignore[0] = "features/experimental/*";
        trigger.push.paths_ignore = new string[1];
        trigger.push.paths_ignore[0] = "README.md";
        trigger.push.tags_ignore = new string[2];
        trigger.push.tags_ignore[0] = "v1";
        trigger.push.tags_ignore[1] = "v1.*";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"on:
  push:
    branches-ignore:
    - features/experimental/*
    paths-ignore:
    - README.md
    tags-ignore:
    - v1
    - v1.*
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void PRTriggerComplexPRWithIgnoresStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.pull_request = new();
        trigger.pull_request.branches_ignore = new string[1];
        trigger.pull_request.branches_ignore[0] = "features/experimental/*";
        trigger.pull_request.paths_ignore = new string[1];
        trigger.pull_request.paths_ignore[0] = "README.md";
        trigger.pull_request.tags_ignore = new string[2];
        trigger.pull_request.tags_ignore[0] = "v1";
        trigger.pull_request.tags_ignore[1] = "v1.*";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  pull-request:
    branches-ignore:
    - features/experimental/*
    paths-ignore:
    - README.md
    tags-ignore:
    - v1
    - v1.*
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void TriggerComplexMasterAndPRWithIgnoresStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches_ignore = new string[1];
        trigger.push.branches_ignore[0] = "features/experimental/*";
        trigger.push.paths_ignore = new string[1];
        trigger.push.paths_ignore[0] = "README.md";
        trigger.pull_request = new();
        trigger.pull_request.branches_ignore = new string[1];
        trigger.pull_request.branches_ignore[0] = "features/experimental/*";
        trigger.pull_request.paths_ignore = new string[1];
        trigger.pull_request.paths_ignore[0] = "README.md";
        trigger.pull_request.tags_ignore = new string[2];
        trigger.pull_request.tags_ignore[0] = "v1";
        trigger.pull_request.tags_ignore[1] = "v1.*";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches-ignore:
    - features/experimental/*
    paths-ignore:
    - README.md
  pull-request:
    branches-ignore:
    - features/experimental/*
    paths-ignore:
    - README.md
    tags-ignore:
    - v1
    - v1.*
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void ScheduleCronTriggerTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.schedule = new string[1];
        trigger.schedule[0] = "cron: '0 0 3/4 ? * * *'";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  schedule:
  - cron: '0 0 3/4 ? * * *'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void ScheduleCronWithDoubleQuotesTriggerTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.schedule = new string[1];
        trigger.schedule[0] = "cron: '0 0 1/4 ? * * *'";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  schedule:
  - cron: '0 0 1/4 ? * * *'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void SchedulesCronRemoveExtraStringTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.schedule = new string[2];
        trigger.schedule[0] = "cron: '0 0 * * *'";
        trigger.schedule[1] = "cron: '0 2 * * *'";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  schedule:
  - cron: '0 0 * * *'
  - cron: '0 2 * * *'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void ScheduleTriggerTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.schedule = new string[1];
        trigger.schedule[0] = "cron: '0 0 * **'";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  schedule:
  - cron: '0 0 * **'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }



    [TestMethod]
    public void OnPushAndScheduleCronTriggerTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches = new string[1];
        trigger.push.branches[0] = "main";
        trigger.schedule = new string[1];
        trigger.schedule[0] = "cron: '0 0 3/4 ? * * *'";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
  schedule:
  - cron: '0 0 3/4 ? * * *'
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }


    [TestMethod]
    public void TriggerWorkflowDispatchTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        Trigger trigger = new();
        trigger.push = new();
        trigger.push.branches = new string[1];
        trigger.push.branches[0] = "main";
        trigger.workflow_dispatch = "";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
on:
  push:
    branches:
    - main
  workflow_dispatch:
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);

    }

    [TestMethod]
    public void TriggerWorkflowDispatchNoTriggerTest()
    {
        //Arrange
        GitHubActionsRoot root = new();
        root.name = "test trigger pipelines";
        Trigger trigger = new();
        trigger.workflow_dispatch = "";
        root.on = trigger;

        //Act
        string yaml = Serialization.GitHubActionsSerialization.Serialize(root);

        //Assert
        string expected = @"
name: test trigger pipelines
on:
  workflow_dispatch:
";
        expected = UtilityTests.TrimNewLines(expected);
        Assert.AreEqual(expected, yaml);
    }

}
