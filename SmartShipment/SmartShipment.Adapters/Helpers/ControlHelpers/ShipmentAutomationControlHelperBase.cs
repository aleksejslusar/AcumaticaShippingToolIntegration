using System;
using System.Linq;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.AutomationUI.UIAutomation;

namespace SmartShipment.Adapters.Helpers.ControlHelpers
{
    public abstract class ShipmentAutomationControlHelperBase : IShipmentAutomationControlHelper
    {
        protected static void TypeTextWithFocus(IShipmentAutomationControl control, string text)
        {
            Wait(control.AutomationElement.Current.IsKeyboardFocusable);
            control.AutomationElement.SetFocus();
            SendKeys.SendWait(text);
            Keyboard.Type(Key.Enter);
        }        

        protected void CloseModalWindows(AutomationElement mainWindow)
        {
            var window = mainWindow.GetWindowPattern();
            if (window.Current.WindowInteractionState != WindowInteractionState.ReadyForUserInteraction)
            {
                // get sub windows
                foreach (AutomationElement element in mainWindow.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)))
                {
                    // check a window
                    var childWindow = element.TryGetWindowPattern();
                    if (childWindow == null) continue;
                    if (childWindow.Current.WindowInteractionState == WindowInteractionState.ReadyForUserInteraction)
                    {
                        childWindow.Close();
                    }

                    //If dialog box not closed - fund cancel  button and close
                    if (childWindow.Current.WindowInteractionState == WindowInteractionState.Closing) continue;
                    var els = element.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)).Cast<AutomationElement>().ToList();
                    if (els.Any())
                    {
                        els.Last().GetInvokePattern().Invoke();
                    }
                }
            }
        }

        public void CloseModalWindowsWait(AutomationElement mainWindow)
        {
            var delayCount = 0;
            var window = mainWindow.GetWindowPattern();
            if (window.Current.WindowInteractionState == WindowInteractionState.BlockedByModalWindow)
            {
                try
                {
                    while (delayCount < 20 && window.Current.WindowInteractionState != WindowInteractionState.ReadyForUserInteraction)
                    {
                        CloseModalWindows(mainWindow);
                        delayCount++;
                    }
                }
                catch
                {
                    //DO NOTHING                    
                }
                
            }
        }

        public bool IsMainWindowReadyForUserInteraction(AutomationElement mainWindow)
        {
            var window = mainWindow.GetWindowPattern();        
            return window.Current.WindowInteractionState == WindowInteractionState.ReadyForUserInteraction;
        }

        public abstract void Text(IShipmentAutomationControl control, string text);
        public abstract string Text(IShipmentAutomationControl control);
        public abstract void Selection(IShipmentAutomationControl control, string text);
        public abstract string Selection(IShipmentAutomationControl control);
        public abstract void Checked(IShipmentAutomationControl control, bool value);
        public abstract bool Checked(IShipmentAutomationControl control);
        public abstract void Click(IShipmentAutomationButton control);

        protected static void Wait(bool waitBreakingExpression)
        {
            var delayCount = 0;
            while (delayCount < 10 && !waitBreakingExpression)
            {
                delayCount++;
                Thread.Sleep(100);
            }
        }
    }
}