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
            var controls = element?.FindAll(TreeScope.Subtree, new OrCondition(new PropertyCondition(AutomationElement.NameProperty, browserType.BrowserAttributes.SearchPattern, PropertyConditionFlags.IgnoreCase), 
                                                                               new PropertyCondition(AutomationElement.NameProperty, browserType.BrowserAttributes.SearchClass)));
            if (controls != null && controls.Count > 0)
            {
                for (var i = 0; i < controls.Count; i++)
                {
                    var edit = controls[i];
                    if ((edit.Current.Name.Equals(browserType.BrowserAttributes.SearchPattern, StringComparison.CurrentCultureIgnoreCase) || 
                        edit.Current.Name.Equals(browserType.BrowserAttributes.SearchClass, StringComparison.CurrentCultureIgnoreCase)) && 
                        (Equals(edit.Current.ControlType, ControlType.Edit) || Equals(edit.Current.ControlType, ControlType.Pane) || Equals(edit.Current.ControlType, ControlType.Text)))
                    {
                        object valuePattern;
                        edit.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern);
                        var value = ((ValuePattern) valuePattern)?.Current.Value;
                        if (!string.IsNullOrEmpty(value))
                        {
                            return value;
                        }
                    }
                }                                
            }

            return null;
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
