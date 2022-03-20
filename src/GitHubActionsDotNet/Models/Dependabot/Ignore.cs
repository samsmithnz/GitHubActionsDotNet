namespace GitHubActionsDotNet.Models.Dependabot
{
    public class Ignore
    {
        public string dependency_name { get; set; }
        public string[] versions { get; set; }
        public string[] update_types { get; set; }
    }
}
