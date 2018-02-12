using System;
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
                //TODO: remove or comment this message after fix issue.
                MessagesProvider.Log($"----------------- Root automation element info -------------------" +
                                     $"automatoinID: {RootAutomationElement.Current.AutomationId}, " +
                                     $"name:         {RootAutomationElement.Current.Name}, " +
                                     $"hwnd:         {RootAutomationElement.Current.NativeWindowHandle} " +
                                     $"className:    {RootAutomationElement.Current.ClassName} ");

                try
                {
                    RootAutomationElement.SetFocus();
                    Keyboard.Type(Key.F2);
                }
                catch (Exception e)
                {                    
                    MessagesProvider.Log($"Cannot clear UI for start input: Element name {RootAutomationElement.Current.Name}, Exception: {e}");
                }                
            }

            foreach (var tab in Tabs)
            {
                tab.GetTabItems()?.First()?.Select();
            }             
        }
    }
}