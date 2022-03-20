using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public class Package2<T> : IPackage2
    {
        public string name { get; set; }

        private string registryString;
        private string[] registryArray;
        public object registries
        {
            get
            {
                if (typeof(T) == typeof(string))
                {
                    return registryString;
                }
                else if (typeof(T) == typeof(string[]))
                {
                    return registryArray;
                }
                else
                {
                    return default(T);
                }
            }
            set
            {
                if (typeof(T) == typeof(string))
                {
                    registryString = value.ToString();
                }
                else if (typeof(T) == typeof(string[]))
                {
                    registryArray = (string[])value;
                }
            }
        }
        Type IPackage2.Type
        {
            get
            {
                return typeof(T);
            }
        }
    }

    public interface IPackage2
    {
        string name { get; set; }
        object registries { get; set; }
        Type Type { get; }
    }
}
