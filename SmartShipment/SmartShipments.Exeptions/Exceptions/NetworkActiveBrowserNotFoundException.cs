namespace SmartShipment.Information.Exceptions
{
    public class NetworkActiveBrowserNotFoundException : NetworkException
    {
        public NetworkActiveBrowserNotFoundException(string message) : base(message) { }
    }
}