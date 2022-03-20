using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public class Root2
    {
        public string name { get; set; }
        public List<IPackage2> packages { get; set; }
    }
}
