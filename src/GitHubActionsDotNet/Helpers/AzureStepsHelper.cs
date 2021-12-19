using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public static class AzureStepsHelper
    {
        public static Step AddAzureFunctionDeployStep(string name = null,
            string appName = null,
            string package = null)
        {
            Step step = new Step
            {
                name = "Deploy to Azure Function App",
                uses = "Azure/functions-action@v1",
                with = new Dictionary<string, string>()
                {
                    {"app-name" ,appName},
                    {"publish-profile" ,"${{ secrets.{PUBLISH_PROFILE} }}"},
                    {"package" ,package},
                }
            };

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }
    }
}
