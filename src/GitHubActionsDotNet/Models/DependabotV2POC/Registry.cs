using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public class Registry<T>
    {
        private T _value;

        public T Value
        {
            get
            {
                // insert desired logic here
                return _value;
            }
            set
            {
                // insert desired logic here
                _value = value;
            }
        }

        public static implicit operator T(Registry<T> value)
        {
            return value.Value;
        }

        public static implicit operator Registry<T>(T value)
        {
            return new Registry<T> { Value = value };
        }
    }
}


