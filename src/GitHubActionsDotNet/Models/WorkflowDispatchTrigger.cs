using System.Collections.Generic;

namespace GitHubActionsDotNet.Models
{
    public class WorkflowDispatchTrigger
    {
        //  workflow_dispatch:
        //    inputs:
        //      environment:
        //        description: 'Select the environment'
        //        required: true
        //        default: 'staging'
        //        type: choice
        //        options:
        //          - staging
        //          - production

        public Dictionary<string, WorkflowDispatchInput> inputs { get; set; }
    }
}
