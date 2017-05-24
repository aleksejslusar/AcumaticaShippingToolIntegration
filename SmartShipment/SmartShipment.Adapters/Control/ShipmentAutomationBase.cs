using System;
using System.Windows.Automation;
using SmartShipment.Adapters.Common;

namespace SmartShipment.Adapters.Control
{
    public abstract class ShipmentAutomationBase
    {
        private int _nativeHandle;
        protected IShipmentAutomationControlHelper AutomationControlHelper;
        public string Name { get; set; }
        public string AutomaitonId { get; set; }
        public string [] DescendentIdPath { get; set; }
        public IntPtr NativeHwnd
        {
            get { return new IntPtr(_nativeHandle); }
            set { _nativeHandle = value.ToInt32(); }
        }

        public AutomationElement AutomationElement { get; set; }
        public string AutomationControlType { get; set; }

        public virtual ShipmentAutomationBase Init(AutomationElement automationControl)
        {
            AutomationControlType = automationControl.Current.ControlType.ProgrammaticName;
            NativeHwnd = new IntPtr(automationControl.Current.NativeWindowHandle);
            AutomationElement = automationControl;
            return this;
        }

        public T Map<T>(T type) where T: ShipmentAutomationBase
        {
            type.AutomationElement = AutomationElement;
            return type;
        }
    }
}