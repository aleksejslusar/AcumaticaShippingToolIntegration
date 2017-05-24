using System.Collections.Generic;
using System.Diagnostics;

namespace SmartShipment.AutomationUI.Browser
{
    public interface IUIAutomationBrowserHelper
    {
        string GetBrowserUrl(IBrowserType browserType);
        IBrowserType GetProcessBrowserType(Process process);
        IEnumerable<string> BrowserProcessNames { get; }
        IEnumerable<string> BrowserClassNames { get; }
        void RefreshBrowserCurrentPage();
    }
}