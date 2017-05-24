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


        public ParsedShipmentData(ISettings settings, string key, Dictionary<string,string> value)
        {
            _settings = settings;
            _shipmentUriData = new KeyValuePair<string, Dictionary<string, string>>(key, value);
        }

        public string BaseUrl => _shipmentUriData.Key;
        public string ShipmentNbr => _shipmentUriData.Value[SHIPMENT_NBR_PARAM];
        public string ScreenId => _shipmentUriData.Value[SCREEN_ID_PARAM];

        public bool IsDataCorrect()
        {
            return !string.IsNullOrEmpty(BaseUrl) && 
                    BaseUrl.Contains(_settings.AcumaticaBaseUrl.TrimEnd('/')) &&
                   !string.IsNullOrEmpty(ShipmentNbr) &&
                   !string.IsNullOrEmpty(ScreenId) &&
                   _shipmentScreens.Contains(ScreenId);
        }
    }
}