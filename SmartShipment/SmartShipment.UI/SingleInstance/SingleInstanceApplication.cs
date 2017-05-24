using Microsoft.VisualBasic.ApplicationServices;

namespace SmartShipment.UI.SingleInstance
{
    public class SingleInstanceApplication : WindowsFormsApplicationBase
    {                        
        public SingleInstanceApplication()
        {
            // Set IsSingleInstance property to true to make the application  
            IsSingleInstance = true;                        
        }       
    }
}
