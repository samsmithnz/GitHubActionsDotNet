namespace GitHubActionsDotNet.Models
{
    public class WorkflowDispatchInput
    {
        //  inputs:
        //    environment:
        //      description: 'Select the environment'
        //      required: true
        //      default: 'staging'
        //      type: choice
        //      options:
        //        - staging
        //        - production
        //    version:
        //      description: 'Version to deploy'
        //      required: true
        //      type: string

        public string description { get; set; }
        public bool? required { get; set; }
        public string _default { get; set; }
        public string type { get; set; }
        public string[] options { get; set; }
    }
}
