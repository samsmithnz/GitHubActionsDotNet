using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public class JobHelper
    {
        /// <summary>
        /// Add a job
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="runs_on"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public Job AddJob(string displayName,
            string runs_on,
            Step[] steps)
        {
            return AddJob(displayName,
                runs_on,
                steps,
                null,
                0,
                null,
                null,
                null);
        }

        /// <summary>
        /// Add a job 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="runs_on"></param>
        /// <param name="steps"></param>
        /// <param name="needs"></param>
        /// <returns></returns>
        public Job AddJob(string displayName,
        string runs_on,
        Step[] steps,
        string[] needs)
        {
            return AddJob(displayName,
                runs_on,
                steps,
                needs,
                0,
                null,
                null,
                null);
        }

        /// <summary>
        /// Add a job
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="runs_on"></param>
        /// <param name="steps"></param>
        /// <param name="needs"></param>
        /// <param name="_if"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public Job AddJob(string displayName,
            string runs_on,
            Step[] steps,
            string[] needs,
            string _if,
            Dictionary<string, string> env)
        {
            return AddJob(displayName,
                runs_on,
                steps,
                needs,
                0,
                null,
                _if,
                env);
        }

        /// <summary>
        /// Add a job
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="runs_on"></param>
        /// <param name="steps"></param>
        /// <param name="needs"></param>
        /// <param name="timeout_minutes"></param>
        /// <param name="environment"></param>
        /// <param name="_if"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public Job AddJob(
            string displayName = null,
            string runs_on = null,
            Step[] steps = null,
            string[] needs = null,
            int timeout_minutes = 0,
            Environment environment = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            Job job = new Job
            {
                name = displayName,
                runs_on = runs_on,
                needs = needs,
                timeout_minutes = timeout_minutes,
                environment = environment,
                steps = steps,
                _if = _if,
                env = env
            };
            return job;
        }
    }
}