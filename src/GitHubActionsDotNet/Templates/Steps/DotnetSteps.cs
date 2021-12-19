using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Templates.Steps
{
    public static class DotNetSteps
    {
        public static Step CreateDotNetUseStep(string name = null)
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

        public static Step CreateDotNetBuildStep(string name = null,
            string project = null,
            string configuration = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet build ");
            if (project != null)
            {
                sb.Append(project);
                sb.Append(" ");
            }
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

        public static Step CreateDotNetRestoreStep(string name = null,
            string project = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet restore ");
            if (project != null)
            {
                sb.Append(project);
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

        public static Step CreateDotNetNuGetPushStep(string name = null,
            string nupkgFile = null,
            string source = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet nuget push ");
            if (nupkgFile != null)
            {
                sb.Append(nupkgFile);
                sb.Append(" ");
            }
            if (source != null)
            {
                if (useShortParameters == true)
                {
                    sb.Append("-s ");
                }
                else
                {
                    sb.Append("--source ");
                }
                sb.Append(source);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = "Push NuGet package to GitHub Packages",
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
