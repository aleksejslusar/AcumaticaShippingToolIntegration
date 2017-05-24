using System.Collections;
using System.Configuration;

namespace SmartShipment.Settings.SettingsHelper
{
    public interface ISmartShipmentsSettingsHelper
    {
        void InitializeIniFile();
        void ClearIniFile();
        void SetOrUpdateProvertyValues(IEnumerable properties, SmartShipmentsSettingsHelper.KeyUpdateMode mode);
        string GetValue(string valueName);
        string GetValue(SettingsProperty property);
        void Reset();
        void SaveIniFile();
        void InitDefaultSection(string sectionName, string sectionComment);
        void InitDefaultValue(string propertyName, string serializedValue);
        string UserHomeDirectory { get; }
        string ApplicationBasePath { get; }
        string ApplicationSettingsDirectoryPath { get; }
        string ApplicationName { get; set; }
        string ApplicationSettingsFilePath { get; }
    }
}