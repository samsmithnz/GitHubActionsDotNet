using GitHubActionsDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[TestClass]
public class ModelTests
{
    [TestMethod]
    public void EnvironmentTest()
    {
        //arrange
        Environment environment = new();

        //act

        //assert
        Assert.IsNotNull(environment);
    }
}