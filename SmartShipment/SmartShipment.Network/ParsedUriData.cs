using System.Collections.Generic;
using SmartShipment.Settings;

namespace SmartShipment.Network
{
    public class ParsedShipmentData
    {
        private readonly ISettings _settings;
        private KeyValuePair<string, Dictionary<string, string>> _shipmentUriData;
        private readonly List<string> _shipmentScreens = new List<string>{ "SO302000"};

        public const string SCREEN_ID_PARAM = "ScreenId";
        public const string SHIPMENT_NBR_PARAM = "ShipmentNbr";
        public const string SCREEN_POPUP_ON = "PopupPanel";


        public ParsedShipmentData(ISettings settings, string key, Dictionary<string,string> value)
        {
            _settings = settings;
            _shipmentUriData = new KeyValuePair<string, Dictionary<string, string>>(key, value);
        }

        public string BaseUrl => _shipmentUriData.Key;
        public string SettingsBaseUrl => _settings.AcumaticaBaseUrl;
        public string ShipmentNbr => _shipmentUriData.Value.ContainsKey(SHIPMENT_NBR_PARAM)? _shipmentUriData.Value[SHIPMENT_NBR_PARAM] : string.Empty;
        public string ScreenId => _shipmentUriData.Value.ContainsKey(SCREEN_ID_PARAM) ? _shipmentUriData.Value[SCREEN_ID_PARAM] : string.Empty;
        public string PopupPanel => _shipmentUriData.Value.ContainsKey(SCREEN_POPUP_ON) ? _shipmentUriData.Value[SCREEN_POPUP_ON] : string.Empty;

        public bool IsDataCorrect()
        {
            return !string.IsNullOrEmpty(BaseUrl) && 
                   BaseUrl.ToLowerInvariant().Equals(_settings.AcumaticaBaseUrl.ToLowerInvariant().TrimEnd('/')) &&
                   !string.IsNullOrEmpty(ShipmentNbr) && 
                   ((string.IsNullOrEmpty(PopupPanel) && !string.IsNullOrEmpty(ScreenId) && _shipmentScreens.Contains(ScreenId)) || (!string.IsNullOrEmpty(PopupPanel) && string.IsNullOrEmpty(ScreenId) && !_shipmentScreens.Contains(ScreenId)));
        }
    }
}