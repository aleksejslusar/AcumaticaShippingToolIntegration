using System;
using System.Windows.Automation;
using Microsoft.Test.Input;

namespace SmartShipment.Adapters.Common
{
    public interface IShipmentAutomationControl
    {
        string DataFieldName { get; set; }
        string Value { get; set; }
        bool IsTypedInputRequired { get; set; }
        bool IsFocusedInputRequired { get; set; }
        int Order { get; set; }
        ShipmentDataType ShipmentDataType { get; set; }
        string Name { get; set; }
        string AutomaitonId { get; set; }
        IntPtr NativeHwnd { get; set; }
        AutomationElement AutomationElement { get; set; }
        string AutomationControlType { get; set; }
        bool IsValueRequired { get; set; }        
        bool IsCharInputRequired { get; set; }
        bool IsClearMask { get; set; }
        Func<string, bool> ValidateFunc { get; set; }
    }
}