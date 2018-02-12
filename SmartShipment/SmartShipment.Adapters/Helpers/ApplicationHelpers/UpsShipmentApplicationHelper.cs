using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Automation;
using SmartShipment.Adapters.Cache;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Map;
using SmartShipment.AutomationUI.UIAutomation;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Mapping;
using SmartShipment.Settings;

// ReSharper disable InconsistentNaming
#pragma warning disable 169

namespace SmartShipment.Adapters.Helpers.ApplicationHelpers
{
    public class UpsShipmentApplicationHelper : ShipmentApplicationHelperBase, IShipmentApplicationHelper
    {
        private readonly ISettings _settings;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;
        private readonly IShipmentAutomationCache _cache;
        private AutomationElement _mainWindow;
        private AutomationElement _workplace;
        private const string _workplaceAutomationId = "59648";

        private UpsManagerMap _upsManagerMap;
        private Process _process;
        private UpsManagerShellMap _upsManagerShellMap;       

        public UpsShipmentApplicationHelper(ISettings settings, ISmartShipmentMessagesProvider messagesProvider, IShipmentAutomationCache cache)
        {
            _settings = settings;
            _messagesProvider = messagesProvider;
            _cache = cache;
        }

        #region Workflow

        public bool RunShipmentApplication(string shipmentNumber)
        {
            _messagesProvider.Log(InformationResources.INFO_UPS_STARTING_APPLICATION);
            _process = RunApplication(_settings.UpsProcessName);
            if (_process != null)
            {
                var delayCount = 0;
                while (delayCount < 10 && (_mainWindow == null || _workplace == null))
                {
                    _mainWindow = AutomationElement.RootElement.FindChildByProcessId(_process.Id);
                    _workplace = _mainWindow.FindDescendentByIdPath(new[] {_workplaceAutomationId});
                    delayCount++;
                }

                if (_mainWindow != null && _workplace != null)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_UPS_START_APPLICATION, shipmentNumber));
                    return true;
                }
                else
                {
                    _messagesProvider.Warn(string.Format(InformationResources.WARN_NO_PROCESS_INIT_FOR_UPS_APPLICATION, shipmentNumber));
                    return false;
                }

            }
            else
            {
                _messagesProvider.Warn(string.Format(InformationResources.WARN_NO_PROCESS_FOUND_FOR_UPS_APPLICATION, shipmentNumber));
                return false;
            }
        }

        public bool PopulateApplicaitonControlMap()
        {
            _upsManagerMap = (UpsManagerMap) _cache.Get(ShipmentApplicaotinHelperType.UpsManagerMap, _mainWindow);
            _upsManagerShellMap = (UpsManagerShellMap) _cache.Get(ShipmentApplicaotinHelperType.UpsManagerShellMap, _mainWindow);
            _upsManagerShellMap.ClearUIForStartInput();

            _upsManagerMap.Map();

            if (NeedClearing(_upsManagerMap.ShipmentAutomationControls))
            {
                _messagesProvider.Info(InformationResources.WARN_CURRENT_SHIPMENT_SCREEN_ISNOT_EMPTY);
                return false;
            }

            _messagesProvider.Log(InformationResources.INFO_UPS_INIT_CONTROL_MAP);
            return true;
        }

        public void PopulateApplicaitonByShipmentData(ShipmentMapper shipment)
        {
            StartTimer(_upsManagerMap);

            try
            {
                //Shipment informaiton
                SendShipmentData(shipment);                
                Wait(1);
                if (CheckShipmentProgrammWarnings(_messagesProvider)) return;

                //Package informaiton
                if (CheckShipmentFieldsFilled(_upsManagerMap.RequiredShipmentPane, _messagesProvider))
                {
                    _messagesProvider.Log(InformationResources.INFO_UPS_FILL_SHIPMENT_DATA);
                    SendPackagesData(shipment);
                }
                
            }
            finally
            {
                StopTimer();
            }

            CheckShipmentProgrammWarnings(_messagesProvider);
        }

        #endregion

        #region Private

        private void SendShipmentData(ShipmentMapper shipment)
        {
            PrepareControlsBeforeWorkflow(shipment);

            //Check Update addressbook checkbox
            _upsManagerMap.UpdateAddressBookCheckBox.SetValueToControl(_settings.UpsAddUpdateAddressBook ? "true" : "false");
            _upsManagerMap.MapShipmentData(shipment);
            Wait(1);
            
            //Is Country is not US
            if (shipment.Contact.Address.Country.Value != "US")
            {
                _upsManagerMap.FindControlsInTree(_mainWindow, new[] {ControlType.Edit, ControlType.ComboBox},
                    new List<string>
                    {
                        _upsManagerMap.UpsServiceComboBox.AutomaitonId,
                        _upsManagerMap.GeneralDescOfGoodsPane.AutomaitonId
                    });
                _upsManagerMap.UpsServiceComboBox.SetValueToControl("S");
                _upsManagerMap.GeneralDescOfGoodsPane.SetValueToControl("Automatic information input");
            }
        }

        private void PrepareControlsBeforeWorkflow(ShipmentMapper shipment)
        {
            if (shipment.Contact.Address.Country.Value == "US")
            {
                _upsManagerMap.PostalCodePane.IsClearMask = true;
                _upsManagerMap.PostalCodePane.MaxLength = 9;
                _upsManagerMap.TelephonePane.IsClearMask = true;
                _upsManagerMap.TelephonePane.MaxLength = 10; 
                _upsManagerMap.TelephonePane.IsTypedInputRequired = true;
                _upsManagerMap.TelephonePane.IsCharInputRequired = false;
            }
            else
            {
                _upsManagerMap.PostalCodePane.IsClearMask = false;
                _upsManagerMap.TelephonePane.IsClearMask = false;
                _upsManagerMap.TelephonePane.MaxLength = 15; 
                _upsManagerMap.TelephonePane.IsTypedInputRequired = false;
                _upsManagerMap.TelephonePane.IsCharInputRequired = true;
            }
        }

        private void SendPackagesData(ShipmentMapper shipment)
        {
            if (!shipment.Packages.Any())
            {
                _upsManagerMap.ReferenceNumberOnePane.SetValueToControl(shipment.ShipmentNbr.Value.Trim());
                _upsManagerMap.PackageWeightPane.AutomationElement.SetFocus();
            }

            foreach (var package in shipment.Packages)
            {
                if (IsWarnDialogFired)
                {
                    StopTimer();
                    break;
                }

                _upsManagerMap.PackageWeightPane.SetValueToControl(package.PackageFormattedWeight.Value);

                var delayCount = 100;
                while (delayCount > 0 &&
                       string.IsNullOrEmpty(_upsManagerMap.ReferenceNumberOnePane.GetCurrentValue().Trim()))
                {
                    delayCount--;
                    _upsManagerMap.ReferenceNumberOnePane.SetValueToControl(shipment.ShipmentNbr.Value);
                }
                _upsManagerMap.AddPackageButton.Click();
            }
            _messagesProvider.Log(InformationResources.INFO_UPS_FILL_PACKAGE_DATA);
        }

        #endregion
        
    }
}
