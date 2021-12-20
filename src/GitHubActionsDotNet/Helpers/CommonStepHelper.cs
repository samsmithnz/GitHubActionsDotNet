using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Helpers
{
    public static class CommonStepHelper
    {
        public static Step AddScriptStep(string name = null,
             string runStep = null,
             string shell = null
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
                run = sb.ToString(),
                shell = shell
            };
            return step;
        }

        public static Step AddCheckoutStep(string name = null,
            string repository = null,
            string fetchDepth = null)
        {
            Dictionary<string, string> with = null;
            if (repository != null || repository != null)
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
                with = with
            };
            return step;

        }

    }
}
