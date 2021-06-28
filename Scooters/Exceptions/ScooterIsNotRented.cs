using System;

namespace Exceptions
{
    [Serializable]
    public class ScooterIsNotRented : Exception
    {
        public ScooterIsNotRented()
        { }

        public ScooterIsNotRented(string message)
            : base(message)
        { }

        public ScooterIsNotRented(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
