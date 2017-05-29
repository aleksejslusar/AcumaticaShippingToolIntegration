using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;
using SmartShipment.Information.Exceptions;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Adapters.Map
{
    public abstract class ShipmentAutomationMapBase : IEnumerable<ShipmentAutomationBase>
    {
        protected readonly IShipmentAutomationControlHelper AutomationControlHelper;

        protected AutomationElement RootAutomationElement;

        protected readonly ISmartShipmentMessagesProvider MessagesProvider;

        protected ShipmentAutomationMapBase(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider)
        {
            AutomationControlHelper = automationControlHelper;
            RootAutomationElement = rootAutomationElement;
            MessagesProvider = messagesProvider;
            CloseModalWindowsWait();        
            Init();
        }

        protected List<ShipmentAutomationBase> ControlsList { get; private set; }

        public List<ShipmentAutomationControl> ShipmentAutomationControls => ControlsList.OfType<ShipmentAutomationControl>().ToList();

        public List<ShipmentAutomationControl> RequiredShipmentPane => ShipmentAutomationControls.Where(c => c.IsValueRequired).ToList();

        public bool IsAutomationMapped => ShipmentAutomationControls.All(c => c.AutomationElement != null);

        public int GetNativeWindowHandle()
        {
            return RootAutomationElement.Current.NativeWindowHandle;
        }

        public void Map()
        {
            if (!IsMainWindowReadyForUserInteraction())
            {
                throw new AdaptersException(InformationResources.ERROR_ADAPTER_SHIPMENT_SCREEN_IS_NOT_INTERACTIVE);
            }
            if (!IsAutomationMapped)
            {
                MapAutomationControls(RootAutomationElement, ControlsList);
            }
        }

        public void MapShipmentData(IEnumerable<IShipmentValue<string>> data)
        {
            MapShipmentData(ShipmentAutomationControls, ShipmentDataType.Shipment, data, RootAutomationElement);
        }

        public void MapPackageData(IEnumerable<IShipmentValue<string>> data)
        {
            MapShipmentData(ShipmentAutomationControls, ShipmentDataType.Package, data, RootAutomationElement);
        }

        public void CloseModalWindowsWait()
        {
            AutomationControlHelper.CloseModalWindowsWait(RootAutomationElement);
        }

        public bool IsMainWindowReadyForUserInteraction()
        {
            return AutomationControlHelper.IsMainWindowReadyForUserInteraction(RootAutomationElement);
        }

        protected abstract void InitAutomationControls();

        private void Init()
        {
            if (ControlsList == null)
            {
                ControlsList = new List<ShipmentAutomationBase>();
                InitAutomationControls();               
            }
        }

        IEnumerator<ShipmentAutomationBase> IEnumerable<ShipmentAutomationBase>.GetEnumerator()
        {
            return ControlsList.TakeWhile(control => control != null).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ControlsList.GetEnumerator();
        }

        private static void MapAutomationControls(AutomationElement parent, List<ShipmentAutomationBase> shipManagerMap)
        {            
            //Map controls
            foreach (var automationControl in parent.FindAll(TreeScope.Children, Condition.TrueCondition).Cast<AutomationElement>())
            {
                var control = shipManagerMap.FirstOrDefault(c => c.AutomationElement == null && c.AutomaitonId == automationControl.Current.AutomationId);
                control?.Init(automationControl);

                if (shipManagerMap.Any(c => c.AutomationElement == null || c.NativeHwnd == IntPtr.Zero))
                {
                    MapAutomationControls(automationControl, shipManagerMap);
                }
            }
        }

        private void MapShipmentData<T>(IEnumerable<T> controlList, ShipmentDataType type, IEnumerable<IShipmentValue<string>> data, AutomationElement mainWindow) where T : ShipmentAutomationControl
        {
            var shipmentValues = data as IList<IShipmentValue<string>> ?? data.ToList();
            foreach (var control in controlList.Where(c => c.ShipmentDataType == type 
                                                        && c.AutomationElement != null  
                                                        && !string.IsNullOrEmpty(c.DataFieldName))
                                               .OrderBy(c => c.Order)
                                               .ToList())
            {
                var shipmentValue = shipmentValues.FirstOrDefault(s => s.ValueMapName == control.DataFieldName);
                if (shipmentValue?.Value != null)
                {
                    control.SetValueToControl(shipmentValue.Value);                                        
                }                
            }
        }

        public virtual void ClearUIForStartInput()
        {
            CloseModalWindowsWait();            
        }

        public void FindControlsInTree(AutomationElement parent, ControlType [] controlTypes, List<string> automationIds)
        {
            foreach (var control in parent.FindAll(TreeScope.Children, Condition.TrueCondition).Cast<AutomationElement>())
            {
                if (controlTypes.Any(c => c.Equals(control.Current.ControlType)) && automationIds.Any(e => e == control.Current.AutomationId))
                {
                    var element = ControlsList.FirstOrDefault(c => c.AutomaitonId == control.Current.AutomationId);
                    if (element != null)
                    {
                        element.Init(control);
                    }
                    else
                    {
                        element = new ShipmentAutomationContainerBase(AutomationControlHelper){AutomationElement = control};
                        ControlsList.Add(element);
                    }
                    
                    automationIds.Remove(control.Current.AutomationId);
                }

                if (!automationIds.Any()) return;

                FindControlsInTree(control, controlTypes, automationIds);
            }
        }

        
    }
}