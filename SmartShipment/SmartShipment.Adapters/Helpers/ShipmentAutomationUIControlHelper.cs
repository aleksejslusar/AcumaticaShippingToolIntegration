using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.AutomationUI.UIAutomation;

namespace SmartShipment.Adapters.Helpers
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
                while (delayCount < 20 && window.Current.WindowInteractionState != WindowInteractionState.ReadyForUserInteraction)
                {
                    CloseModalWindows(mainWindow);
                    delayCount++;
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

    public class ShipmentAutomationUIControlHelper : ShipmentAutomationControlHelperBase
    {
        public override void Text(IShipmentAutomationControl control, string text)
        {
            Wait(control.AutomationElement.Current.IsEnabled);
            if (!control.AutomationElement.Current.IsEnabled)
            {
                return;
            }

            if (control.IsTypedInputRequired)
            {
                TypeTextWithFocus(control, text);
                return;
            }

            control.AutomationElement.SetValue(text);

            if (control.IsFocusedInputRequired && control.AutomationElement.Current.IsKeyboardFocusable)
            {
                control.AutomationElement.SetFocus();
                Keyboard.Type(Key.Enter);
            }
        }

        public override string Text(IShipmentAutomationControl control)
        {
            return control.AutomationElement.GetValue();
        }

        public override void Selection(IShipmentAutomationControl control, string text)
        {
            Wait(control.AutomationElement.Current.IsEnabled);
            if (!control.AutomationElement.Current.IsEnabled)
            {
                return;
            }
            
            //Set focus if required
            if ((control.IsFocusedInputRequired || control.IsTypedInputRequired) && control.AutomationElement.Current.IsKeyboardFocusable)
            {
                control.AutomationElement.SetFocus();
            }

            //Try set with value pattern  - COMBOBOX
            if (control.IsValueRequired && control.AutomationElement.GetSupportedPatterns().Any(p => p.ProgrammaticName == ValuePattern.Pattern.ProgrammaticName))
            {                
                control.AutomationElement.SetValue(text);
                return;
            }

            //OR LISTBOX
            var comboboxList = control.AutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List));

            //Get the all the listitems in List control
            var comboboxItems = comboboxList.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));

            if (comboboxItems.Count < 1)
            {
                return;
            }

            //Index to set in combo box
            var itemToSelect = comboboxItems.Cast<AutomationElement>().FirstOrDefault(e => e.Current.Name.StartsWith(text));


            //Finding the pattern which need to select            
            itemToSelect?.GetSelectionItemPattern()?.Select();
        }

        public override string Selection(IShipmentAutomationControl control)
        {
            //Combobox
            if (control.AutomationElement.GetSupportedPatterns().Any(p => p.ProgrammaticName == ValuePattern.Pattern.ProgrammaticName))
            {
                return control.AutomationElement.GetValue();
            }

            //Listbox
            var comboboxList = control.AutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List));
            var comboboxItems = comboboxList.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
            if (comboboxItems.Count >  0)
            {
                return (from AutomationElement comboboxItem in comboboxItems
                        where comboboxItem.GetSelectionItemPattern().Current.IsSelected
                        select comboboxItem.Current.Name).FirstOrDefault();
            }

            return null;
        }

        public override void Checked(IShipmentAutomationControl control, bool value)
        {
            if (!control.AutomationElement.Current.IsEnabled)
                return;

            if (value && control.AutomationElement.GetTogglePattern().Current.ToggleState == ToggleState.Off)
            {
                control.AutomationElement.GetTogglePattern().Toggle();
            }

            if (!value && control.AutomationElement.GetTogglePattern().Current.ToggleState == ToggleState.On)
            {
                control.AutomationElement.GetTogglePattern().Toggle();
            }

        }

        public override bool Checked(IShipmentAutomationControl control)
        {
            return control.AutomationElement.GetTogglePattern().Current.ToggleState == ToggleState.On;
        }

        public override void Click(IShipmentAutomationButton control)
        {
            var button = (ShipmentAutomationBase) control;
            button?.AutomationElement.GetInvokePattern().Invoke();
        }
    }
}