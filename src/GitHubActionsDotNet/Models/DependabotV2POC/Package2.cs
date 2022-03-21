using System;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public interface IPackage2
    {
        string name { get; set; }
        dynamic registries { get; set; }
    }

    public class Package2String : IPackage2
    {
        public string name { get; set; }
        private string _result;
        public dynamic registries
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }
    }

    public class Package2StringArray : IPackage2
    {
        public string name { get; set; }
        private string[] _result;
        public dynamic registries
        {
            get
            {
                return _result;
            }
            set
            {
                if (value.GetType().ToString() == "System.Collections.Generic.List`1[System.Object]")
                {
                    _result = Array.ConvertAll(((List<object>)value).ToArray(), o => (string)o);
                }
                else
                {
                    _result = value;
                }
            }
        }
    }

}
