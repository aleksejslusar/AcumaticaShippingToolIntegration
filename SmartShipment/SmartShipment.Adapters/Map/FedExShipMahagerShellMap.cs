using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Map
{
    public class FedExShipMahagerShellMap : ShipmentAutomationMapBase
    {
        //Toolbars
        public ShipmentAutomationTolbar BottomToolboxControl;
        public ShipmentAutomationTolbar MainToolboxControl;

        //Tab control
        public ShipmentAutomationTab TopTabControl;


        public FedExShipMahagerShellMap(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider) : base(automationControlHelper, rootAutomationElement, messagesProvider)
        {            
        }

        protected override void InitAutomationControls()
        {
            ClearUIForStartInput();

            MainToolboxControl = new ShipmentAutomationTolbar(AutomationControlHelper) {AutomaitonId = "toolStripMain"};
            BottomToolboxControl = new ShipmentAutomationTolbar(AutomationControlHelper) { AutomaitonId = "toolStripBottom" };
            TopTabControl = new ShipmentAutomationTab(AutomationControlHelper) { AutomaitonId = "tabControlShipView" };
            
            FindControlsInTree(RootAutomationElement, new []{ControlType.ToolBar, ControlType.Tab }, new List<string> { MainToolboxControl.AutomaitonId, BottomToolboxControl.AutomaitonId, TopTabControl.AutomaitonId});
            
            ControlsList.FirstOrDefault(c => c.AutomationElement.Current.AutomationId == MainToolboxControl.AutomaitonId)?.Map(MainToolboxControl);
            if (MainToolboxControl != null)
            {
                MainToolboxControl.GetButtons()?.First().Click();

                ControlsList.FirstOrDefault(c => c.AutomationElement.Current.AutomationId == TopTabControl.AutomaitonId)?.Map(TopTabControl);
                ControlsList.FirstOrDefault(c => c.AutomationElement.Current.AutomationId == BottomToolboxControl.AutomaitonId)?.Map(BottomToolboxControl);

                TopTabControl?.GetTabItems()?.First().Select();
            }
        }

        public override void ClearUIForStartInput()
        {
            base.ClearUIForStartInput();
            if (MainToolboxControl != null)
            {
                MainToolboxControl.GetButtons()?.First().Click();
                TopTabControl?.GetTabItems()?.First().Select();
            }
            else if (IsMainWindowReadyForUserInteraction())
            {
                RootAutomationElement.SetFocus();
                Keyboard.Type(Key.F2);
            }
        }
    }
}