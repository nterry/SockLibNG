using System;

namespace SockLibNG.Domain.Exceptions
{
    public class ConstraintException : Exception
    {
        public ConstraintException(string message) : base(message) { }
    }
}
