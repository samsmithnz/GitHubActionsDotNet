using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Helpers
{
    public static class GitVersionStepHelper
    {
        public static Step AddGitVersionSetupStep(string name = null,
            string versionSpec = "5.x",
            string _if = null)
        {
            //- name: Setup GitVersion
            //  uses: gittools/actions/gitversion/setup@v0.9.11
            //  with:
            //    versionSpec: 5.x

            Step step = new Step
            {
                name = "Setup GitVersion",
                uses = "gittools/actions/gitversion/setup@v0.9.11",
                with = new Dictionary<string, string>(),
                _if = _if
            };
            step.with.Add("versionSpec", versionSpec);

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }
        public static Step AddGitVersionDetermineVersionStep(string name = null,
            string id = "gitversion",
            string _if = null)
        {
            //-name: Determine Version
            // uses: gittools/actions/gitversion/execute@v0.9.11
            // id: gitversion 

            Step step = new Step
            {
                name = "Determine Version",
                uses = "gittools/actions/gitversion/execute@v0.9.11",
                id = id,
                _if = _if
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

  
    }
}
