using GitHubActionsDotNet.Models;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Helpers
{
    public static class CommonStepsHelper
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
            string repository = null)
        {
            Dictionary<string, string> with = null;
            if (repository != null)
            {
                with = new Dictionary<string, string>
                {
                    { "repository", repository }
                };
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
