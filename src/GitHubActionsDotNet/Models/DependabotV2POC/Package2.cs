using System;
using System.Collections.Generic;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    //public class Package2<T> : IPackage2
    //{
    //    public string name { get; set; }

    //    //private string registryString;
    //    //private string[] registryArray;
    //    public virtual dynamic registries { get; set; }
    //    //{
    //    //    get
    //    //    {
    //    //        if (typeof(T) == typeof(string))
    //    //        {
    //    //            return registryString;
    //    //        }
    //    //        else if (typeof(T) == typeof(string[]))
    //    //        {
    //    //            return registryArray;
    //    //        }
    //    //        else
    //    //        {
    //    //            return default(T);
    //    //        }
    //    //    }
    //    //    set
    //    //    {
    //    //        if (typeof(T) == typeof(string))
    //    //        {
    //    //            registryString = value.ToString();
    //    //        }
    //    //        else if (typeof(T) == typeof(string[]))
    //    //        {
    //    //            registryArray = (string[])value;
    //    //        }
    //    //    }
    //    //}
    //    //Type IPackage2.Type
    //    //{
    //    //    get
    //    //    {
    //    //        return typeof(T);
    //    //    }
    //    //}
    //}

    public interface IPackage2
    {
        string name { get; set; }
        dynamic registries { get; set; }
        //Type Type { get; }
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
                if (value.GetType().ToString() == "System.Collections.Generic.List`1[System.Object]")//typeof(List<object>).GetType().ToString())
                {
                    _result = Array.ConvertAll(((List<object>)value).ToArray(), o => (string)o);
                    //_result = (string[])((List<object>)value).ToArray();
                }
                else
                {
                    _result = value;
                }
            }
        }
    }

}
