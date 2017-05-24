using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Automation;
using Microsoft.Test.Input;

namespace SmartShipment.AutomationUI.Browser
{
    public class UIAutomationBrowserHelper : IUIAutomationBrowserHelper
    {
        public string GetBrowserUrl(IBrowserType browserType)
        {
            if (browserType.Process == null)
            {
                throw new ArgumentNullException("Browser process not found: " + nameof(browserType.BrowserAttributes.BrowserName));
            }

            //Skip not active tabs
            if (browserType.Process.MainWindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException("Browser is not active right now: " + nameof(browserType.BrowserAttributes.BrowserName));
            }

            var element = AutomationElement.FromHandle(browserType.Process.MainWindowHandle);
            
            var edit = element?.FindFirst(TreeScope.Subtree, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, browserType.BrowserAttributes.SearchPattern, PropertyConditionFlags.IgnoreCase),
                                                                              new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)));
            return ((ValuePattern) edit?.GetCurrentPattern(ValuePattern.Pattern))?.Current.Value;
        }

        public IBrowserType GetProcessBrowserType(Process process)
        {
            return new BrowserType(process);
        }

        public IEnumerable<string> BrowserProcessNames => BrowserType.BrowserProcessNames;
        public IEnumerable<string> BrowserClassNames => BrowserType.BrowserClassNames;
        public void RefreshBrowserCurrentPage()
        {
            Keyboard.Type(Key.F5);
        }
    }
}
