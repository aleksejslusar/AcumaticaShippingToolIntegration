using System;
using System.Windows.Forms;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Common;
using SmartShipment.Settings;
using SmartShipment.UI.Common;
using SmartShipment.UI.Views;

namespace SmartShipment.UI.Presenters
{
    public class SmartShipmentSettingDialogPresenter : BasePresenter<ISmartSettingsDialogView>
    {
        private readonly ISettings _settings;
        private readonly IAcumaticaNetworkProvider _acumaticaNetworkProvider;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        public SmartShipmentSettingDialogPresenter(IApplicationController controller, ISmartSettingsDialogView view,
            ISettings settings, IAcumaticaNetworkProvider acumaticaNetworkProvider, ISmartShipmentMessagesProvider messagesProvider) : base(controller, view)
        {
            _settings = settings;
            _acumaticaNetworkProvider = acumaticaNetworkProvider;
            _messagesProvider = messagesProvider;
            View.OnSettingsSave += () =>
            {
                if (!_settings.Validate())
                {
                    _settings.Reload();
                    _messagesProvider.Warn(InformationResources.WARN_PARAMETERS_ARE_NOT_SAVED);
                    View.Form.DialogResult = DialogResult.None;
                }
                _settings.Save();
            };
            View.OnSettingsCancel += () => _settings.Reload();
            View.OnTestLoginClick += View_OnTestLoginClick;
            View.OnFormLoad += () =>
            {               
                View.Form.BringToFront();
                View.TextAcumaticaBaseUrl.Focus();
            };

            BindControlsToDataSource(settings);
            SetDefaults(settings);
            View.SetFormAttributes();
        }

        private void View_OnTestLoginClick()
        {
            try
            {
                View.TestButton.Enabled = false;
                _acumaticaNetworkProvider.TestNetworkSettings();
                _messagesProvider.Info(InformationResources.INFO_CHECK_NETWORK_PARAMETERS);
            }
            catch (Exception e)
            {
                _messagesProvider.Warn(string.Format(InformationResources.ERROR_CHECK_NETWORK_PARAMETERS,
                    "Invalid credentials or URL string"));
            }
            finally
            {
                View.TestButton.Enabled = true;
            }
        }

        public void BindControlsToDataSource(ISettings settings)
        {
            //Credentions
            BindControlTextProperty(View.TextAcumaticaLogin, settings, nameof(settings.AcumaticaLogin));
            BindControlTextProperty(View.TextAcumaticaPassword, settings, nameof(settings.AcumaticaPassword));
            BindControlTextProperty(View.TextAcumaticaCompany, settings, nameof(settings.AcumaticaCompany));
            BindControlTextProperty(View.TextAcumaticaBaseUrl, settings, nameof(settings.AcumaticaBaseUrl));
            BindControlTextProperty(View.TextAcumaticaDefaultBoxId, settings, nameof(settings.AcumaticaDefaultBoxId));

            //General
            BindControlCheckedProperty(View.AcumaticaConfirmShipments, settings, nameof(settings.AcumaticaConfirmShipment));
            BindControlCheckedProperty(View.UpsAddUpdateAddressBook, settings, nameof(settings.UpsAddUpdateAddressBook));
            BindControlCheckedProperty(View.FedexAddUpdateAddressBook, settings, nameof(settings.FedexAddUpdateAddressBook));
        }

        public static void SetDefaults(ISettings settings)
        {

        }

        private void BindControlTextProperty(Control control, ISettings settings, string property)
        {
            control.DataBindings.Add(nameof(control.Text), settings, property, false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void BindControlCheckedProperty(CheckBox control, ISettings settings, string property)
        {
            control.DataBindings.Add(nameof(control.Checked), settings, property, false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
