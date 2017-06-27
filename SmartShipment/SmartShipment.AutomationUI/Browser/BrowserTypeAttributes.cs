namespace SmartShipment.AutomationUI.Browser
{
    public class BrowserTypeAttributes : IBrowserTypeAttributes
    {
        public BrowserTypeAttributes(string processName, string searchPattern, string searchClass, string browserName, string browserClassName)
        {
            ProcessName = processName;
            SearchPattern = searchPattern;
            SearchClass = searchClass;
            BrowserName = browserName;
            BrowserClassName = browserClassName;
        }

        public string BrowserName { get; }
        public string BrowserClassName { get; }
        public string ProcessName { get; }
        public string SearchPattern { get; }
        public string SearchClass { get; }
    }
}