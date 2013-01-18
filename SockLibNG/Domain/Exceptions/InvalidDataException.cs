using System;

namespace SockLibNG.Domain.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
}
