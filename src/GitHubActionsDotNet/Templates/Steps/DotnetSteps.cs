﻿using GitHubActionsDotNet.Models;
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
            string otherArguments = null,
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
            if (otherArguments != null)
            {
                sb.Append(otherArguments);
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
            string otherArguments = null 
            //bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet restore ");
            if (project != null)
            {
                sb.Append(project);
                sb.Append(" ");
            }
            if (otherArguments != null)
            {
                sb.Append(otherArguments);
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
            string otherArguments = null,
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
            if (otherArguments != null)
            {
                sb.Append(otherArguments);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = "Push NuGet package",
                run = sb.ToString()
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step CreateDotNetPackStep(string name = null,
            string project = null,
            string otherArguments = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet pack ");
            if (project != null)
            {
                sb.Append(project);
                sb.Append(" ");
            }
            if (otherArguments != null)
            {
                sb.Append(otherArguments);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = ".NET NuGet pack",
                run = sb.ToString()
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step CreateDotNetPublishStep(string name = null,
            string project = null,
            string configuration = null,
            string output = null,
            string otherArguments = null,
            bool useShortParameters = false //Included for inclusivity reasons
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet publish ");
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
            if (output != null)
            {
                if (useShortParameters == true)
                {
                    sb.Append("-o ");
                }
                else
                {
                    sb.Append("--output ");
                }
                sb.Append(output);
                sb.Append(" ");
            }
            if (otherArguments != null)
            {
                sb.Append(otherArguments);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = ".NET publish",
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