using GitHubActionsDotNet.Models;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Helpers
{
    public static class GitHubStepHelper
    {
        //- name: Create Release
        //  uses: ncipollo/release-action@v1
        //  if: needs.build.outputs.CommitsSinceVersionSource > 0
        //  with:
        //    tag: "v${{ needs.build.outputs.Version }}"
        //    name: "v${{ needs.build.outputs.Version }}"
        //    token: ${{ secrets.GITHUB_TOKEN }}
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
            step.uses = "ncipollo/release-action@v1";
            step.with = new Dictionary<string, string>
            {
                { "tag_name", tagName },
                { "release_name", releaseName },
                { "token", "${{ secrets.GITHUB_TOKEN }}" }
            };
            return step;
        }

    }
}
