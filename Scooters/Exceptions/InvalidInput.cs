using System;

namespace Exceptions
{
    [Serializable]
    public class InvalidInput : Exception
    {
        public InvalidInput()
        { }

        public InvalidInput(string message)
            : base(message)
        { }

        public InvalidInput(string message, Exception innerException)
            : base(message, innerException)
        { }


    }
}
