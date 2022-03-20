using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class Package
    {
        public string package_ecosystem { get; set; }
        public string directory { get; set; }
        public Schedule schedule { get; set; }
        public List<string> assignees { get; set; }
        public string open_pull_requests_limit { get; set; }
        public Allow[] allow { get; set; }
        public CommitMessage commit_message { get; set; }
        public Ignore[] ignore { get; set; }
        public string insecure_external_code_execution { get; set; }
        public string registries { get; set; }
        //public string[] registries { get; set; }
        public string[] labels { get; set; }
        public int? milestone { get; set; }
        public PullRequestBranchName pull_request_branch_name { get; set; }
        public string rebase_strategy { get; set; }
        public string[] reviewers { get; set; }
        public string target_branch {get;set;}
        public bool? vendor { get; set; }
        public string versioning_strategy { get; set; }
    }
}
