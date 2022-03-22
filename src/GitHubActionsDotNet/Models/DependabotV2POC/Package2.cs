using System;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    //The base package class implementation
    public class Package2
    {
        public virtual string name { get; set; }
        public virtual dynamic registries { get; set; }
    }

    public class Package2String : Package2
    {
        public override string name { get; set; }
        private string _result;
        public override dynamic registries
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

    public class Package2StringArray : Package2
    {
        public override string name { get; set; }
        private string[] _result;
        public override dynamic registries
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
