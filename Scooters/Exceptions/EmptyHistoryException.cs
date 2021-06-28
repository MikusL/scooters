using System;

namespace Exceptions
{
    [Serializable]
    public class EmptyHistoryException : Exception
    {
        public EmptyHistoryException()
        { }

        public EmptyHistoryException(string message)
            : base(message)
        { }

        public EmptyHistoryException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
