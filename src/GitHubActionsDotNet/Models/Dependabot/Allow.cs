using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class Allow
    {
        public string dependency_name { get; set; }
        public string dependency_type { get; set; }
    }
}
