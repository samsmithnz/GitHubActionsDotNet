using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubActionsDotNet;

namespace GitHubActionsDotNet.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        //arrange
        Class1 obj = new();

        //act

        //assert
        Assert.IsNotNull(obj);
    }
}