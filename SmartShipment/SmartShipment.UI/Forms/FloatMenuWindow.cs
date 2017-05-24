using System;
using System.Windows.Forms;
using SmartShipment.UI.Common;
using SmartShipment.UI.Views;

namespace SmartShipment.UI.Forms
{
    public partial class FloatMenuWindow : SmartShipmentBaseForm, IFloatMenuView
    {

        public event Action FormLoad;
        public event Action StartUps;
        public event Action StartFedEx;
        public event Action StartSettings;        
        public event Action CloseApplication;
        public event Action<object,MouseEventArgs> OnFormMouseUp;
        public event Action<object, MouseEventArgs> OnFormMouseDown;
        public event Action<object, MouseEventArgs> OnFormMouseMove;



        public Button ButtonUps => buttonUps;
        public Button ButtonFedEx => buttonFedEx;
        public Button ButtonSettings => buttonSettings;
        public Button Exit => buttonExit;
        public Form Form => this;

        public FloatMenuWindow(ApplicationContext context): base(context)
        {                 
            InitializeComponent();
            //Events        
            Load += (sender, args) => Invoke(FormLoad);
            buttonExit.Click += (sender, args) => Invoke(CloseApplication);
            buttonUps.Click += (sender, args) => Invoke(StartUps);
            buttonFedEx.Click += (sender, args) => Invoke(StartFedEx);
            ButtonSettings.Click += (sender, args) => Invoke(StartSettings);

            MouseDown += (sender, args) => Invoke(OnFormMouseDown, sender, args);
            MouseUp += (sender, args) => Invoke(OnFormMouseUp, sender, args);
            MouseMove += (sender, args) => Invoke(OnFormMouseMove, sender, args);
        }        

        public new void Show()
        {
            ApplicationContext.MainForm = this;
            base.Show();
        }
    }
}
