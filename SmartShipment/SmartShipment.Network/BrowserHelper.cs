using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using SmartShipment.AutomationUI.Browser;
using SmartShipment.AutomationUI.Win32Api;
using SmartShipment.Network.Common;
using System.Windows.Automation;
using SmartShipment.Information.Exceptions;
using SmartShipment.Information.Properties;

namespace SmartShipment.Network
{
    public class BrowserHelper : IBrowserHelper
    {
        private readonly IUIAutomationBrowserHelper _automationUtils;
        private readonly IAcumaticaUriParser _acumaticaUriParser;

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        const uint GW_HWNDPREV = 3;
        const uint GW_HWNDLAST = 1;

        static int GetZOrder(IntPtr hWnd)
        {
            var z = 0;
            for (var h = hWnd; h != IntPtr.Zero; h = GetWindow(h, GW_HWNDPREV)) z++;
            return z;
        }

        public BrowserHelper(IUIAutomationBrowserHelper automationUtils, IAcumaticaUriParser acumaticaUriParser)
        {
            _automationUtils = automationUtils;
            _acumaticaUriParser = acumaticaUriParser;
        }

        public ParsedShipmentData GetShipmentUriData()
        {
            var urlString = GetUrlUseUIAutomation();
            var shipmentId = _acumaticaUriParser.GetShipmentId(urlString);
            if (shipmentId != null && shipmentId.IsDataCorrect())
            {
                return shipmentId;
            }

            throw new NetworkActiveUriNotFoundException(InformationResources.ERROR_NO_SHIPMENT_URI_FOUND);
        }

        public void ReloadActiveBrowserPage(string shipmentId)
        {
            var topMostBrowser = GetOrderedBrowserProcesses().FirstOrDefault();
            if (topMostBrowser != null)
            {

                try
                {
                    var shipmentUriData = GetShipmentUriData();
                    if (!shipmentUriData.IsDataCorrect() || shipmentUriData.ShipmentNbr != shipmentId) return;
                    Win32ApiHelper.SetWindowTopMost(topMostBrowser);
                    _automationUtils.RefreshBrowserCurrentPage();
                }
                catch (NetworkActiveUriNotFoundException)
                {
                    // Do nothing  - just not reaload browser
                }
            }
        }

        private string GetUrlUseUIAutomation()
        {
            var zOrderedProcesses = GetOrderedBrowserProcesses();
            if (zOrderedProcesses.Any())
            {
                var activeBrowserProcess = zOrderedProcesses.First();
                var activeBrowser = _automationUtils.GetProcessBrowserType(activeBrowserProcess);
                var shipmentUri = _automationUtils.GetBrowserUrl(activeBrowser);
                return shipmentUri;
            }

            throw new  NetworkActiveBrowserNotFoundException(InformationResources.ERROR_NO_ACTIVE_BROWSER_FOUND);                       
        }

        private Process[] GetOrderedBrowserProcesses()
        {
            var rootElement = AutomationElement.RootElement;
            var winCollection = rootElement.FindAll(TreeScope.Children, Condition.TrueCondition);
            var processList = new Dictionary<int, Process>();
            
            foreach (var element in winCollection.Cast<AutomationElement>().Where(e => /*e.Current.Name.StartsWith("Shipments") 
                                                                                    &&*/ _automationUtils.BrowserClassNames.Contains(e.Current.ClassName)
                                                                                    && e.Current.NativeWindowHandle > 0))
            {                
                var zOrder = GetZOrder(new IntPtr(element.Current.NativeWindowHandle));
                var process = Process.GetProcessById(element.Current.ProcessId);
                processList.Add(zOrder, process);        
            }

            return processList.OrderBy(p => p.Key).Select(p => p.Value).ToArray();
        }
    }
}
