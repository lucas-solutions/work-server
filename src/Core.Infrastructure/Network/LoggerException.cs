using System;

namespace Lucas.Solutions.Network
{
    public class LoggerException : Exception
    {
        public LoggerException() { }
        public LoggerException(string message) : base(message) { }
        public LoggerException(string message, Exception innerException) : base(message, innerException) { }
    }
}