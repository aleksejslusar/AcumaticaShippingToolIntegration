using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Settings.SettingsHelper;
using SmartShipment.UI.Annotations;

namespace SmartShipment.Settings
{
    public class SmartShipmentSettings : ISettings
    {
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        private readonly Properties.Settings _settings = Properties.Settings.Default;

        public SmartShipmentSettings(ISmartShipmentMessagesProvider messagesProvider)
        {
            _messagesProvider = messagesProvider;
        }

        public string UpsProcessName
        {
            get { return _settings.UpsProcessName; }
            set
            {
                if (_settings.UpsProcessName != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(UpsProcessName), _settings.UpsProcessName, value));
                }
                _settings.UpsProcessName = value;
                OnPropertyChanged(nameof(UpsProcessName));                
            }
        }

        public string UpsExportFilePath
        {
            get { return _settings.UpsExportFilePath; }
            set
            {
                if (_settings.UpsExportFilePath != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(UpsExportFilePath), _settings.UpsExportFilePath, value));
                }
                _settings.UpsExportFilePath = value;
                OnPropertyChanged(nameof(UpsExportFilePath));
            }
        }
        
        public string FedexProcessName
        {
            get { return _settings.FedexProcessName; }
            set
            {
                if (_settings.FedexProcessName != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(FedexProcessName), _settings.FedexProcessName, value));
                }
                _settings.FedexProcessName = value;
                OnPropertyChanged(nameof(FedexProcessName));
            }
        }

        public string FedexExportFilePath
        {
            get { return _settings.FedexExportFilePath; }
            set
            {
                if (_settings.FedexExportFilePath != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(FedexExportFilePath), _settings.FedexExportFilePath, value));
                }
                _settings.FedexExportFilePath = value;
                OnPropertyChanged(nameof(FedexExportFilePath));
            }
        }

        public string AcumaticaLogin
        {
            get { return _settings.AcumaticaLogin; }
            set
            {
                if (_settings.AcumaticaLogin != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaLogin), _settings.AcumaticaLogin, value));
                }
                _settings.AcumaticaLogin = value;
                OnPropertyChanged(nameof(AcumaticaLogin));
            }
        }

        public string AcumaticaPassword
        {
            get { return SettingsSecureHelper.ToInsecureString(SettingsSecureHelper.DecryptString(_settings.AcumaticaPassword)); }
            set
            {               
                _settings.AcumaticaPassword = SettingsSecureHelper.EncryptString(SettingsSecureHelper.ToSecureString(value));
                OnPropertyChanged(nameof(AcumaticaPassword));
            }
        }

        public string AcumaticaCompany
        {
            get { return _settings.AcumaticaCompany; }
            set
            {
                if (_settings.AcumaticaCompany != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaCompany), _settings.AcumaticaCompany, value));
                }
                _settings.AcumaticaCompany = value;
                OnPropertyChanged(nameof(AcumaticaCompany));
            }
        }

        public bool AcumaticaConfirmShipment
        {
            get { return _settings.AcumaticaConfirmShipment; }
            set
            {
                if (_settings.AcumaticaConfirmShipment != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaConfirmShipment), _settings.AcumaticaConfirmShipment, value));
                }
                _settings.AcumaticaConfirmShipment = value;
                OnPropertyChanged(nameof(AcumaticaConfirmShipment));
            }
        }

        public bool UpsAddUpdateAddressBook
        {
            get { return _settings.UpsAddUpdateAddressBook; }
            set
            {
                if (_settings.UpsAddUpdateAddressBook != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(UpsAddUpdateAddressBook), _settings.UpsAddUpdateAddressBook, value));
                }
                _settings.UpsAddUpdateAddressBook = value;
                OnPropertyChanged(nameof(UpsAddUpdateAddressBook));
            }
        }

        public bool FedexAddUpdateAddressBook
        {
            get { return _settings.FedexAddUpdateAddressBook; }
            set
            {
                if (_settings.FedexAddUpdateAddressBook != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(FedexAddUpdateAddressBook), _settings.FedexAddUpdateAddressBook, value));
                }
                _settings.FedexAddUpdateAddressBook = value;
                OnPropertyChanged(nameof(FedexAddUpdateAddressBook));
            }
        }

        public string AcumaticaSoapDataEndpoint
        {
            get { return _settings.AcumaticaSoapDataEndPoint; }
            set
            {
                if (_settings.AcumaticaSoapDataEndPoint != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaSoapDataEndpoint), _settings.AcumaticaSoapDataEndPoint, value));
                }
                _settings.AcumaticaSoapDataEndPoint = value;
                OnPropertyChanged(nameof(AcumaticaSoapDataEndpoint));
            }
        }

        public string AcumaticaSoapLoginEndpoint
        {
            get { return _settings.AcumaticaSoapLoginEndPoint; }
            set
            {
                if (_settings.AcumaticaSoapLoginEndPoint != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaSoapLoginEndpoint), _settings.AcumaticaSoapLoginEndPoint, value));
                }
                _settings.AcumaticaSoapLoginEndPoint = value;
                OnPropertyChanged(nameof(AcumaticaSoapLoginEndpoint));
            }
        }

        public string AcumaticaBaseUrl
        {
            get { return _settings.AcumaticaBaseUrl; }
            set
            {
                if (_settings.AcumaticaBaseUrl != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaBaseUrl), _settings.AcumaticaBaseUrl, value));
                }
                _settings.AcumaticaBaseUrl = value;
                OnPropertyChanged(nameof(AcumaticaBaseUrl));
            }
        }

        public string AcumaticaDefaultBoxId 
        {
            get { return _settings.AcumaticaDefaultBoxId; }
            set
            {
                if (_settings.AcumaticaDefaultBoxId != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(AcumaticaDefaultBoxId), _settings.AcumaticaDefaultBoxId, value));
                }
                _settings.AcumaticaDefaultBoxId = value;
                OnPropertyChanged(nameof(AcumaticaDefaultBoxId));
            }
        }

        public int GeneralApplicationStartPositionTop
        {
            get { return _settings.GeneralApplicationStartPositionTop; }
            set
            {
                if (_settings.GeneralApplicationStartPositionTop != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(GeneralApplicationStartPositionTop), _settings.GeneralApplicationStartPositionTop, value));
                }
                _settings.GeneralApplicationStartPositionTop = value;
                OnPropertyChanged(nameof(GeneralApplicationStartPositionTop));
            }
        }

        public int GeneralApplicationStartPositionLeft
        {
            get { return _settings.GeneralApplicationStartPositionLeft; }
            set
            {
                if (_settings.GeneralApplicationStartPositionLeft != value)
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_PROPERTY_VALUE_CHANGED, nameof(GeneralApplicationStartPositionLeft), _settings.GeneralApplicationStartPositionLeft, value));
                }
                _settings.GeneralApplicationStartPositionLeft = value;
                OnPropertyChanged(nameof(GeneralApplicationStartPositionLeft));
            }
        }

        public void Save()
        {            
            _messagesProvider.Log(InformationResources.INFO_PROPERTIES_SAVED);
            _settings.Save();            
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(AcumaticaBaseUrl)  && 
                   !string.IsNullOrEmpty(AcumaticaLogin)    &&
                   !string.IsNullOrEmpty(AcumaticaPassword) && 
                   !string.IsNullOrEmpty(AcumaticaDefaultBoxId);
        }

        public void Reload()
        {
            _settings.Reload();
            OnPropertyChanged(nameof(UpsProcessName));
            OnPropertyChanged(nameof(UpsExportFilePath));
            OnPropertyChanged(nameof(FedexProcessName));
            OnPropertyChanged(nameof(FedexExportFilePath));
            OnPropertyChanged(nameof(AcumaticaLogin));
            OnPropertyChanged(nameof(AcumaticaPassword));
            OnPropertyChanged(nameof(AcumaticaCompany));
            OnPropertyChanged(nameof(AcumaticaConfirmShipment));
            OnPropertyChanged(nameof(UpsAddUpdateAddressBook));
            OnPropertyChanged(nameof(FedexAddUpdateAddressBook));
            OnPropertyChanged(nameof(AcumaticaSoapDataEndpoint));
            OnPropertyChanged(nameof(AcumaticaSoapLoginEndpoint));
            OnPropertyChanged(nameof(AcumaticaBaseUrl));
            OnPropertyChanged(nameof(AcumaticaDefaultBoxId));
            OnPropertyChanged(nameof(GeneralApplicationStartPositionTop));
            OnPropertyChanged(nameof(GeneralApplicationStartPositionLeft));
            OnPropertyChanged(nameof(AcumaticaPassword));
            OnPropertyChanged(nameof(AcumaticaLogin));
            OnPropertyChanged(nameof(AcumaticaBaseUrl));
            OnPropertyChanged(nameof(AcumaticaDefaultBoxId));
        }

        public bool IsSettingsRequireSetValues()
        {
            return string.IsNullOrEmpty(AcumaticaPassword) ||
                   string.IsNullOrEmpty(AcumaticaLogin) ||
                   string.IsNullOrEmpty(AcumaticaBaseUrl) ||
                   string.IsNullOrEmpty(AcumaticaDefaultBoxId);            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
