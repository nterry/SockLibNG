using System;

namespace SockLibNG.Domain.Exceptions
{
    class DataException : Exception
    {
        public DataException(string message) : base(message) { }
    }
}
