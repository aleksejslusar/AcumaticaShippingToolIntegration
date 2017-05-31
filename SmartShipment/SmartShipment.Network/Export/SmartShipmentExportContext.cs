using System.Collections.Generic;
using System.Linq;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Common;
using SmartShipment.Settings;


namespace SmartShipment.Network.Export
{
    public class SmartShipmentExportContext : ISmartShipmentExportContext
    {
        private readonly ISettings _settings;
        private readonly IAcumaticaNetworkProvider _acumaticaNetworkProvider;
        private readonly List<Shipment> _shipments;

        public SmartShipmentExportContext(ISettings settings, IAcumaticaNetworkProvider acumaticaNetworkProvider)
        {
            _settings = settings;
            _acumaticaNetworkProvider = acumaticaNetworkProvider;
            _shipments = new List<Shipment>();
        }

        public SmartShipmentExportContext ExportData(IEnumerable<ShipmentFileExportRow> shipmentFileExportRows)
        {
            //* shipmentGroupedData.Key => shipment number  
            //* shipmentGroupedData.Value => last shipment packages group
            var shipmentGroupedData = shipmentFileExportRows.GroupBy(s => s.ShipmentNbr)
                                                        .Where(s => !string.IsNullOrEmpty(s.Key))
                                                        .ToDictionary(g => g.Key, g => g.GroupBy(t => t.ShipmentTrackingNumber).LastOrDefault())
                                                        .ToDictionary(s => s.Key, s => s.Value);

            foreach (var shipmentData in shipmentGroupedData)
            {
                var shipment = GetShipmentFromGroupedData(shipmentData);
                shipment.Packages = GetPackagesFromGroupedData(shipmentData, _settings.AcumaticaDefaultBoxId);
                _shipments.Add(shipment);
            }
           
            return this;
        }

        public SmartShipmentExportContext UpdateAcumatica()
        {
            _acumaticaNetworkProvider.UpdateShipments(_shipments, this);
            _shipments.Clear();
            return this;
        }

        public void MapTargetToExportedShipment(Shipment targetShipment, Shipment sourceShipment)
        {
            //Shipment
            targetShipment.ShipmentDate.Value = sourceShipment.ShipmentDate.Value;
            targetShipment.Hold.Value = sourceShipment.Hold.Value;
            var newCustomFields = new List<CustomField>();
            foreach (var customField in sourceShipment.CustomFields)
            {
                var shipmentCustomField = targetShipment.CustomFields.FirstOrDefault(c => c.viewName == customField.viewName && c.fieldName == customField.fieldName);
                if (shipmentCustomField != null)
                {
                    ((CustomDecimalField) shipmentCustomField).Value = ((CustomDecimalField) customField).Value;
                    newCustomFields.Add(shipmentCustomField);
                }
                else
                {
                    newCustomFields.Add(customField);
                }               
            }

            targetShipment.CustomFields = newCustomFields.ToArray();

            //Shipment Packages
            var newPackages = new List<ShipmentPackage>();
            foreach (var sourcePackage in sourceShipment.Packages)
            {
                if (sourcePackage.RowNumber.Value.HasValue)
                {
                    var package = targetShipment.Packages.FirstOrDefault(p => p.RowNumber.Value == sourcePackage.RowNumber.Value);
                    if (package != null)
                    {
                        package.TrackingNumber.Value = sourcePackage.TrackingNumber.Value;
                        package.Weight.Value = sourcePackage.Weight.Value;
                        package.Confirmed.Value = sourcePackage.Confirmed.Value;
                        newPackages.Add(package);
                    }
                    else
                    {
                        newPackages.Add(sourcePackage);
                    }
                }
            }

            targetShipment.Packages = newPackages.ToArray();
        }

        private Shipment GetShipmentFromGroupedData(KeyValuePair<string, IGrouping<string, ShipmentFileExportRow>> shipmentData)
        {
            var shipment = new Shipment();
            if (shipmentData.Value.Any())
            {
                var totalFreighCost = shipmentData.Value.Where(p => !p.VoidIndicator).Sum(p => p.FreightCost);
                var shipmentDate = shipmentData.Value.OrderBy(p => p.RowNumber).Last().ShipmentDate;

                shipment.ShipmentNbr = new StringValue { Value = shipmentData.Key };
                shipment.ShipmentDate = new DateTimeValue { Value = shipmentDate };
                shipment.CustomFields = new CustomField[]
                {
                        new CustomDecimalField
                        {
                            viewName = "CurrentDocument",
                            fieldName = "CuryFreightCost",
                            Value = new DecimalValue {Value = totalFreighCost}
                        }
                };
                shipment.Hold = new BooleanValue { Value = shipmentData.Value.All(p => p.VoidIndicator) };
                shipment.Packages = new ShipmentPackage[shipmentData.Value.Count()];
            }
            return shipment;
        }

        private ShipmentPackage[] GetPackagesFromGroupedData(KeyValuePair<string, IGrouping<string, ShipmentFileExportRow>> shipmentData, string acumaticaDefaultBoxId)
        {
            var packages = new List<ShipmentPackage>();

            foreach (var shipmentDataPackage in shipmentData.Value)
            {
                var shipmentPackage = new ShipmentPackage
                {
                    BoxID = new StringValue { Value = acumaticaDefaultBoxId },
                    RowNumber = new LongValue { Value = shipmentDataPackage.RowNumber > 0 ? shipmentDataPackage.RowNumber : packages.Count + 1 },
                    Confirmed = new BooleanReturn { Value = !shipmentDataPackage.VoidIndicator },
                    TrackingNumber = new StringValue { Value = !shipmentDataPackage.VoidIndicator ? shipmentDataPackage.PackageTrackingNumber : string.Empty },
                    Weight = new DecimalValue { Value = shipmentDataPackage.PackageWeight }
                };

                packages.Add(shipmentPackage);
            }

            return packages.ToArray();
        }
       
    }
}
