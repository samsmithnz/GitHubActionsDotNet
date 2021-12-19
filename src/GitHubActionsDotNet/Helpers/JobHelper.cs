using GitHubActionsDotNet.Models;

namespace GitHubActionsDotNet.Helpers
{
    public static class JobHelper
    {
        public static Job AddJob(
            string displayName = null,
            string runs_on = null,
            int timeout_minutes = 0,
            Step[] steps = null)
        {
            Job job = new Job
            {
                name = displayName,
                runs_on = runs_on,
                timeout_minutes = timeout_minutes,
                steps = steps
            };
            return job;
        }
    }
}
