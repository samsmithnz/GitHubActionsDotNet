using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public static class GitHubStepHelper
    {
        //- name: Create Release
        //  uses: actions/create-release@v1
        //  if: needs.build.outputs.CommitsSinceVersionSource > 0
        //  env:
        //    GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        //  with:
        //    tag_name: ${{ needs.build.outputs.Version }}
        //    release_name: Release ${{ needs.build.outputs.Version }}
        public static Step AddCreateReleaseStep(string name = null,
            string tagName = null,
            string releaseName = null,
            string _if = null,
            Dictionary<string, string> env = null)
        {
            if (name == null)
            {
                name = "Create Release";
            }
            Step step = BaseStep.AddBaseStep(name, _if, env);
            step.uses = "actions/create-release@v1";
            if (step.env == null)
            {
                step.env = new Dictionary<string, string>();
            }
            step.env.Add("GITHUB_TOKEN", "${{ secrets.GITHUB_TOKEN }}");
            step.with = new Dictionary<string, string>
            {
                { "tag_name", tagName },
                { "release_name", releaseName }
            };
            return step;
        }

    }
}
