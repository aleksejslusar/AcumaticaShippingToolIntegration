using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using IniParser;
using IniParser.Model;
using SmartShipment.Information.Exceptions;
using SmartShipment.Information.Properties;

namespace SmartShipment.Settings.SettingsHelper
{
    public class SmartShipmentsSettingsHelper : ISmartShipmentsSettingsHelper
    {
        public const string CLASS_NAME = "SmartShipmentSettingsProvider";

        private const string APPLICATION_PARENT_PATH = "Sprinterra";
        private const string APPLICATION_BASE_PATH = "Sprinterra\\Acumatica Shipping Tool Integration";
        private const string APPLICATION_CONFIGURATION_PATH = APPLICATION_BASE_PATH +  "\\Configuration";

        public const string SECTION_GENERAL = "General";
        public const string SECTION_UPS = "UPS";
        public const string SECTION_FEDEX = "FedEx";
        public const string SECTION_ACUMATICA = "Acumatica";


        private IniData _iniData;
        private FileIniDataParser _iniParser;

        public string UserHomeDirectory { get; }
        public string ProgramDataPath { get; }
        public string ApplicationParentPath { get; }
        public string ApplicationBasePath { get; }
        public string ApplicationSettingsDirectoryPath { get; }
        public string ApplicationName { get; set; }
        public string ApplicationSettingsFilePath { get; }

        public SmartShipmentsSettingsHelper()
        {
            ProgramDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            UserHomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ApplicationParentPath = Path.Combine(UserHomeDirectory, APPLICATION_PARENT_PATH);
            ApplicationBasePath = Path.Combine(UserHomeDirectory, APPLICATION_BASE_PATH);
            ApplicationSettingsDirectoryPath = Path.Combine(UserHomeDirectory, APPLICATION_CONFIGURATION_PATH);
            ApplicationName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            ApplicationSettingsFilePath = Path.Combine(ApplicationSettingsDirectoryPath, $"{ApplicationName}.ini");
            
        }
        
        public void InitializeIniFile()
        {
            if (!Directory.Exists(ApplicationSettingsDirectoryPath))
            {
                throw new SettingsException(InformationResources.ERROR_APPLICATION_SETTINGS_DIRECTORY_ACCESS + " " + ApplicationSettingsDirectoryPath);
            }

            if (!File.Exists(ApplicationSettingsFilePath))
            {
                throw new SettingsException(InformationResources.ERROR_APPLICATION_SETTINGS_FILE_ACCESS + " " + ApplicationSettingsFilePath);
            }

            _iniParser = new FileIniDataParser();
            _iniData = _iniParser.ReadFile(ApplicationSettingsFilePath);

        }

        public void ClearIniFile()
        {
            var isFileModified = false;
            foreach (var section in _iniData.Sections.ToArray())
            {
                foreach (var key in section.Keys.Where(key => string.IsNullOrEmpty(key.Value) || key.Value.StartsWith("\"")).ToArray())
                {
                    section.Keys.RemoveKey(key.KeyName);
                    isFileModified = true;
                }
            }

            if (isFileModified)
            {
                SaveIniFile();
            }
        }

        public void SetOrUpdateProvertyValues(IEnumerable properties, KeyUpdateMode mode)
        {
            var errorMessage = mode == KeyUpdateMode.SetKeyValue ? InformationResources.ERROR_APPLICATION_SETTINGS_SET_OR_SAVE : InformationResources.ERROR_APPLICATION_SETTINGS_UPDATE;
            foreach (var property in properties.Cast<SettingsPropertyValue>().Where(property => property.IsDirty))
            {
                SetOrUpdateKeyValue(property, mode);
            }

            try
            {
                SaveIniFile();
            }
            catch
            {
                throw new SettingsException(errorMessage);
            }
        }

        public string GetValue(string valueName)
        {
            return GetValue(new SettingsProperty(valueName));
        }

        public string GetValue(SettingsProperty property)
        {
            var sectionName = GetSectionNameByPropertyName(property.Name);
            var keyName = new StringBuilder();
            var propertyValue = string.Empty;
            if (sectionName != null)
            {
                keyName.Append(sectionName).Append(_iniData.SectionKeySeparator).Append(property.Name);
                _iniData.TryGetKey(keyName.ToString(), out propertyValue);
            }
            keyName.Clear();
            return propertyValue;


        }

        public void Reset()
        {
            foreach (var section in _iniData.Sections)
            {
                section.ClearKeyData();
                section.ClearComments();
            }
        }

        public void SaveIniFile()
        {
            _iniParser.WriteFile(ApplicationSettingsFilePath, _iniData);
        }

        private string GetSectionNameByPropertyName(string name)
        {
            if (name.StartsWith(SECTION_UPS, StringComparison.OrdinalIgnoreCase))
                return SECTION_UPS;

            if (name.StartsWith(SECTION_FEDEX, StringComparison.OrdinalIgnoreCase))
                return SECTION_FEDEX;

            if (name.StartsWith(SECTION_ACUMATICA, StringComparison.OrdinalIgnoreCase))
                return SECTION_ACUMATICA;

            if (name.StartsWith(SECTION_GENERAL, StringComparison.OrdinalIgnoreCase))
                return SECTION_GENERAL;

            return null;
        }

        private void SetOrUpdateKeyValue(SettingsPropertyValue property, KeyUpdateMode mode)
        {
            var keyName = property.Name;
            var keyData = new KeyData(keyName) { Value = property.SerializedValue.ToString() };

            var sectionName = GetSectionNameByPropertyName(property.Name);
            if (sectionName != null)
            {
                var section = _iniData.Sections.GetSectionData(sectionName);
                if (mode == KeyUpdateMode.SetKeyValue && !section.Keys.ContainsKey(property.Name))
                {
                    section.Keys.AddKey(keyName);
                }

                //Do not save empty values
                if (!string.IsNullOrEmpty(keyData.Value))
                {
                    section.Keys.SetKeyData(keyData);
                }
                else
                {
                    section.Keys.RemoveKey(keyName);
                }
            }
        }

        public void InitDefaultSection(string sectionName, string sectionComment)
        {
            if (!_iniData.Sections.ContainsSection(sectionName))
            {
                _iniData.Sections.AddSection(sectionName);
                _iniData.Sections.GetSectionData(sectionName).LeadingComments.Add(sectionComment);
            }
        }

        public void InitDefaultValue(string propertyName, string serializedValue)
        {
            var settingsProperty = new SettingsProperty(propertyName){PropertyType = typeof(string)};
            var settingsPropertyValue = new SettingsPropertyValue(settingsProperty) { SerializedValue = serializedValue };
            if (string.IsNullOrEmpty(GetValue(settingsProperty)))
            {
                SetOrUpdateKeyValue(settingsPropertyValue, KeyUpdateMode.SetKeyValue);
            }
        }


        public enum KeyUpdateMode
        {
            SetKeyValue,
            UpdateKeyValue
        }
    }
}