using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Models.Dependabot;
using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace GitHubActionsDotNet.Tests
{

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestCategory("Dependabot")]
    [TestClass]
    public class DependabotSerializationTests
    {

        [TestMethod]
        public void ScanDotnetSampleProjectTest()
        {
            //arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            projectDirectory += "\\dependabotSamples\\dotnet";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory.Replace("\\", "/");
            }
            List<string> assignees = new() { "samsmithnz" };
            int openPRLimit = 10;
            string interval = "daily";
            string time = "06:00";
            string timezone = "America/New_York";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            string yaml = DependabotSerialization.Serialize(projectDirectory, files, interval, time, timezone, assignees, openPRLimit);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
";

            //If it's a Linux runner, reverse the brackets
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                expected = expected.Replace("\\", "/");
            }
            Assert.AreEqual(expected, yaml);
        }

        [TestMethod]
        public void ScanThisProjectTest()
        {
            //arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory?.Replace("\\", "/");
            }
            List<string> assignees = new() { "samsmithnz" };
            int openPRLimit = 10;

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            string yaml = DependabotSerialization.Serialize(projectDirectory, files, "daily", null, null, assignees, openPRLimit);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /dependabotSamples/dotnet/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: maven
  directory: /dependabotSamples/java/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: npm
  directory: /dependabotSamples/javascript/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: pip
  directory: /dependabotSamples/python/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: bundler
  directory: /dependabotSamples/ruby/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: nuget
  directory: /src/GitHubActionsDotNet.Tests/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: nuget
  directory: /src/GitHubActionsDotNet/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
";

            //If it's a Linux runner, reverse the brackets
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                expected = expected.Replace("\\", "/");
            }
            Assert.AreEqual(expected, yaml);
        }

        [TestMethod]
        public void ScanPomSampleProjectTest()
        {
            //arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            projectDirectory += "\\dependabotSamples\\java";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory.Replace("\\", "/");
            }
            string interval = "daily";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            string yaml = DependabotSerialization.Serialize(projectDirectory, files, interval);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: maven
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

            //If it's a Linux runner, reverse the brackets
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                expected = expected.Replace("\\", "/");
            }
            Assert.AreEqual(expected, yaml);
        }

        [TestMethod]
        public void ScanNPMSampleProjectTest()
        {
            //arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            projectDirectory += "\\dependabotSamples\\javascript";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory.Replace("\\", "/");
            }
            string interval = "daily";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            string yaml = DependabotSerialization.Serialize(projectDirectory, files, interval);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: npm
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

            //If it's a Linux runner, reverse the brackets
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                expected = expected.Replace("\\", "/");
            }
            Assert.AreEqual(expected, yaml);
        }

        [TestMethod]
        public void ScanRubySampleProjectTest()
        {
            //arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            projectDirectory += "\\dependabotSamples\\ruby";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory.Replace("\\", "/");
            }
            string interval = "daily";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            string yaml = DependabotSerialization.Serialize(projectDirectory, files, interval);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: bundler
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

            //If it's a Linux runner, reverse the brackets
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                expected = expected.Replace("\\", "/");
            }
            Assert.AreEqual(expected, yaml);
        }

        [TestMethod]
        public void ScanPythonSampleProjectTest()
        {
            //arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            projectDirectory += "\\dependabotSamples\\python";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory.Replace("\\", "/");
            }
            string interval = "daily";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            string yaml = DependabotSerialization.Serialize(projectDirectory, files, interval);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: pip
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

            //If it's a Linux runner, reverse the brackets
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                expected = expected.Replace("\\", "/");
            }
            Assert.AreEqual(expected, yaml);
        }

        [TestMethod]
        public void CreateEmptyDependabotConfigurationTest()
        {
            //Arrange
            string workingDirectory = System.Environment.CurrentDirectory;

            //Act
            List<string> files = FileSearch.GetFilesForDirectory(workingDirectory);
            string yaml = DependabotSerialization.Serialize(workingDirectory, files);

            //Assert
            string expected = @"version: 2
updates:
- package-ecosystem: github-actions
  directory: /";
            Assert.AreEqual(expected, UtilityTests.TrimNewLines(yaml));
        }

        [TestMethod]
        public void DeserializationTest()
        {
            //Arrange
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            projectDirectory += "\\.github\\dependabot.yml";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                projectDirectory = projectDirectory.Replace("\\", "/");
            }
            string contents = File.ReadAllText(projectDirectory);

            //Act
            DependabotRoot dependabot = DependabotSerialization.Deserialize(contents);

            //Assert
            Assert.IsNotNull(dependabot);
            Assert.IsNotNull(dependabot.updates);
            Assert.AreEqual(2, dependabot.updates.Count);
            Assert.AreEqual("nuget", dependabot.updates[0].package_ecosystem);
            Assert.AreEqual("/src/GitHubActionsDotNet", dependabot.updates[0].directory);
            Assert.AreEqual("daily", dependabot.updates[0].schedule.interval);
            Assert.AreEqual("06:00", dependabot.updates[0].schedule.time);
            Assert.AreEqual("America/New_York", dependabot.updates[0].schedule.timezone);
            Assert.AreEqual("10", dependabot.updates[0].open_pull_requests_limit);
            Assert.AreEqual("samsmithnz", dependabot.updates[0].assignees[0]);
            Assert.AreEqual("*", dependabot.updates[0].groups["core"].patterns[0]);
            Assert.AreEqual("minor", dependabot.updates[0].groups["core"].update_types[0]);
            Assert.AreEqual("patch", dependabot.updates[0].groups["core"].update_types[1]);

        }

    }
}