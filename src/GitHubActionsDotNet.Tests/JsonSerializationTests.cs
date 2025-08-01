using GitHubActionsDotNet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class JsonSerializationTests
{
    [TestMethod]
    public void DeserializeStringToJsonElementTest()
    {
        // Arrange
        string yaml = @"name: test
value: 123
nested:
  key: value";

        // Act
        JsonElement result = JsonSerialization.DeserializeStringToJsonElement(yaml);

        // Assert
        Assert.IsTrue(result.TryGetProperty("name", out JsonElement nameElement));
        Assert.AreEqual("test", nameElement.GetString());
        Assert.IsTrue(result.TryGetProperty("value", out JsonElement valueElement));
        Assert.AreEqual("123", valueElement.GetString());
        Assert.IsTrue(result.TryGetProperty("nested", out JsonElement nestedElement));
        Assert.IsTrue(nestedElement.TryGetProperty("key", out JsonElement keyElement));
        Assert.AreEqual("value", keyElement.GetString());
    }

    [TestMethod]
    public void DeserializeStringToJsonElementSimpleTest()
    {
        // Arrange
        string yaml = "name: simple test";

        // Act
        JsonElement result = JsonSerialization.DeserializeStringToJsonElement(yaml);

        // Assert
        Assert.IsTrue(result.TryGetProperty("name", out JsonElement nameElement));
        Assert.AreEqual("simple test", nameElement.GetString());
    }

    [TestMethod]
    public void JsonCompareIdenticalObjectsTest()
    {
        // Arrange
        var obj1 = new { name = "test", value = 123 };
        var obj2 = new { name = "test", value = 123 };

        // Act
        bool result = obj1.JsonCompare(obj2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void JsonCompareDifferentObjectsTest()
    {
        // Arrange
        var obj1 = new { name = "test", value = 123 };
        var obj2 = new { name = "test", value = 456 };

        // Act
        bool result = obj1.JsonCompare(obj2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void JsonCompareNullObjectsTest()
    {
        // Arrange
        object obj1 = null;
        object obj2 = null;

        // Act
        bool result = obj1.JsonCompare(obj2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void JsonCompareOneNullObjectTest()
    {
        // Arrange
        var obj1 = new { name = "test" };
        object obj2 = null;

        // Act
        bool result = obj1.JsonCompare(obj2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void JsonCompareComplexObjectsTest()
    {
        // Arrange
        var obj1 = new 
        { 
            name = "test", 
            nested = new { key = "value", count = 5 },
            array = new[] { 1, 2, 3 }
        };
        var obj2 = new 
        { 
            name = "test", 
            nested = new { key = "value", count = 5 },
            array = new[] { 1, 2, 3 }
        };

        // Act
        bool result = obj1.JsonCompare(obj2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void JsonCompareDifferentComplexObjectsTest()
    {
        // Arrange
        var obj1 = new 
        { 
            name = "test", 
            nested = new { key = "value", count = 5 }
        };
        var obj2 = new 
        { 
            name = "test", 
            nested = new { key = "different", count = 5 }
        };

        // Act
        bool result = obj1.JsonCompare(obj2);

        // Assert
        Assert.IsFalse(result);
    }
}