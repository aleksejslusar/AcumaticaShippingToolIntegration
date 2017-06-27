using System;
using System.Collections.Generic;
using System.Linq;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Common;
using SmartShipment.Settings;

namespace SmartShipment.Network
{
    public class AcumaticaUriParser : IAcumaticaUriParser
    {
        private readonly ISettings _settings;

        public AcumaticaUriParser(ISettings settings)
        {
            _settings = settings;
        }

        public ParsedShipmentData GetShipmentId(string uriString)
        {
            if (!string.IsNullOrEmpty(uriString))
            {
                try
                {
                    if (!uriString.StartsWith("http") && !uriString.StartsWith("https"))
                    {
                        uriString = "http://" + uriString;
                    }

                    var uri = new Uri(uriString);
                    var baseUrl = DecodeBaseUrl(uri);
                    var queryData = DecodeQueryParameters(uri);
                    if (queryData.ContainsKey(ParsedShipmentData.SHIPMENT_NBR_PARAM) && (queryData.ContainsKey(ParsedShipmentData.SCREEN_ID_PARAM) || queryData.ContainsKey(ParsedShipmentData.SCREEN_POPUP_ON)))
                    {
                        return new ParsedShipmentData(_settings, baseUrl, queryData);
                    }
                }
                catch
                {
                    throw new UriFormatException(string.Format(InformationResources.ERROR_URI_STRING_PARSE, uriString));
                }
            }

            return null;
        }

        private static string DecodeBaseUrl(Uri uri)
        {
            var baseUri = uri.GetLeftPart(UriPartial.Path);                        
            return baseUri.Substring(0, baseUri.LastIndexOf('/'));
        }

        private Dictionary<string, string> DecodeQueryParameters(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (uri.Query.Length == 0)
                return new Dictionary<string, string>();

            return uri.Query.TrimStart('?')
                            .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                            .GroupBy(parts => parts[0],
                                     parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""))
                            .ToDictionary(grouping => grouping.Key,
                                          grouping => string.Join(",", grouping));
        }
    }
}