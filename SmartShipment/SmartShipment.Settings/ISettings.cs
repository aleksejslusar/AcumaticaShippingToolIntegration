using System.ComponentModel;

namespace SmartShipment.Settings
{
    public interface ISettings : INotifyPropertyChanged
    {
        string UpsProcessName { get; set; }        
        string UpsExportFilePath { get; set; }
        string FedexProcessName { get; set; }
        string FedexExportFilePath { get; set; }
        string AcumaticaLogin { get; set; }
        string AcumaticaPassword { get; set; }
        string AcumaticaCompany { get; set; }
        bool AcumaticaConfirmShipment { get; set; }
        bool UpsAddUpdateAddressBook { get; set; }
        bool FedexAddUpdateAddressBook { get; set; }
        string AcumaticaSoapDataEndpoint { get; set; }
        string AcumaticaSoapLoginEndpoint { get; set; }
        string AcumaticaBaseUrl { get; set; }
        string AcumaticaDefaultBoxId { get; set; }
        int GeneralApplicationStartPositionTop { get; set; }
        int GeneralApplicationStartPositionLeft { get; set; }

        void Save();
        bool IsSettingsRequireSetValues();
        void Reload();
        bool Validate();
    }
}