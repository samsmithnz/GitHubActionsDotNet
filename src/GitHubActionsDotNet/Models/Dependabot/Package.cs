using System;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.Dependabot
{
    public class PackageString : Package
    {
        //This registries property will be a string 
        private string _registries;
        public new dynamic registries
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
    }

    public class PackageStringArray : Package
    {
        //This registries property will be a string
        private string[] _registries;
        public new dynamic registries
        {
            get
            {
                return _registries;
            }
            set
            {
                if (value != null)
                {
                    //Danger Will Robinson, Danger! 
                    //Need to convert the dynamic List<object> to string[], a bit gross
                    _registries = Array.ConvertAll(((List<object>)value).ToArray(), o => (string)o);
                }
                else
                {
                    _registries = null;
                }
            }
        }

    }

    //The base package class implementation
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
        public dynamic registries { get; set; }
        public string[] labels { get; set; }
        public int? milestone { get; set; }
        public PullRequestBranchName pull_request_branch_name { get; set; }
        public string rebase_strategy { get; set; }
        public string[] reviewers { get; set; }
        public string target_branch { get; set; }
        public bool? vendor { get; set; }
        public string versioning_strategy { get; set; }
    }
}
