using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.AutomationUI.Win32Api;

namespace SmartShipment.Adapters.Helpers
{
    public class ShipmentWinApiControlHelper : ShipmentAutomationControlHelperBase, IShipmentAutomationControlHelper
    {
        public override void Text(IShipmentAutomationControl control, string text)
        {            

            if (control.IsTypedInputRequired)
            {
                TypeTextWithFocus(control, text);
                Keyboard.Type(Key.Enter);
                return;
            }

            if (control.IsCharInputRequired)
            {
                control.AutomationElement.SetFocus();
                Win32ApiHelper.SendChars(control.NativeHwnd, text, 50);
                Keyboard.Type(Key.Enter);
                return;
            }            

            Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.WM_SETTEXT, 0, text);
            if (control.IsFocusedInputRequired)
            {
                control.AutomationElement.SetFocus();
                Keyboard.Type(Key.Enter);
            }
        }

        public override string Text(IShipmentAutomationControl control)
        {
            var length = Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.WM_GETTEXTLENGTH, 0, 0);
            if (length > 0)
            {
                var text = new StringBuilder(length + 1);
                Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.WM_GETTEXT, text.Capacity, text);
                return text.ToString();
            }
            return null;
        }

        public override void Selection(IShipmentAutomationControl control, string text)
        {
            if (Selection(control) == text) { return; }

            if (control.IsCharInputRequired)
            {
                Win32ApiHelper.SendMessage(control.NativeHwnd, 335, 1, null);                
                Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.CB_SELECTSTRING, Win32ApiHelper.SEARCH_ALL, text);               
                Win32ApiHelper.SendChars(control.NativeHwnd, text, 10);
                Win32ApiHelper.SendMessage(control.NativeHwnd, 335, 0, null);
                return;
            }

            var contents = GetListBoxContents(control.NativeHwnd);
            var item = contents.FirstOrDefault(c => string.Equals(c, text, StringComparison.CurrentCultureIgnoreCase));
            var index = contents.IndexOf(item);
            if (item != null && index > -1)
            {
                Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.CB_SETCURSEL, index, null);
            }
            else
            {
                Text(control, text);
            }
        }

        public override string Selection(IShipmentAutomationControl control)
        {
            
            var index = Win32ApiHelper.SendRefMessage(control.NativeHwnd, Win32ApiHelper.CB_GETCURSEL, 0, null).ToInt32();
            if (index > 1)
            {
                var ssb = new StringBuilder(256, 256);
                Win32ApiHelper.SendRefMessage(control.NativeHwnd, Win32ApiHelper.CB_GETLBTEXT, index, ssb);
                return ssb.ToString();
            }

            return Text(control);
        }

        public override void Checked(IShipmentAutomationControl control, bool value)
        {
            if (control.IsTypedInputRequired)
            {
                var current = Checked(control);
                if (current != value)
                {
                    control.AutomationElement.SetFocus();
                    Keyboard.Type(Key.Space);
                }                    
                return;
            }

            var bsd = value ? Win32ApiHelper.BST_CHECKED : Win32ApiHelper.BST_UNCHECKED;
            Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.BM_SETCHECK, bsd, IntPtr.Zero);
        }

        public override bool Checked(IShipmentAutomationControl control)
        {
            var state = Win32ApiHelper.SendMessage(control.NativeHwnd, Win32ApiHelper.BM_GETCHECK, 0, IntPtr.Zero);
            return  state == new IntPtr(Win32ApiHelper.BST_CHECKED);
        }

        public override void Click(IShipmentAutomationButton control)
        {
            var button = (ShipmentAutomationBase)control;
            if (button != null)
            {
                Win32ApiHelper.SendMessage(button.NativeHwnd, Win32ApiHelper.BM_CLICK, 0, IntPtr.Zero);
            }
        }

        private List<string> GetListBoxContents(IntPtr listBoxHwnd)
        {
            var cnt = (int)Win32ApiHelper.SendMessage(listBoxHwnd, Win32ApiHelper.CB_GETCOUNT, IntPtr.Zero, null);
            var listBoxContent = new List<string>();
            for (var i = 0; i < cnt; i++)
            {
                var sb = new StringBuilder(256);
                Win32ApiHelper.SendMessage(listBoxHwnd, Win32ApiHelper.CB_GETLBTEXT, (IntPtr)i, sb);
                listBoxContent.Add(sb.ToString());
            }
            return listBoxContent;
        }
    }
}