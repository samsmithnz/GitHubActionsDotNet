using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Helpers
{
    public static class DotNetStepHelper
    {
        public static Step AddDotNetSetupStep(string name = null,
            string dotnetVersion = "6.x")
        {
            Step step = new Step
            {
                name = "Use .NET sdk",
                uses = "actions/setup-dotnet@v1",
                with = new Dictionary<string, string>()
            };
            step.with.Add("dotnet-version", dotnetVersion);

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step AddDotNetRestoreStep(string name = null,
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

        public static Step AddDotNetBuildStep(string name = null,
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
                sb.Append(ProcessAlternativeParameters(configuration, "c", "configuration", useShortParameters));
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

        public static Step AddDotNetTestStep(string name = null,
            string project = null,
            string configuration = null,
            string otherArguments = null,
            bool useShortParameters = false //Included for inclusivity reasons)
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dotnet test ");
            if (project != null)
            {
                sb.Append(project);
                sb.Append(" ");
            }
            if (configuration != null)
            {
                sb.Append(ProcessAlternativeParameters(configuration, "c", "configuration", useShortParameters));
            }
            if (otherArguments != null)
            {
                sb.Append(otherArguments);
                sb.Append(" ");
            }

            Step step = new Step
            {
                name = ".NET test",
                run = sb.ToString()
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step AddDotNetNuGetPushStep(string name = null,
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
                sb.Append(ProcessAlternativeParameters(source, "s", "source", useShortParameters));
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

        public static Step AddDotNetPackStep(string name = null,
            string project = null,
            string configuration = null,
            string output = null,
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
            if (configuration != null)
            {
                sb.Append(ProcessAlternativeParameters(configuration, "c", "configuration", useShortParameters));
            }
            if (output != null)
            {
                sb.Append(ProcessAlternativeParameters(output, "o", "output", useShortParameters));
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

        public static Step AddDotNetPublishStep(string name = null,
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
                sb.Append(ProcessAlternativeParameters(configuration, "c", "configuration", useShortParameters));
            }
            if (output != null)
            {
                sb.Append(ProcessAlternativeParameters(output, "o", "output", useShortParameters));
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

        private static string ProcessAlternativeParameters(string commandText,
            string shortCommand,
            string longCommand,
            bool useShortParameters)
        {
            //Generates either -c Release or --command Release
            StringBuilder sb = new StringBuilder();
            if (commandText != null)
            {
                if (useShortParameters == true)
                {
                    //e.g. -c
                    sb.Append("-");
                    sb.Append(shortCommand);
                    sb.Append(" ");
                }
                else
                {
                    //e.g. --command
                    sb.Append("--");
                    sb.Append(longCommand);
                    sb.Append(" ");
                }
                //e.g. Release
                sb.Append(commandText);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
