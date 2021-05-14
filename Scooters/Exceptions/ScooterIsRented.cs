using System;

namespace Exceptions
{
    [Serializable]
    public class ScooterIsRented : Exception
    {
        public ScooterIsRented()
        { }

        public ScooterIsRented(string message)
            : base(message)
        { }

        public ScooterIsRented(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
