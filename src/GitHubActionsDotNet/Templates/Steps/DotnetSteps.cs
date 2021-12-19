using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Templates.Steps
{
    public static class DotnetSteps
    {
        public static Step CreateDotnetUseStep(string name = null)
        {
            Step step = new Step
            {
                name = "Use .NET sdk",
                uses = "actions/setup-dotnet@v1",
                with = new Dictionary<string, string>()
            };
            step.with.Add("dotnet-version", "6.x");

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step CreateDotnetBuildStep(string name = null,
            string configuration = null,
            string projectPath = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet build ");
            if (configuration != null)
            {
                if (useShortParameters == true)
                {
                    sb.Append("-c ");
                }
                else
                {

                    sb.Append("--configuration ");
                }
                sb.Append(configuration);
                sb.Append(" ");
            }
            if (projectPath != null)
            {
                sb.Append(projectPath);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = ".NET build",
                run = sb.ToString()
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step CreateDotnetRestoreStep(string name = null,
            string projectPath = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet restore ");
            if (projectPath != null)
            {
                sb.Append(projectPath);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = ".NET restore",
                run = sb.ToString()
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }
    }
}
