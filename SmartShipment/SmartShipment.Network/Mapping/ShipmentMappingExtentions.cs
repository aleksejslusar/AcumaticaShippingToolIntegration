using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SmartShipment.Network.AcumaticaSoapService;

namespace SmartShipment.Network.Mapping
{
    public static class ShipmentMappingExtentions
    {
        private static Dictionary<string, string> _countries;

        static ShipmentMappingExtentions()
        {
            InitCountries();
        }

        public static ShipmentMapper MapShipment(this ShipmentMapper shipmentMapper, Shipment shipment)
        {
            return shipmentMapper.MapShipmentMapperToShipment(shipment);
        }

        public static ShipmentMapper MapShipment(this ShipmentMapper shipmentMapper, string shipment)
        {
            return JsonConvert.DeserializeObject<ShipmentMapper>(shipment, new JsonSerializerSettings() { Culture = CultureInfo.CurrentCulture });            
        }

        public static string GetConuntryName(this string countryKey)
        {
            string countryName;
            _countries.TryGetValue(countryKey, out countryName);
            
            if (!string.IsNullOrEmpty(countryName))
            {
                countryName = countryName.Trim();
                return countryName.IndexOfAny(new [] {','}) > 0 ? countryName.Split(',')[0] : countryName;
            }
            return countryName;
        }

        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        private static ShipmentMapper MapShipmentMapperToShipment(this ShipmentMapper shipmentMapper, Shipment shipment)
        {
            //Shipment
            shipmentMapper.Id = shipment.ID.GetValueOrDefault();
            shipmentMapper.RowNumber = shipment.RowNumber?.Value;
            shipmentMapper.Note = shipment.Note;
            shipmentMapper.CustomerId.Value = shipment.CustomerID?.Value;
            shipmentMapper.ToWarehouseId.Value = shipment.ToWarehouseID?.Value;
            shipmentMapper.ShipmentDate.Value = shipment.ShipmentDate?.Value;
            shipmentMapper.ShipmentNbr.Value = shipment.ShipmentNbr?.Value;
            shipmentMapper.WarehouseId.Value = shipment.WarehouseID?.Value;
            shipmentMapper.Status.Value = shipment.Status?.Value;
            shipmentMapper.ShipVia.Value = shipment.ShipVia?.Value;
            shipmentMapper.Hold.Value = shipment.Hold?.Value;
            shipmentMapper.Operation.Value = shipment.Operation?.Value;
            shipmentMapper.Type.Value = shipment.Type?.Value;
            shipmentMapper.Workgroup.Value = shipment.Workgroup?.Value;
            shipmentMapper.ShippedQuantity.Value = shipment.ShippedQuantity?.Value;
            shipmentMapper.ResidentialDelivery.Value = shipment.ResidentialDelivery?.Value;
            shipmentMapper.SaturdayDelivery.Value = shipment.SaturdayDelivery?.Value;
            shipmentMapper.UseCustomersAccount.Value = shipment.UseCustomersAccount?.Value;
            shipmentMapper.OverrideAddress.Value = shipment.OverrideAddress?.Value;
            shipmentMapper.OverrideContact.Value = shipment.OverrideContact?.Value;
            //Shipment->Contact
            shipmentMapper.Contact.Id = shipment.Contact?.ID;
            shipmentMapper.Contact.Note = shipment.Contact?.Note;
            shipmentMapper.Contact.RowNumber = shipment.Contact?.RowNumber?.Value;
            shipmentMapper.Contact.ContactId.Value = shipment.Contact?.ContactID?.Value;
            shipmentMapper.Contact.DisplayName.Value = shipment.Contact?.DisplayName?.Value;
            shipmentMapper.Contact.Position.Value = shipment.Contact?.Position?.Value;
            shipmentMapper.Contact.Email.Value = shipment.Contact?.Email?.Value;
            shipmentMapper.Contact.Phone1.Value = shipment.Contact?.Phone1?.Value;
            shipmentMapper.Contact.Fax.Value = shipment.Contact?.Fax?.Value;
            shipmentMapper.Contact.Phone2.Value = shipment.Contact?.Phone2?.Value;
            shipmentMapper.Contact.Web.Value = shipment.Contact?.Web?.Value;
            shipmentMapper.Contact.FirstName.Value = shipment.Contact?.FirstName?.Value;
            shipmentMapper.Contact.MiddleName.Value = shipment.Contact?.MiddleName?.Value;
            shipmentMapper.Contact.LastName.Value = shipment.Contact?.LastName?.Value;
            shipmentMapper.Contact.Title.Value = shipment.Contact?.Title?.Value;
            shipmentMapper.Contact.Phone1Type.Value = shipment.Contact?.Phone1Type?.Value;
            shipmentMapper.Contact.Phone2Type.Value = shipment.Contact?.Phone2Type?.Value;
            //Shipment->Contact->Address
            shipmentMapper.Contact.Address.Id = shipment.Contact?.Address?.ID;
            shipmentMapper.Contact.Address.Note = shipment.Contact?.Address?.Note;
            shipmentMapper.Contact.Address.RowNumber = shipment.Contact?.Address?.RowNumber?.Value;
            shipmentMapper.Contact.Address.AddressLine1.Value = shipment.Contact?.Address?.AddressLine1?.Value;
            shipmentMapper.Contact.Address.AddressLine2.Value = shipment.Contact?.Address?.AddressLine2?.Value;
            shipmentMapper.Contact.Address.City.Value = shipment.Contact?.Address?.City?.Value;
            shipmentMapper.Contact.Address.Country.Value = shipment.Contact?.Address?.Country?.Value;
            shipmentMapper.Contact.Address.CountryName.Value = UppercaseWords(shipmentMapper.Contact.Address.Country.Value.GetConuntryName().ToLower());
            shipmentMapper.Contact.Address.PostalCode.Value = shipment.Contact?.Address?.PostalCode?.Value;
            shipmentMapper.Contact.Address.State.Value = shipment.Contact?.Address?.State?.Value;            

            //Shipment->Packages
            foreach (var package in shipment.Packages)
            {
                var shipmentPackage = new ShipmentPackage
                {
                    Id = package.ID.GetValueOrDefault(),
                    RowNumber = package.RowNumber?.Value,
                    Note = package.Note,
                    BoxId = { Value = package.BoxID?.Value },
                    CodAmount = { Value = package.CODAmount?.Value },
                    Confirmed = { Value = package.Confirmed?.Value },
                    CustomRefNbr1 = { Value = package.CustomRefNbr1?.Value },
                    CustomRefNbr2 = { Value = package.CustomRefNbr2?.Value },
                    DeclaredValue = { Value = package.DeclaredValue?.Value },
                    Description = { Value = package.Description?.Value },
                    Type = { Value = package.Type?.Value },
                    TrackingNumber = { Value = package.TrackingNumber?.Value },
                    Weight = { Value = package.Weight?.Value },
                    Uom = { Value = package.UOM?.Value },
                    PackageWeight = { Value = package.Weight?.Value.ToString() },
                    PackageRowNumberOne = { Value = shipment.ShipmentNbr?.Value },
                    PackageRowNumberTwo = { Value = package.RowNumber?.Value.ToString() }
                };

                decimal weight;
                if (decimal.TryParse(shipmentPackage.PackageWeight.Value, out weight))
                {
                    shipmentPackage.PackageFormattedWeight.Value = decimal.Round(weight, 1).ToString(CultureInfo.CurrentCulture);
                }

                shipmentMapper.Packages.Add(shipmentPackage);
            }


            //Shipment->Details
            foreach (var detail in shipment.Details)
            {
                var shipmentDetail = new ShipmentDetail
                {
                    Id = detail.ID.GetValueOrDefault(),
                    RowNumber = detail.RowNumber?.Value,
                    Note = detail.Note,
                    InventoryId = { Value = detail.InventoryID?.Value },
                    LineNbr = { Value = detail.LineNbr?.Value },
                    LocationId = { Value = detail.LocationID?.Value },
                    OpenQty = { Value = detail.OpenQty?.Value },
                    OrderLineNbr = { Value = detail.OrderLineNbr?.Value },
                    OrderNbr = { Value = detail.OrderNbr?.Value },
                    OrderedQty = { Value = detail.OrderedQty?.Value },
                    ReasonCode = { Value = detail.ReasonCode?.Value },
                    ShippedQty = { Value = detail.ShippedQty?.Value },
                    WarehouseId = { Value = detail.WarehouseID?.Value },
                    Description = { Value = detail.Description?.Value },
                    Uom = { Value = detail.UOM?.Value },
                    OrderType = { Value = detail.OrderType?.Value },
                    ExpirationDate = { Value = detail.ExpirationDate?.Value },
                    FreeItem = { Value = detail.FreeItem?.Value },
                    LotSerialNbr = { Value = detail.LotSerialNbr?.Value }
                };


                foreach (var allocation in detail.Allocations)
                {
                    var shipmentAllocation = new ShipmentDetailAllocation
                    {
                        Id = allocation.ID.GetValueOrDefault(),
                        RowNumber = allocation.RowNumber?.Value,
                        Note = allocation.Note,
                        LocationId = { Value = allocation.LocationID?.Value },
                        LotSerialNbr = { Value = allocation.LotSerialNbr?.Value },
                        Qty = { Value = allocation.Qty?.Value },
                        Uom = { Value = allocation.UOM?.Value },
                        ExpirationDate = { Value = allocation.ExpirationDate?.Value },
                        Description = { Value = allocation.Description?.Value }
                    };

                    shipmentDetail.Allocations.Add(shipmentAllocation);
                }

                shipmentMapper.Details.Add(shipmentDetail);
            }

            return shipmentMapper;
        }

        private static void InitCountries()
        {
            _countries = new Dictionary<string, string>
            {
               {"AD","ANDORRA"},
               {"AE"," UNITED ARAB EMIRATES"},
               {"AF"," AFGHANISTAN"},
               {"AG"," ANTIGUA AND BARBUDA"},
               {"AI"," ANGUILLA"},
               {"AL"," ALBANIA"},
               {"AM"," ARMENIA"},
               {"AN"," NETHERLANDS ANTILLES"},
               {"AO"," ANGOLA"},
               {"AQ"," ANTARCTICA"},
               {"AR"," ARGENTINA"},
               {"AS"," AMERICAN SAMOA"},
               {"AT"," AUSTRIA"},
               {"AU"," AUSTRALIA"},
               {"AW"," ARUBA"},
               {"AX"," ÅLAND ISLANDS"},
               {"AZ"," AZERBAIJAN"},
               {"BA"," BOSNIA AND HERZEGOVINA"},
               {"BB"," BARBADOS"},
               {"BD"," BANGLADESH"},
               {"BE"," BELGIUM"},
               {"BF"," BURKINA FASO"},
               {"BG"," BULGARIA"},
               {"BH"," BAHRAIN"},
               {"BI"," BURUNDI"},
               {"BJ"," BENIN"},
               {"BL"," SAINT BARTHÉLEMY"},
               {"BM"," BERMUDA"},
               {"BN"," BRUNEI DARUSSALAM"},
               {"BO"," BOLIVIA"},
               {"BQ"," BONAIRE, SINT EUSTATIUS AND SABA"},
               {"BR"," BRAZIL"},
               {"BS"," BAHAMAS"},
               {"BT"," BHUTAN"},
               {"BV"," BOUVET ISLAND"},
               {"BW"," BOTSWANA"},
               {"BY"," BELARUS"},
               {"BZ"," BELIZE"},
               {"CA"," CANADA"},
               {"CC"," COCOS (KEELING) ISLANDS"},
               {"CD"," CONGO, THE DEMOCRATIC REPUBLIC"},
               {"CF"," CENTRAL AFRICAN REPUBLIC"},
               {"CG"," CONGO"},
               {"CH"," SWITZERLAND"},
               {"CI"," CÔTE D'IVOIRE"},
               {"CK"," COOK ISLANDS"},
               {"CL"," CHILE"},
               {"CM"," CAMEROON"},
               {"CN"," CHINA"},
               {"CO"," COLOMBIA"},
               {"CR"," COSTA RICA"},
               {"CU"," CUBA"},
               {"CV"," CAPE VERDE"},
               {"CW"," CURAÇAO"},
               {"CX"," CHRISTMAS ISLAND"},
               {"CY"," CYPRUS"},
               {"CZ"," CZECH REPUBLIC"},
               {"DE"," GERMANY"},
               {"DJ"," DJIBOUTI"},
               {"DK"," DENMARK"},
               {"DM"," DOMINICA"},
               {"DO"," DOMINICAN REPUBLIC"},
               {"DZ"," ALGERIA"},
               {"EC"," ECUADOR"},
               {"EE"," ESTONIA"},
               {"EG"," EGYPT"},
               {"EH"," WESTERN SAHARA"},
               {"ER"," ERITREA"},
               {"ES"," SPAIN"},
               {"ET"," ETHIOPIA"},
               {"FI"," FINLAND"},
               {"FJ"," FIJI"},
               {"FK"," FALKLAND ISLANDS (MALVINAS)"},
               {"FM"," MICRONESIA, FEDERATED STATES O"},
               {"FO"," FAROE ISLANDS"},
               {"FR"," FRANCE"},
               {"GA"," GABON"},
               {"GB"," UNITED KINGDOM"},
               {"GD"," GRENADA"},
               {"GE"," GEORGIA"},
               {"GF"," FRENCH GUIANA"},
               {"GG"," GUERNSEY"},
               {"GH"," GHANA"},
               {"GI"," GIBRALTAR"},
               {"GL"," GREENLAND"},
               {"GM"," GAMBIA"},
               {"GN"," GUINEA"},
               {"GP"," GUADELOUPE"},
               {"GQ"," EQUATORIAL GUINEA"},
               {"GR"," GREECE"},
               {"GS"," SOUTH GEORGIA AND THE SOUTH SA"},
               {"GT"," GUATEMALA"},
               {"GU"," GUAM"},
               {"GW"," GUINEA-BISSAU"},
               {"GY"," GUYANA"},
               {"HK"," HONG KONG"},
               {"HM"," HEARD ISLAND AND MCDONALD ISLA"},
               {"HN"," HONDURAS"},
               {"HR"," CROATIA"},
               {"HT"," HAITI"},
               {"HU"," HUNGARY"},
               {"ID"," INDONESIA"},
               {"IE"," IRELAND"},
               {"IL"," ISRAEL"},
               {"IM"," ISLE OF MAN"},
               {"IN"," INDIA"},
               {"IO"," BRITISH INDIAN OCEAN TERRITORY"},
               {"IQ"," IRAQ"},
               {"IR"," IRAN, ISLAMIC REPUBLIC OF"},
               {"IS"," ICELAND"},
               {"IT"," ITALY"},
               {"JE"," JERSEY"},
               {"JM"," JAMAICA"},
               {"JO"," JORDAN"},
               {"JP"," JAPAN"},
               {"KE"," KENYA"},
               {"KG"," KYRGYZSTAN"},
               {"KH"," CAMBODIA"},
               {"KI"," KIRIBATI"},
               {"KM"," COMOROS"},
               {"KN"," SAINT KITTS AND NEVIS"},
               {"KP"," KOREA, DEMOCRATIC PEOPLE'S REP"},
               {"KR"," KOREA, REPUBLIC OF"},
               {"KW"," KUWAIT"},
               {"KY"," CAYMAN ISLANDS"},
               {"KZ"," KAZAKHSTAN"},
               {"LA"," LAO PEOPLE'S DEMOCRATIC REPUBL"},
               {"LB"," LEBANON"},
               {"LC"," SAINT LUCIA"},
               {"LI"," LIECHTENSTEIN"},
               {"LK"," SRI LANKA"},
               {"LR"," LIBERIA"},
               {"LS"," LESOTHO"},
               {"LT"," LITHUANIA"},
               {"LU"," LUXEMBOURG"},
               {"LV"," LATVIA"},
               {"LY"," LIBYAN ARAB JAMAHIRIYA"},
               {"MA"," MOROCCO"},
               {"MC"," MONACO"},
               {"MD"," MOLDOVA, REPUBLIC OF"},
               {"ME"," MONTENEGRO"},
               {"MF"," SAINT MARTIN"},
               {"MG"," MADAGASCAR"},
               {"MH"," MARSHALL ISLANDS"},
               {"MK"," MACEDONIA, THE FORMER YUGOSLAV"},
               {"ML"," MALI"},
               {"MM"," MYANMAR"},
               {"MN"," MONGOLIA"},
               {"MO"," MACAO"},
               {"MP"," NORTHERN MARIANA ISLANDS"},
               {"MQ"," MARTINIQUE"},
               {"MR"," MAURITANIA"},
               {"MS"," MONTSERRAT"},
               {"MT"," MALTA"},
               {"MU"," MAURITIUS"},
               {"MV"," MALDIVES"},
               {"MW"," MALAWI"},
               {"MX"," MEXICO"},
               {"MY"," MALAYSIA"},
               {"MZ"," MOZAMBIQUE"},
               {"NA"," NAMIBIA"},
               {"NC"," NEW CALEDONIA"},
               {"NE"," NIGER"},
               {"NF"," NORFOLK ISLAND"},
               {"NG"," NIGERIA"},
               {"NI"," NICARAGUA"},
               {"NL"," NETHERLANDS"},
               {"NO"," NORWAY"},                 
               {"NP"," NEPAL"},
               {"NR"," NAURU"},
               {"NU"," NIUE"},
               {"NZ"," NEW ZEALAND"},
               {"OM"," OMAN"},
               {"PA"," PANAMA"},
               {"PE"," PERU"},
               {"PF"," FRENCH POLYNESIA"},
               {"PG"," PAPUA NEW GUINEA"},
               {"PH"," PHILIPPINES"},
               {"PK"," PAKISTAN"},
               {"PL"," POLAND"},
               {"PM"," SAINT PIERRE AND MIQUELON"},
               {"PN"," PITCAIRN"},
               {"PR"," PUERTO RICO"},
               {"PS"," PALESTINIAN TERRITORY, OCCUPIE"},
               {"PT"," PORTUGAL"},
               {"PW"," PALAU"},
               {"PY"," PARAGUAY"},
               {"QA"," QATAR"},
               {"RE"," RÉUNION"},
               {"RO"," ROMANIA"},
               {"RS"," SERBIA"},
               {"RU"," RUSSIAN FEDERATION"},
               {"RW"," RWANDA"},
               {"SA"," SAUDI ARABIA"},
               {"SB"," SOLOMON ISLANDS"},
               {"SC"," SEYCHELLES"},
               {"SD"," SUDAN"},
               {"SE"," SWEDEN"},
               {"SG"," SINGAPORE"},
               {"SH"," SAINT HELENA"},
               {"SI"," SLOVENIA"},
               {"SJ"," SVALBARD AND JAN MAYEN"},
               {"SK"," SLOVAKIA"},
               {"SL"," SIERRA LEONE"},
               {"SM"," SAN MARINO"},
               {"SN"," SENEGAL"},
               {"SO"," SOMALIA"},
               {"SR"," SURINAME"},
               {"SS"," SOUTH SUDAN"},
               {"ST"," SAO TOME AND PRINCIPE"},
               {"SV"," EL SALVADOR"},
               {"SX"," SINT MAARTEN (DUTCH PART)"},
               {"SY"," SYRIAN ARAB REPUBLIC"},
               {"SZ"," SWAZILAND"},
               {"TC"," TURKS AND CAICOS ISLANDS"},
               {"TD"," CHAD"},
               {"TF"," FRENCH SOUTHERN TERRITORIES"},
               {"TG"," TOGO"},
               {"TH"," THAILAND"},
               {"TJ"," TAJIKISTAN"},
               {"TK"," TOKELAU"},
               {"TL"," TIMOR-LESTE"},
               {"TM"," TURKMENISTAN"},
               {"TN"," TUNISIA"},
               {"TO"," TONGA"},
               {"TR"," TURKEY"},
               {"TT"," TRINIDAD AND TOBAGO"},
               {"TV"," TUVALU"},
               {"TW"," TAIWAN"},
               {"TZ"," TANZANIA, UNITED REPUBLIC OF"},
               {"UA"," UKRAINE"},
               {"UG"," UGANDA"},
               {"UM"," UNITED STATES MINOR OUTLYING I"},
               {"US"," UNITED STATES"},
               {"UY"," URUGUAY"},
               {"UZ"," UZBEKISTAN"},
               {"VA"," HOLY SEE (VATICAN CITY STATE)"},
               {"VC"," SAINT VINCENT AND THE GRENADIN"},
               {"VE"," VENEZUELA"},
               {"VG"," VIRGIN ISLANDS, BRITISH"},
               {"VI"," VIRGIN ISLANDS, U.S."},
               {"VN"," VIETNAM"},
               {"VU"," VANUATU"},
               {"WF"," WALLIS AND FUTUNA"},
               {"WS"," SAMOA"},
               {"YE"," YEMEN"},
               {"YT"," MAYOTTE"},
               {"ZA"," SOUTH AFRICA"},
               {"ZM"," ZAMBIA"},
               {"ZW"," ZIMBABWE"},
            };
        }       
    }
}