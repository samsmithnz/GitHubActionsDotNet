using GitHubActionsDotNet.Models;
using System.Text;

namespace GitHubActionsDotNet.Templates.Steps
{
    public static class CommonSteps
    {
        public static Step CreateScriptStep(string name = null,
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

        public static Step CheckoutStep(string name = null)
        {
            Step step = new Step
            {
                name = name,
                uses = "actions/checkout@v2"
            };
            return step;

        }

    }
}
