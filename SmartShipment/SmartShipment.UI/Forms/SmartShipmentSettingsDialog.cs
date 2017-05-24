using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SmartShipment.UI.Common;
using SmartShipment.UI.Views;


namespace SmartShipment.UI.Forms
{
    public partial class SmartShipmentSettingsDialog : SmartShipmentBaseForm, ISmartSettingsDialogView
    {
        public event Action OnSettingsSave;
        public event Action OnSettingsCancel;
        public event Action OnTestLoginClick;
        public event Action OnFormLoad;

        public Form Form => this;
        public TextBox TextAcumaticaLogin => textAcumaticaLogin;
        public TextBox TextAcumaticaPassword => textAcumaticaPassword;
        public TextBox TextAcumaticaCompany => textAcumaticaCompany;
        public TextBox TextAcumaticaBaseUrl => textBoxBaseUrl;
        public TextBox TextAcumaticaDefaultBoxId => textBoxAcumaticaDefaultBoxId;
        public CheckBox AcumaticaConfirmShipments => checkBoxAcumaticaConfirmShipment;
        public CheckBox UpsAddUpdateAddressBook => checkBoxUpsAddUpdateAddressBook;
        public CheckBox FedexAddUpdateAddressBook => checkBoxFedexAddUpdateAddressBook;
        public Button TestButton => btnAcumaticaTestLogin;


        public SmartShipmentSettingsDialog()
        {
            InitializeComponent();
           
            SetControlsVisible();           
            btnAcumaticaTestLogin.Click += (sender, args) => Invoke(OnTestLoginClick);
            btnOk.Click += (sender, args) => Invoke(OnSettingsSave);
            btnCancel.Click += (sender, args) => Invoke(OnSettingsCancel);      
            Load += (sender, args) => Invoke(OnFormLoad);
        }

        public new void Show()
        {
            ShowDialog();
        }

        public void SetFormAttributes()
        {
            Text = GetAssemblyInformation(Text);            
        }

        public void SetControlsVisible()
        {

        }

        private string GetAssemblyInformation(string formTitle)
        {
            var currentAssembly = typeof(ApplicationMain).Assembly;
            var version = "0.0.0.0";
            var title = "Generic application";

            var attributes = currentAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
            if (attributes.Any())
            {
                version = ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }

            attributes = currentAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (attributes.Any())
            {
                title = ((AssemblyTitleAttribute)attributes[0]).Title;
            }

            return formTitle.Replace("{applicationTitle}", title).Replace("{version}", version);
        }
    }
}
