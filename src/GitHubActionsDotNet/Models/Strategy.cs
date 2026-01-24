using System.Collections.Generic;

namespace GitHubActionsDotNet.Models
{
    public class Strategy
    {
        //strategy:
        //  matrix:
        //    node: [6, 8, 10]
        //steps:
        //  - uses: actions/setup-node@v1
        //    with:
        //      node-version: ${{ matrix.node }}

        //runs-on: ${{ matrix.os }}
        //strategy:
        //  matrix:
        //    os: [ubuntu-16.04, ubuntu-18.04]
        //    node: [6, 8, 10]
        //    exclude:
        //      - os: windows-latest
        //        node: 16
        //steps:
        //  - uses: actions/setup-node@v1
        //    with:
        //      node-version: ${{ matrix.node }}

        public Dictionary<string, string[]> matrix { get; set; }
        public Dictionary<string, string>[] exclude { get; set; } //https://docs.github.com/en/actions/how-tos/write-workflows/choose-what-workflows-do/run-job-variations#excluding-matrix-configurations
        public string max_parallel { get; set; }
    }
}
