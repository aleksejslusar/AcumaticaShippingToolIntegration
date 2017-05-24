using System;
using System.Windows.Forms;
using SmartShipment.Information.InformationTypes;
using SmartShipment.Information.Logger;
using SmartShipment.Information.Properties;

namespace SmartShipment.Information
{
    public class SmartShipmentMessagesProvider : ISmartShipmentMessagesProvider
    {
        private readonly ILogger _logger;

        public SmartShipmentMessagesProvider(ILogger logger)
        {
            _logger = logger;
        }

        public void Error(Exception exception)
        {
            new Error(_logger, exception).HandleInfo();
        }

        public void Warn(string message)
        {
            new Warning(_logger, message).HandleInfo();            
        }

        public void Info(string message)
        {
            new Info(_logger, message).HandleInfo();
        }

        public void Log(string message)
        {
            new Logging(_logger, message).HandleInfo();
        }

        public void Fatal(Exception exception)
        {
            new Fatal(_logger, exception).HandleInfo();
        }

        public DialogResult Message(string message)
        {
            return MessageBox.Show(message, 
                                   InformationResources.APPLICATION_NAME, 
                                   MessageBoxButtons.YesNo, 
                                   MessageBoxIcon.Question, 
                                   MessageBoxDefaultButton.Button2, 
                                   MessageBoxOptions.ServiceNotification|MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
