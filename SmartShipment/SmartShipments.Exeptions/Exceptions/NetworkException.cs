using System;

namespace SmartShipment.Information.Exceptions
{
    public class NetworkException : Exception
    {
        public NetworkException(string message): base(message) { }
    }
}