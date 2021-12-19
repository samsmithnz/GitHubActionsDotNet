using GitHubActionsDotNet.Models;

namespace GitHubActionsDotNet.Helpers
{
    public static class TriggerHelper
    {
        public static Trigger AddStandardTrigger(string defaultBranch = "main")
        {
            Trigger trigger = new Trigger
            {
                push = new TriggerDetail
                {
                    branches = new string[1] { defaultBranch }
                },
                pull_request = new TriggerDetail()
                {
                    branches = new string[1] { defaultBranch }
                }
            };
            return trigger;
        }
    }
}
