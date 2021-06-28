using System;

namespace Exceptions
{
    [Serializable]
    public class ScooterDoesNotExist : Exception
    {
        public ScooterDoesNotExist()
        { }

        public ScooterDoesNotExist(string message)
            : base(message)
        { }

        public ScooterDoesNotExist(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
