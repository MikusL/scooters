using System;

namespace Exceptions
{
    [Serializable]
    public class EmptyList : Exception
    {
        public EmptyList()
        { }

        public EmptyList(string message)
            : base(message)
        { }

        public EmptyList(string message, Exception innerException)
            : base(message, innerException)
        { }


    }
}
