using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public static class GitVersionStepHelper
    {
        //- name: Setup GitVersion
        //  uses: gittools/actions/gitversion/setup@v0.10.2
        //  with:
        //    versionSpec: 6.x
        public static Step AddGitVersionSetupStep(string name = null,
            string versionSpec = "6.x",
            string _if = null,
            Dictionary<string, string> env = null)
        {
            if (name == null)
            {
                name = "Setup GitVersion";
            }
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "gittools/actions/gitversion/setup@v4.0.1";
            step.with = new Dictionary<string, string>()
            {
                { "versionSpec", versionSpec }
            };
            return step;
        }

        //-name: Determine Version
        // uses: gittools/actions/gitversion/execute@v0.10.2
        // id: gitversion 
        public static Step AddGitVersionDetermineVersionStep(string name = null,
            string id = "gitversion",
            string _if = null,
            Dictionary<string, string> env = null)
        {
            if (name == null)
            {
                name = "Determine Version";
            }
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "gittools/actions/gitversion/execute@v4.0.1";
            step.id = id;
            return step;
        }

    }
}
