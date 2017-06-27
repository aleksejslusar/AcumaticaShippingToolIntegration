namespace SmartShipment.AutomationUI.Browser
{
    public interface IBrowserTypeAttributes
    {
        string BrowserName { get; }
        string BrowserClassName { get; }
        string ProcessName { get; }
        string SearchPattern { get; }
        string SearchClass { get; }
    }
}