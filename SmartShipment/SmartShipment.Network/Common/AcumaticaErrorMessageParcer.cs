using System;
using System.Linq;
using SmartShipment.Information.Properties;

namespace SmartShipment.Network.Common
{
    public class AcumaticaErrorMessageParcer
    {
        public static string GetUserFriendlyMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || (!message.Contains("\n") && !message.Contains(":"))) return message;

            var messageArray = message.Substring(0, message.IndexOf("\n", StringComparison.Ordinal)).Split(':');
            if (messageArray.Any())
            {
                return messageArray.Last();
            }

            return InformationResources.Warn_Acumatica_Internal_Error_Message;
        }
    }
}
