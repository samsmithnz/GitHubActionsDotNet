using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public static class AzureStepHelper
    {
        public static Step AddAzureFunctionDeployStep(string name = null,
            string appName = null,
            string package = null,
            string publishProfileName = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            if (name == null)
            {
                name = "Deploy to Azure Function App";
            }
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "Azure/functions-action@v1";
            step.with = new Dictionary<string, string>()
            {
                {"app-name", appName},
                {"publish-profile", publishProfileName},
                {"package", package}
            };
            return step;
        }

        public static Step AddAzureWebAppDeployStep(string name = null,
            string appName = null,
            string package = null,
            string publishProfileName = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            if (name == null)
            {
                name = "Deploy to Azure Web App";
            }
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "Azure/webapps-deploy@v2";
            step.with = new Dictionary<string, string>()
            {
                {"app-name", appName},
                {"publish-profile", publishProfileName},
                {"package", package}
            };
            return step;
        }
    }
}
