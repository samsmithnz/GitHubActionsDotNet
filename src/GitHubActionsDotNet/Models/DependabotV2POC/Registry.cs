namespace GitHubActionsDotNet.Models.DependabotV2POC
{
    public class Registry<T>
    {
        private T _value;

        public virtual T Value
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

        //public static implicit operator T(Registry<T> value)
        //{
        //    return value.Value;
        //}

        //public static implicit operator Registry<T>(T value)
        //{
        //    return new Registry<T> { Value = value };
        //}
    }

    public class RegistryString : Registry<string>
    {
        private string _value;
        public override string Value
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
    }

    public class RegistryStringArray : Registry<string[]>
    {
        private string[] _value;
        public override string[] Value
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
    }
}


