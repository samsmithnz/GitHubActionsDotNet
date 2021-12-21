using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    //Basic step properties that all steps can have
    public static class BaseStep
    {
        public static Step AddBaseStep(
            string name = null,
            string _if = null,
            Dictionary<string,string> env = null)
        {
            Step step = new Step
            {
                name = name,
                 _if = _if,
                env = env
            };

            return step;
        }
    }
}
