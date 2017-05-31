using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Control
{
    public abstract class ShipmentAutomationControl : ShipmentAutomationBase, IShipmentAutomationControl
    {
        protected char[] TrimCharsArray = new char[] { ' ', '-', '(', ')', '[', ']' };

        private readonly ISmartShipmentMessagesProvider _messagesProvider;
        public string DataFieldName { get; set; }
        public string Value { get; set; }
        public bool IsTypedInputRequired { get; set; }
        public bool IsFocusedInputRequired { get; set; }
        public bool IsValueRequired { get; set; }
        public int Order { get; set; }
        public int MaxLength { get; set; }
        public ShipmentDataType ShipmentDataType { get; set; }        
        public bool IsCharInputRequired { get; set; }
        public bool IsClearMask { get; set; } //MaxLength property MUST be setted together!!!!
        public virtual Func<string, bool> ValidateFunc { get; set; }

        protected ShipmentAutomationControl(ISmartShipmentMessagesProvider messagesProvider)
        {
            _messagesProvider = messagesProvider;            
        }

        public void SetValueToControl(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            Value = value.Trim();
            
            try
            {
                var delayCount = 5;
                while (delayCount > 0 && (string.IsNullOrEmpty(GetCurrentValue()) || !ValidateFunc.Invoke(GetCurrentValue())))                          
                {
                    SetControlValue();                    
                    delayCount--;
                    Thread.Sleep(delayCount == 4 ? 100 : 1000);
                }
            }
            catch (Exception e)
            {
                _messagesProvider.Warn($"Unable to perform of set value to control operation. AutomationId: {AutomaitonId}, Name: {Name}, Value: {value}");
                _messagesProvider.Log(e.Message);
            }
        }

        public string GetCurrentValue()
        {
            try
            {
                return GetcontrolValue();
            }
            catch (Exception e)
            {
                _messagesProvider.Warn($"Unable to perform of get value of control operation. AutomationId: {AutomaitonId}, Name: {Name}");
                _messagesProvider.Log(e.Message);
            }
            return null;
        }

        protected abstract void SetControlValue();

        protected abstract string GetcontrolValue();

        protected static string ClearForMaskedInput(string value, int maxLength = 10)
        {
            if (maxLength == 0)
                maxLength = 10;

            if (!string.IsNullOrEmpty(value))
            {
                var clearedValue = Regex.Replace(value, @"\W+", "");
                if (clearedValue.Length > maxLength)
                {
                    var startIndex = clearedValue.Length - maxLength;
                    return clearedValue.Substring(startIndex);
                }
                return clearedValue;
            }
            throw new ArgumentException("Error to clearing value mask");
        }
    }
}