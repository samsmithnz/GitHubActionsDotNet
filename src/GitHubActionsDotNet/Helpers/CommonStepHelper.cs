using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public static class CommonStepHelper
    {
        public static Step AddScriptStep(string name = null,
            string runScript = null,
            string shell = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.run = runScript;
            step.shell = shell;
            return step;
        }

        //- uses: actions/checkout@v2
        //  with:
        //    fetch-depth: 0
        public static Step AddCheckoutStep(string name = null,
            string repository = null,
            string fetchDepth = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "actions/checkout@v2";
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
            step.with = with;
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
            string _if = null,
            Dictionary<string, string> env = null)
        {
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "actions/upload-artifact@v2";
            step.with = new Dictionary<string, string>()
            {
                { "name", packageName },
                { "path", packagePath }
            };
            return step;
        }

        //- name: Download nuget package artifact
        //  uses: actions/download-artifact@v2.1.0
        //  with:
        //    name: nugetPackage
        //    path: nugetPackage
        public static Step AddDownloadArtifactStep(string name = null,
            string packageName = null,
            string packagePath = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "actions/download-artifact@v2.1.0";
            step.with = new Dictionary<string, string>()
            {
                { "name", packageName },
                { "path", packagePath }
            };
            return step;
        }

    }
}
