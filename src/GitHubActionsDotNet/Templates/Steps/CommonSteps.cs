using GitHubActionsDotNet.Models;
using System.Text;

namespace GitHubActionsDotNet.Templates.Steps
{
    public static class CommonSteps
    {
        public static Step CreateScriptStep(string name = null,
             string runStep = null
             )
        {
            StringBuilder sb = new StringBuilder();
            if (runStep != null)
            {
                sb.Append(runStep);
            }

            Step step = new Step
            {
                name = name,
                run = sb.ToString()
            };

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
