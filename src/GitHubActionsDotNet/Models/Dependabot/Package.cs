using System;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class PackageString : IPackage
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
        //This registries property will be a string 
        private string _registries;
        public dynamic registries
        {
            get
            {
                return _registries;
            }
            set
            {
                _registries = value;
            }
        }
        public string[] labels { get; set; }
        public int? milestone { get; set; }
        public PullRequestBranchName pull_request_branch_name { get; set; }
        public string rebase_strategy { get; set; }
        public string[] reviewers { get; set; }
        public string target_branch { get; set; }
        public bool? vendor { get; set; }
        public string versioning_strategy { get; set; }
    }
    public class PackageStringArray : IPackage
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
        //This registries property will be a string
        private string[] _registries;
        public dynamic registries
        {
            get
            {
                return _registries;
            }
            set
            {
                //if (value != null && value.GetType().ToString() == "System.Collections.Generic.List`1[System.Object]")
                //{
                if (value != null)
                {
                    _registries = Array.ConvertAll(((List<object>)value).ToArray(), o => (string)o);
                }
                else
                {
                    _registries = null;
                }
                //}
                //else
                //{
                //    _registries = value;
                //}
            }
        }
        public string[] labels { get; set; }
        public int? milestone { get; set; }
        public PullRequestBranchName pull_request_branch_name { get; set; }
        public string rebase_strategy { get; set; }
        public string[] reviewers { get; set; }
        public string target_branch { get; set; }
        public bool? vendor { get; set; }
        public string versioning_strategy { get; set; }
    }

    public interface IPackage
    {
        string package_ecosystem { get; set; }
        string directory { get; set; }
        Schedule schedule { get; set; }
        List<string> assignees { get; set; }
        string open_pull_requests_limit { get; set; }
        Allow[] allow { get; set; }
        CommitMessage commit_message { get; set; }
        Ignore[] ignore { get; set; }
        string insecure_external_code_execution { get; set; }
        //registries can be string or string[]
        dynamic registries { get; set; }
        string[] labels { get; set; }
        int? milestone { get; set; }
        PullRequestBranchName pull_request_branch_name { get; set; }
        string rebase_strategy { get; set; }
        string[] reviewers { get; set; }
        string target_branch { get; set; }
        bool? vendor { get; set; }
        string versioning_strategy { get; set; }
    }
}
