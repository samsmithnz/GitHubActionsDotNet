using GitHubActionsDotNet.Models;

namespace GitHubActionsDotNet.Helpers
{
    public static class TriggerHelper
    {
        public static Trigger AddStandardPushTrigger(string defaultBranch = "main")
        {
            Trigger trigger = new Trigger
            {
                push = new TriggerDetail
                {
                    branches = new string[] { defaultBranch }
                }
            };
            return trigger;
        }

        public static Trigger AddStandardPushAndPullTrigger(string defaultBranch = "main")
        {
            Trigger trigger = new Trigger
            {
                push = new TriggerDetail
                {
                    branches = new string[] { defaultBranch }
                },
                pull_request = new TriggerDetail()
                {
                    branches = new string[] { defaultBranch }
                }
            };
            return trigger;
        }
    }
}
