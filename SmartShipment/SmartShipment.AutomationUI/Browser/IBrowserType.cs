using System.Diagnostics;

namespace SmartShipment.AutomationUI.Browser
{
    public interface IBrowserType
    {
        IBrowserTypeAttributes BrowserAttributes { get; }
        Process Process { get; }
    }
}