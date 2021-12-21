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
            Step step = new Step
            {
                name = "Deploy to Azure Function App",
                uses = "Azure/functions-action@v1",
                with = new Dictionary<string, string>()
                {
                    {"app-name", appName},
                    {"publish-profile", publishProfileName},
                    {"package", package}
                },
                _if = _if,
                env = env
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

        public static Step AddAzureWebAppDeployStep(string name = null,
            string appName = null,
            string package = null,
            string publishProfileName = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            Step step = new Step
            {
                name = "Deploy to Azure Web App",
                uses = "Azure/webapps-deploy@v2",
                with = new Dictionary<string, string>()
                {
                    {"app-name", appName},
                    {"publish-profile", publishProfileName},
                    {"package", package}
                },
                _if = _if,
                env = env
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }
    }
}
