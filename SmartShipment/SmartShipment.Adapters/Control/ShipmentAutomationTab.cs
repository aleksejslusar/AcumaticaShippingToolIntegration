using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using SmartShipment.Adapters.Common;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationTab : ShipmentAutomationContainerBase
    {
        public ShipmentAutomationTab(IShipmentAutomationControlHelper automationControlHelper) : base(automationControlHelper)
        {
            _toolbarTabItems = new List<ShipmentAutomationTabItem>();
        }

        private readonly List<ShipmentAutomationTabItem> _toolbarTabItems;

        public List<ShipmentAutomationTabItem> GetTabItems()
        {            
            if (!_toolbarTabItems.Any() && AutomationElement != null)
            {
                var tabItems = AutomationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem)).Cast<AutomationElement>();

                foreach (var tabItem in tabItems)
                {
                    _toolbarTabItems.Add(new ShipmentAutomationTabItem(AutomationControlHelper).Init(tabItem) as ShipmentAutomationTabItem);
                }
            }

            return _toolbarTabItems;
        }
    }
}   