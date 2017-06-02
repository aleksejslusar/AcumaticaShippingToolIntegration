using System.Windows.Forms;
using SmartShipment.Information.Logger;
using SmartShipment.Information.Properties;

namespace SmartShipment.Information.InformationTypes
{
    public class Info : InfoObjectBase
    {        
        public Info(ILogger logger, string message, bool isMessage = true) : base(logger)
        {
            Message = message;
            IsUIMessage = isMessage;
        }
       
        protected override void Log(ILogger logger)
        {
            logger.Info("Info shown: " + Message);
        }

        protected override DialogResult ShowMessage()
        {
            return MessageBox.Show(Message, 
                InformationResources.APPLICATION_NAME, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information, 
                MessageBoxDefaultButton.Button1,  
                MessageBoxOptions.ServiceNotification);
        }
    }
}