using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Helpers
{
    public static class CommonStepHelper
    {
        public static Step AddScriptStep(string name = null,
            string runStep = null,
            string shell = null,
            string _if = null)
        {
            StringBuilder sb = new StringBuilder();
            if (runStep != null)
            {
                sb.Append(runStep);
            }

            Step step = new Step
            {
                name = name,
                run = sb.ToString(),
                shell = shell,
                _if = _if
            };
            return step;
        }

        public static Step AddCheckoutStep(string name = null,
            string repository = null,
            string fetchDepth = null,
            string _if = null)
        {
            //- uses: actions/checkout@v2
            //  with:
            //    fetch-depth: 0

            Dictionary<string, string> with = null;
            if (repository != null || fetchDepth != null)
            {
                with = new Dictionary<string, string>();
            }
            if (repository != null)
            {
                with.Add("repository", repository);
            }
            if (fetchDepth != null)
            {
                with.Add("fetch-depth", fetchDepth);
            }

            Step step = new Step
            {
                name = name,
                uses = "actions/checkout@v2",
                with = with,
                _if = _if
            };
            return step;
        }

        //- name: Upload nuget package back to GitHub
        //  uses: actions/upload-artifact@v2
        //  if: runner.OS == 'Linux' #Only pack the Linux nuget package
        //  with:
        //    name: nugetPackage
        //    path: src/GitHubActionsDotNet/bin/Release

        public static Step AddUploadArtifactStep(string name = null,
            string packageName = null,
            string packagePath = null,
            string _if = null)
        {
            Step step = new Step
            {
                name = name,
                uses = "actions/upload-artifact@v2",
                with = new Dictionary<string, string>()
                {
                    { "name", packageName },
                    { "path", packagePath }
                },
                _if = _if
            };
            return step;
        }

    }
}
