using System;
using System.Windows.Forms;
using SmartShipment.UI.Common;

namespace SmartShipment.UI.Views
{
    public interface ISmartSettingsDialogView : IView
    {
        event Action OnSettingsSave;

        Form Form { get; }
        
        //Credentials
        TextBox TextAcumaticaLogin { get; }
        TextBox TextAcumaticaPassword { get; }
        TextBox TextAcumaticaCompany { get; }
        TextBox TextAcumaticaBaseUrl { get; }
        TextBox TextAcumaticaDefaultBoxId { get; }

        CheckBox AcumaticaConfirmShipments { get; }
        CheckBox UpsAddUpdateAddressBook { get; }
        CheckBox FedexAddUpdateAddressBook { get; }

        Button TestButton { get; }
                
        void SetControlsVisible();
        void SetFormAttributes();
        event Action OnTestLoginClick;
        event Action OnFormLoad;
        event Action OnSettingsCancel;
    }
}
