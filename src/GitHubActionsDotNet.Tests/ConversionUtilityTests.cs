using GitHubActionsDotNet.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ConversionUtilityTests
{
    [TestMethod]
    public void GenerateSpacesTest()
    {
        // Arrange & Act
        string spaces = ConversionUtility.GenerateSpaces(4);

        // Assert
        Assert.AreEqual("    ", spaces);
        Assert.AreEqual(4, spaces.Length);
    }

    [TestMethod]
    public void GenerateSpacesZeroTest()
    {
        // Arrange & Act
        string spaces = ConversionUtility.GenerateSpaces(0);

        // Assert
        Assert.AreEqual("", spaces);
        Assert.AreEqual(0, spaces.Length);
    }

    [TestMethod]
    public void CountSpacesBeforeTextTest()
    {
        // Arrange
        string input = "    some text";

        // Act
        int count = ConversionUtility.CountSpacesBeforeText(input);

        // Assert
        Assert.AreEqual(4, count);
    }

    [TestMethod]
    public void CountSpacesBeforeTextWithNewLineTest()
    {
        // Arrange
        string input = "  some text" + Environment.NewLine;

        // Act
        int count = ConversionUtility.CountSpacesBeforeText(input);

        // Assert
        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public void CountSpacesBeforeTextNoSpacesTest()
    {
        // Arrange
        string input = "some text";

        // Act
        int count = ConversionUtility.CountSpacesBeforeText(input);

        // Assert
        Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void CleanYamlBeforeDeserializationV2ValidYamlTest()
    {
        // Arrange
        string yaml = "name: test\nvalue: 123";

        // Act
        string result = ConversionUtility.CleanYamlBeforeDeserializationV2(yaml);

        // Assert
        Assert.AreEqual(yaml, result);
    }

    [TestMethod]
    public void CleanYamlBeforeDeserializationV2InvalidYamlTest()
    {
        // Arrange
        string yaml = "invalid yaml without colon";

        // Act & Assert
        Assert.ThrowsException<Exception>(() => ConversionUtility.CleanYamlBeforeDeserializationV2(yaml));
    }

    [TestMethod]
    public void CleanYamlBeforeDeserializationV2WithIfStatementsTest()
    {
        // Arrange
        string yaml = @"name: test
{{#if condition}}
  var1: value1
{{/if}}
other: value";

        // Act
        string result = ConversionUtility.CleanYamlBeforeDeserializationV2(yaml);

        // Assert
        Assert.IsFalse(result.Contains("{{#if"));
        Assert.IsFalse(result.Contains("{{/if"));
        Assert.IsTrue(result.Contains("var1: value1"));
    }

    [TestMethod]
    public void CleanYamlBeforeDeserializationV2WithDollarIfStatementsTest()
    {
        // Arrange
        string yaml = @"name: test
${{if condition}}
  var1: value1
{{/if}}
other: value";

        // Act
        string result = ConversionUtility.CleanYamlBeforeDeserializationV2(yaml);

        // Assert
        Assert.IsFalse(result.Contains("${{if"));
        Assert.IsFalse(result.Contains("{{/if"));
        Assert.IsTrue(result.Contains("var1: value1"));
    }

    [TestMethod]
    public void ConvertMessageToYamlCommentTest()
    {
        // Arrange
        string message = "This is a comment";

        // Act
        string result = ConversionUtility.ConvertMessageToYamlComment(message);

        // Assert
        Assert.AreEqual("#This is a comment", result);
    }

    [TestMethod]
    public void ConvertMessageToYamlCommentAlreadyCommentTest()
    {
        // Arrange
        string message = "  # This is already a comment";

        // Act
        string result = ConversionUtility.ConvertMessageToYamlComment(message);

        // Assert
        Assert.AreEqual("  # This is already a comment", result);
    }

    [TestMethod]
    public void StepsPreProcessingWithStepsTest()
    {
        // Arrange
        string input = @"steps:
- name: test
  run: echo hello";

        // Act
        string result = ConversionUtility.StepsPreProcessing(input);

        // Assert
        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void StepsPreProcessingWithoutStepsTest()
    {
        // Arrange
        string input = @"- name: test
  run: echo hello";

        // Act
        string result = ConversionUtility.StepsPreProcessing(input);

        // Assert
        Assert.IsTrue(result.StartsWith("steps:"));
        Assert.IsTrue(result.Contains("- name: test"));
    }

    [TestMethod]
    public void StepsPreProcessingEmptyStringTest()
    {
        // Arrange
        string input = "";

        // Act
        string result = ConversionUtility.StepsPreProcessing(input);

        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void WriteLineVerboseTrueTest()
    {
        // Arrange
        string message = "Test message";

        // Act - This method writes to Debug, so we just ensure it doesn't throw
        ConversionUtility.WriteLine(message, true);

        // Assert - Method completed without exception
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void WriteLineVerboseFalseTest()
    {
        // Arrange
        string message = "Test message";

        // Act - This method should not write anything when verbose is false
        ConversionUtility.WriteLine(message, false);

        // Assert - Method completed without exception
        Assert.IsTrue(true);
    }
}