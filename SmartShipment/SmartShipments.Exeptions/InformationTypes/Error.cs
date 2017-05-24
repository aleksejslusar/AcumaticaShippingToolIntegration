using System;
using System.Windows.Forms;
using SmartShipment.Information.Logger;
using SmartShipment.Information.Properties;

namespace SmartShipment.Information.InformationTypes
{
    public class Error : InfoObjectBase
    {
        private readonly Exception _exception;

        public Error(ILogger logger, Exception exception, bool isMessage = true) : base(logger)
        {
            _exception = exception;
            Message = exception.Message;
            IsUIMessage = isMessage;
        }
       
        protected override void Log(ILogger logger)
        {
            logger.Error("Error shown: " + Message);
        }

        protected override DialogResult ShowMessage()
        {
            return MessageBox.Show(Message,
                InformationResources.APPLICATION_NAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification);
        }
    }
}