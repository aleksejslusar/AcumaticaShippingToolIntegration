using System.Collections.Specialized;
using System.Configuration;
using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.Settings.SettingsProvider
{
    public sealed class SmartShipmentSettingsProvider : System.Configuration.SettingsProvider, IApplicationSettingsProvider
    {
        private readonly ISmartShipmentsSettingsHelper _settingsProviderHelper;

        public SmartShipmentSettingsProvider()
        {           
            _settingsProviderHelper = new SmartShipmentsSettingsHelper();
        }

        #region SettingsProvider

        public override string ApplicationName
        {
            get { return _settingsProviderHelper.ApplicationName; }

            set { _settingsProviderHelper.ApplicationName = value; }
        }

        public override string Name => SmartShipmentsSettingsHelper.CLASS_NAME;

        public override void Initialize(string name, NameValueCollection config)
        {
            _settingsProviderHelper.InitializeIniFile();
            _settingsProviderHelper.ClearIniFile();
            base.Initialize(Name, config);
        }
        
        #endregion

        #region IApplicationSettingsProvider

        public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
        {
            return new SettingsPropertyValue(property);
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            var values = new SettingsPropertyValueCollection();

            foreach (SettingsProperty property in collection)
            {
                values.Add(new SettingsPropertyValue(property)
                {
                    SerializedValue = _settingsProviderHelper.GetValue(property)
                });
            }

            return values;
        }

        public void Reset(SettingsContext context)
        {
            _settingsProviderHelper.Reset();
            _settingsProviderHelper.SaveIniFile();            
        }       

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection properties)
        {
            _settingsProviderHelper.SetOrUpdateProvertyValues(properties, SmartShipmentsSettingsHelper.KeyUpdateMode.SetKeyValue);
        }

        public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
        {
            _settingsProviderHelper.SetOrUpdateProvertyValues(properties, SmartShipmentsSettingsHelper.KeyUpdateMode.UpdateKeyValue);
        }        

        #endregion        
        
    }    
}
