using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SmartShipment.AutomationUI.Win32Api
{
    public static class Win32ApiHelper
    {
        //this is a constant indicating the window that we want to send a text message
        public const int WM_SETTEXT = 0X000C;
        public const int BM_CLICK = 0x00F5;
        public const int BM_SETCHECK = 0x00f1;
        public const int BM_GETCHECK = 0xF0;
        public const int BST_CHECKED = 0x0001;
        public const int BST_UNCHECKED = 0x0;
        public const uint WM_CHAR = 0x0102;
        public const uint WM_GETTEXT = 0x000D;
        public const uint WM_GETTEXTLENGTH = 0x000E;
        public const int WM_CLOSE = 0x10;

        public const int LB_GETCOUNT = 0x018B;
        public const int LB_GETTEXT = 0x0189;
        public const int CB_GETLBTEXT = 0x0148;
        public const int CB_SETCURSEL = 0x014E;
        public const int CB_GETCURSEL = 0x147;
        public const int CB_GETCOUNT = 0x146;

        public const int SEARCH_ALL = -1;
        public const int CB_SELECTSTRING = 0x014D; //ComboBox


        public enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        public struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);

        // for WM_CHAR message
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern void SendMessage1(IntPtr hWnd, uint Msg, int wParam, int lParam);

        // for WM_COMMAND message
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern void SendMessage2(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);

        // for WM_LBUTTONDOWN and WM_LBUTTONUP messages
        [DllImport("user32.dll", EntryPoint = "PostMessage", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        // for WM_GETTEXT message
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int SendMessage3(IntPtr hWndControl, uint Msg, int wParam, byte[] lParam);

        // for LB_FINDSTRING message
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int SendMessage4(IntPtr hWnd, uint Msg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwndControl, uint Msg, int wParam, StringBuilder strBuffer); // get text

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwndControl, uint Msg, int wParam, int lParam);  // text length

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false, EntryPoint = "SendMessage")]
        public static extern IntPtr SendRefMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        public static void SendChar(IntPtr hControl, char c)
        {
            
            SendMessage1(hControl, WM_CHAR, c, 0);
        }

        public static void SendChars(IntPtr hControl, string s, int sleep = 0)
        {
            foreach (var c in s.ToCharArray())
            {                
                if (sleep > 0)
                {
                    Thread.Sleep(sleep);
                }
                SendChar(hControl, c);
            }
        }

        public static void SetWindowTopMost(Process process)
        {
            var mainWindowHwnd = process?.MainWindowHandle;
            //Check main window intptr > 0
            if (mainWindowHwnd.GetValueOrDefault() != IntPtr.Zero)
            {
                //Set window top
                //get the hWnd of the process
                var placement = new Win32ApiHelper.Windowplacement();
                var wdwIntPtr = mainWindowHwnd.GetValueOrDefault();
                Win32ApiHelper.GetWindowPlacement(wdwIntPtr, ref placement);

                // Check if window is minimized
                if (placement.showCmd == (int)Win32ApiHelper.ShowWindowEnum.ShowMinimized)
                {
                    //the window is hidden so we restore it
                    Win32ApiHelper.ShowWindow(wdwIntPtr, Win32ApiHelper.ShowWindowEnum.Restore);
                }

                //set user's focus to the window
                Win32ApiHelper.SetForegroundWindow(wdwIntPtr);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hwnd, StringBuilder lpString, int cch);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(int hwnd);


    }
}
