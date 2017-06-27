using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmartShipment.AutomationUI.Browser
{
    public class BrowserType : IBrowserType
    {
        private static readonly Dictionary<string, IBrowserTypeAttributes> BrowserTypeAttributes = new Dictionary<string, IBrowserTypeAttributes>();

        static BrowserType()
        {
            BrowserTypeAttributes.Add(IE_PROCESS_NAME, IeAttributes);
            BrowserTypeAttributes.Add(FF_PROCESS_NAME, FireFoxAttributes);
            BrowserTypeAttributes.Add(CR_PROCESS_NAME, CromeAttributes);
        }

        public static IEnumerable<string> BrowserProcessNames => BrowserTypeAttributes.Keys;
        public static IEnumerable<string> BrowserClassNames => BrowserTypeAttributes.Select(b => b.Value.BrowserClassName);

        public BrowserType(Process process)
        {
            BrowserAttributes = BrowserTypeAttributes[process.ProcessName];
            Process = process;
        }

        public IBrowserTypeAttributes BrowserAttributes { get; }
        public Process Process { get; }

        public static IBrowserTypeAttributes IeAttributes = new BrowserTypeAttributes(IE_PROCESS_NAME, IE_SEARCH_PATTERN, IE_SEARCH_CLASS, IE_BROWSER_NAME, IE_CLASS_NAME);
        public static IBrowserTypeAttributes CromeAttributes = new BrowserTypeAttributes(CR_PROCESS_NAME, CR_SEARCH_PATTERN, CR_SEARCH_CLASS, CR_BROWSER_NAME, CR_CLASS_NAME);
        public static IBrowserTypeAttributes FireFoxAttributes = new BrowserTypeAttributes(FF_PROCESS_NAME, FF_SEARCH_PATTERN, FF_SEARCH_CLASS, FF_BROWSER_NAME, FF_CLASS_NAME);

        private const string IE_BROWSER_NAME = "Internet Explorer";
        private const string IE_PROCESS_NAME = "iexplore";
        private const string IE_CLASS_NAME = "IEFrame";
        private const string IE_SEARCH_PATTERN = "address";
        private const string IE_SEARCH_CLASS = "Shipments";

        private const string FF_BROWSER_NAME = "Firefox";
        private const string FF_PROCESS_NAME = "firefox";
        private const string FF_CLASS_NAME = "MozillaWindowClass";
        private const string FF_SEARCH_PATTERN = "search or enter address";
        private const string FF_SEARCH_CLASS = "Shipments";

        private const string CR_BROWSER_NAME = "Chrome";
        private const string CR_PROCESS_NAME = "chrome";
        private const string CR_CLASS_NAME = "Chrome_WidgetWin_1";
        private const string CR_SEARCH_PATTERN = "address and search bar";
        private const string CR_SEARCH_CLASS = "Shipments";
    }
}