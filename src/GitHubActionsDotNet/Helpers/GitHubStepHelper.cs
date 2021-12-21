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
            Step step = new Step
            {
                name = "Create Release",
                uses = "actions/create-release@v1",
                with = new Dictionary<string, string>(),
                _if = _if,
                env = env
            };
            step.env.Add("GITHUB_TOKEN", "${{ secrets.GITHUB_TOKEN }}");
            step.with.Add("tag_name", tagName);
            step.with.Add("release_name", releaseName);

            if (name != null)
            {
                step.name = name;
            }
            return step;
        }

    }
}
