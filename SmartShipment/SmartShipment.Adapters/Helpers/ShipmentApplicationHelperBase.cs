using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Adapters.Map;
using SmartShipment.AutomationUI.Win32Api;
using SmartShipment.Information.Properties;
using Timer = System.Timers.Timer;

namespace SmartShipment.Adapters.Helpers
{
    public class ShipmentApplicationHelperBase
    {
        protected Timer Timer;
        protected bool IsWarnDialogFired;
        protected char[] TrimCharsArray = {' ', '-', '(', ')', '[', ']'};

        protected Process RunApplication(string processName)
        {
            //Validate
            if (string.IsNullOrEmpty(processName))
            {
                throw new Exception(InformationResources.ERROR_EXTERNAL_APPLICATION_PROCESS_NAME_EMPTY);
            }

            //Get application process identificator
            var process = Process.GetProcesses().FirstOrDefault(p => p.ProcessName.Contains(processName));
            
            //Set Window on Top
            Win32ApiHelper.SetWindowTopMost(process);

            return process;
        }

        protected bool NeedClearing(IEnumerable<ShipmentAutomationControl> controls)
        {
            return controls.OfType<ShipmentAutomationPane>().Any( c => c.AutomationElement != null &&
                                                                      !string.IsNullOrEmpty(c.GetCurrentValue()) && 
                                                                      !string.IsNullOrEmpty(c.GetCurrentValue().Trim(TrimCharsArray)));
        }

        protected List<ShipmentAutomationControl> CheckRequiredFieldsFilled(List<ShipmentAutomationControl> controls)
        {
            var notValidControls = new List<ShipmentAutomationControl>();

            var nullControls = controls.Where(c => c.ShipmentDataType == ShipmentDataType.Shipment && c.IsValueRequired && c.AutomationElement == null)
                                       .Select(c => c)
                                       .ToList();

            var nullValueControls = controls.Where(c => c.ShipmentDataType == ShipmentDataType.Shipment 
                                                     && c.IsValueRequired
                                                     && c.AutomationElement != null 
                                                     && !c.ValidateFunc.Invoke(c.GetCurrentValue()))
                                            .Select(c => c)
                                            .ToList();

            notValidControls.AddRange(nullControls);
            notValidControls.AddRange(nullValueControls);

            return notValidControls;
        }

        protected void Wait(int delayCount)
        {
            var timeout = 100*delayCount;
            Thread.Sleep(timeout);
        }

        protected void OnTimerElapsed(ShipmentAutomationMapBase automationMap)
        {            
            if (!automationMap.IsMainWindowReadyForUserInteraction())
            {
                IsWarnDialogFired = true;
                automationMap.CloseModalWindowsWait();
            }            
        }

        protected void StopTimer()
        {
            Timer.Stop();
            Timer.Close();
        }

        protected void StartTimer(ShipmentAutomationMapBase automationMap)
        {
            Timer = new Timer {Interval = 100, AutoReset = true};
            Timer.Elapsed += (sender, args) => OnTimerElapsed(automationMap);
            Timer.Start();
        }
    }
}