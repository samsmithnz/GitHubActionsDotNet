using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ContainerTests
{
    [TestMethod]
    public void ContainerImagePropertyTest()
    {
        // Arrange
        Container container = new Container();
        string expectedImage = "node:10.16-jessie";

        // Act
        container.image = expectedImage;

        // Assert
        Assert.AreEqual(expectedImage, container.image);
    }

    [TestMethod]
    public void ContainerEnvironmentVariablesTest()
    {
        // Arrange
        Container container = new Container();
        Dictionary<string, string> expectedEnv = new Dictionary<string, string>
        {
            { "NODE_ENV", "development" },
            { "DEBUG", "true" }
        };

        // Act
        container.env = expectedEnv;

        // Assert
        Assert.AreEqual(expectedEnv, container.env);
        Assert.AreEqual("development", container.env["NODE_ENV"]);
        Assert.AreEqual("true", container.env["DEBUG"]);
    }

    [TestMethod]
    public void ContainerPortsTest()
    {
        // Arrange
        Container container = new Container();
        string[] expectedPorts = { "80", "443", "8080" };

        // Act
        container.ports = expectedPorts;

        // Assert
        Assert.AreEqual(expectedPorts, container.ports);
        Assert.AreEqual(3, container.ports.Length);
        Assert.AreEqual("80", container.ports[0]);
        Assert.AreEqual("443", container.ports[1]);
        Assert.AreEqual("8080", container.ports[2]);
    }

    [TestMethod]
    public void ContainerVolumesTest()
    {
        // Arrange
        Container container = new Container();
        string[] expectedVolumes = { "my_docker_volume:/volume_mount", "/host/path:/container/path" };

        // Act
        container.volumes = expectedVolumes;

        // Assert
        Assert.AreEqual(expectedVolumes, container.volumes);
        Assert.AreEqual(2, container.volumes.Length);
        Assert.AreEqual("my_docker_volume:/volume_mount", container.volumes[0]);
        Assert.AreEqual("/host/path:/container/path", container.volumes[1]);
    }

    [TestMethod]
    public void ContainerOptionsTest()
    {
        // Arrange
        Container container = new Container();
        string expectedOptions = "--cpus 1 --memory 512m";

        // Act
        container.options = expectedOptions;

        // Assert
        Assert.AreEqual(expectedOptions, container.options);
    }

    [TestMethod]
    public void ContainerFullConfigurationTest()
    {
        // Arrange & Act
        Container container = new Container
        {
            image = "node:10.16-jessie",
            env = new Dictionary<string, string>
            {
                { "NODE_ENV", "development" }
            },
            ports = new string[] { "80" },
            volumes = new string[] { "my_docker_volume:/volume_mount" },
            options = "--cpus 1"
        };

        // Assert
        Assert.AreEqual("node:10.16-jessie", container.image);
        Assert.IsNotNull(container.env);
        Assert.AreEqual("development", container.env["NODE_ENV"]);
        Assert.IsNotNull(container.ports);
        Assert.AreEqual("80", container.ports[0]);
        Assert.IsNotNull(container.volumes);
        Assert.AreEqual("my_docker_volume:/volume_mount", container.volumes[0]);
        Assert.AreEqual("--cpus 1", container.options);
    }

    [TestMethod]
    public void ContainerDefaultValuesTest()
    {
        // Arrange & Act
        Container container = new Container();

        // Assert
        Assert.IsNull(container.image);
        Assert.IsNull(container.env);
        Assert.IsNull(container.ports);
        Assert.IsNull(container.volumes);
        Assert.IsNull(container.options);
    }
}