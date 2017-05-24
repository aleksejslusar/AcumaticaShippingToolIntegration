using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using SmartShipment.Adapters.Common;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationTolbar : ShipmentAutomationContainerBase
    {
        public ShipmentAutomationTolbar(IShipmentAutomationControlHelper automationControlHelper): base(automationControlHelper)
        {
            _toolbarButtons = new List<ShipmentAutomationButton>();
        }

        private readonly List<ShipmentAutomationButton> _toolbarButtons;

        public List<ShipmentAutomationButton> GetButtons()
        {
            
            if (!_toolbarButtons.Any() && AutomationElement != null)
            {
                var buttons = AutomationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)).Cast<AutomationElement>();

                foreach (var button in buttons)
                {                   
                    _toolbarButtons.Add(new ShipmentAutomationButton(AutomationControlHelper).Init(button) as ShipmentAutomationButton);
                }
            }

            return _toolbarButtons;
        }
    }
}   