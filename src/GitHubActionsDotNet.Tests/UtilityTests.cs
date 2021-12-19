using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubActionsDotNet.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class UtilityTests
{
    public static string TrimNewLines(string input)
    {
        //Trim off any leading or trailing new lines 
        input = input.TrimStart('\r', '\n');
        input = input.TrimEnd('\r', '\n');

        return input;
    }

    public static string DebugNewLineCharacters(string input)
    {
        input = input.Replace("\r", "xxx");
        input = input.Replace("\n", "000");
        return input;
    }

}