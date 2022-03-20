namespace GitHubActionsDotNet.Models.Dependabot
{
    public class Registry
    {
        public string type { get; set; }
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string key { get; set; }
        public string token { get; set; }
        public string replaces_base { get; set; }
        public string organization { get; set; }
    }
}
