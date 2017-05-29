using System.Linq;
using System.Windows.Automation;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.AutomationUI.UIAutomation;

namespace SmartShipment.Adapters.Helpers.ControlHelpers
{
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