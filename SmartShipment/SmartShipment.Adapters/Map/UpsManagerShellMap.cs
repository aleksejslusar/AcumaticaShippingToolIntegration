using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Map
{
    public class UpsManagerShellMap : ShipmentAutomationMapBase
    {       
        public UpsManagerShellMap(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider) : base(automationControlHelper, rootAutomationElement, messagesProvider){}

        public List<ShipmentAutomationTab> Tabs => ControlsList.Select(c => c.Map(new ShipmentAutomationTab(AutomationControlHelper))).ToList();


        protected override void InitAutomationControls()
        {            
            FindControlsInTree(RootAutomationElement, new []{ControlType.Tab}, new List<string> {"12320", "12320"});           
        }

        public override void ClearUIForStartInput()
        {
            base.ClearUIForStartInput();
            if (IsMainWindowReadyForUserInteraction())
            {
                RootAutomationElement.SetFocus();
                Keyboard.Type(Key.F2);
            }

            foreach (var tab in Tabs)
            {
                tab.GetTabItems()?.First()?.Select();
            }             
        }
    }
}