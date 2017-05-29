using System;
using System.Collections.Generic;
using System.Windows.Automation;
using SmartShipment.Adapters.Helpers;
using SmartShipment.Adapters.Helpers.ControlHelpers;
using SmartShipment.Adapters.Map;
using SmartShipment.Information;
using SmartShipment.Information.Properties;

namespace SmartShipment.Adapters.Cache
{
    public interface IShipmentAutomationCache
    {
        object Get(ShipmentApplicaotinHelperType name, AutomationElement rootAutomationElement);        
    }

    public class ShipmentAutomationCache : IShipmentAutomationCache
    {
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        public ShipmentAutomationCache(ISmartShipmentMessagesProvider messagesProvider)
        {
            _messagesProvider = messagesProvider;
            ApplicationMapObjects = new Dictionary<ShipmentApplicaotinHelperType, ShipmentAutomationMapBase>();
        }

        private Dictionary<ShipmentApplicaotinHelperType, ShipmentAutomationMapBase> ApplicationMapObjects { get; }

        public object Get(ShipmentApplicaotinHelperType name, AutomationElement rootAutomationElement)
        {
            if (ApplicationMapObjects.ContainsKey(name) && ApplicationMapObjects[name].GetNativeWindowHandle() == rootAutomationElement.Current.NativeWindowHandle)
            {
                return ApplicationMapObjects[name];
            }
            object applicationHelper;
            switch (name)
            {
                case ShipmentApplicaotinHelperType.FedexShipManagerMap:
                    applicationHelper = new FedexShipManagerMap(new ShipmentAutomationUIControlHelper(), rootAutomationElement, _messagesProvider);
                    Set(ShipmentApplicaotinHelperType.FedexShipManagerMap, (ShipmentAutomationMapBase)applicationHelper);
                    break;
                case ShipmentApplicaotinHelperType.FedExMultiPieceShipmentMap:
                    applicationHelper = new FedExMultiPieceShipmentMap(new ShipmentAutomationUIControlHelper(), rootAutomationElement, _messagesProvider);
                    Set(ShipmentApplicaotinHelperType.FedExMultiPieceShipmentMap, (ShipmentAutomationMapBase)applicationHelper);
                    break;
                case ShipmentApplicaotinHelperType.UpsManagerMap:
                    applicationHelper = new UpsManagerMap(new ShipmentWinApiControlHelper(), rootAutomationElement, _messagesProvider);
                    Set(ShipmentApplicaotinHelperType.UpsManagerMap, (ShipmentAutomationMapBase)applicationHelper);
                    break;
                case ShipmentApplicaotinHelperType.FedExShipAlertTabMap:
                    applicationHelper = new FedExShipAlertTabMap(new ShipmentAutomationUIControlHelper(), rootAutomationElement, _messagesProvider);
                    Set(ShipmentApplicaotinHelperType.FedExShipAlertTabMap, (ShipmentAutomationMapBase)applicationHelper);
                    break;
                case ShipmentApplicaotinHelperType.FedExShipMahagerShellMap:
                    applicationHelper = new FedExShipMahagerShellMap(new ShipmentAutomationUIControlHelper(), rootAutomationElement, _messagesProvider);
                    Set(ShipmentApplicaotinHelperType.FedExShipMahagerShellMap, (ShipmentAutomationMapBase)applicationHelper);
                    break;
                case ShipmentApplicaotinHelperType.UpsManagerShellMap:
                    applicationHelper = new UpsManagerShellMap(new ShipmentWinApiControlHelper(), rootAutomationElement, _messagesProvider);
                    Set(ShipmentApplicaotinHelperType.UpsManagerShellMap, (ShipmentAutomationMapBase)applicationHelper);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(name), name, string.Format(InformationResources.ERROR_CREATE_APPLICATION_MAP_CACHE, nameof(name)));
            }
            return applicationHelper;
        }

        private void Set(ShipmentApplicaotinHelperType name, ShipmentAutomationMapBase map)
        {
            if (!ApplicationMapObjects.ContainsKey(name))
            {
                ApplicationMapObjects.Add(name, map);
            }
        }
    }

    public enum ShipmentApplicaotinHelperType
    {
        FedexShipManagerMap = 0,
        FedExMultiPieceShipmentMap = 1,
        UpsManagerMap = 3,
        FedExShipAlertTabMap = 4,
        FedExShipMahagerShellMap = 5,
        UpsManagerShellMap = 6
    }
}