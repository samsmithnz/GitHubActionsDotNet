using GitHubActionsDotNet.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class StringExtensionTests
{
    [TestMethod]
    public void SplitSingleSeparatorTest()
    {
        // Arrange
        string input = "apple,banana,cherry";
        string separator = ",";

        // Act
        string[] result = input.Split(separator);

        // Assert
        Assert.AreEqual(3, result.Length);
        Assert.AreEqual("apple", result[0]);
        Assert.AreEqual("banana", result[1]);
        Assert.AreEqual("cherry", result[2]);
    }

    [TestMethod]
    public void SplitMultiCharacterSeparatorTest()
    {
        // Arrange
        string input = "apple::banana::cherry";
        string separator = "::";

        // Act
        string[] result = input.Split(separator);

        // Assert
        Assert.AreEqual(3, result.Length);
        Assert.AreEqual("apple", result[0]);
        Assert.AreEqual("banana", result[1]);
        Assert.AreEqual("cherry", result[2]);
    }

    [TestMethod]
    public void SplitNoSeparatorTest()
    {
        // Arrange
        string input = "apple";
        string separator = ",";

        // Act
        string[] result = input.Split(separator);

        // Assert
        Assert.AreEqual(1, result.Length);
        Assert.AreEqual("apple", result[0]);
    }

    [TestMethod]
    public void SplitEmptyStringTest()
    {
        // Arrange
        string input = "";
        string separator = ",";

        // Act
        string[] result = input.Split(separator);

        // Assert
        Assert.AreEqual(1, result.Length);
        Assert.AreEqual("", result[0]);
    }

    [TestMethod]
    public void SplitWithEmptyElementsTest()
    {
        // Arrange
        string input = "apple,,cherry";
        string separator = ",";

        // Act
        string[] result = input.Split(separator);

        // Assert
        Assert.AreEqual(3, result.Length);
        Assert.AreEqual("apple", result[0]);
        Assert.AreEqual("", result[1]);
        Assert.AreEqual("cherry", result[2]);
    }

    [TestMethod]
    public void SplitWithLeadingAndTrailingSeparatorTest()
    {
        // Arrange
        string input = ",apple,banana,";
        string separator = ",";

        // Act
        string[] result = input.Split(separator);

        // Assert
        Assert.AreEqual(4, result.Length);
        Assert.AreEqual("", result[0]);
        Assert.AreEqual("apple", result[1]);
        Assert.AreEqual("banana", result[2]);
        Assert.AreEqual("", result[3]);
    }
}