using System.Drawing;
using System.Windows.Forms;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Settings;
using SmartShipment.UI.Common;
using SmartShipment.UI.Views;

namespace SmartShipment.UI.Presenters
{
    public class FloatMenuPresenter : BasePresenter<IFloatMenuView>
    {
        private readonly IApplicationController _controller;
        private readonly ISettings _settings;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        private bool _mouseDown;
        private Point _lastLocation;
        
        public FloatMenuPresenter(IApplicationController controller, IFloatMenuView view, ISettings settings,ISmartShipmentMessagesProvider messagesProvider) : base(controller, view)
        {
            _controller = controller;
            _settings = settings;
            _messagesProvider = messagesProvider;
            View.CloseApplication += ShutdownApplication;
            View.StartSettings += ShowSettings;
            View.StartUps += StartUps;
            View.StartFedEx += StartFedEx;
            View.OnFormMouseDown += OnFormMouseDown;
            View.OnFormMouseMove += OnFormMouseMove;
            View.OnFormMouseUp += OnFormMouseUp;
            DockTop();
        }

        private void OnFormMouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
            _settings.GeneralApplicationStartPositionTop = View.Form.Top;
            _settings.GeneralApplicationStartPositionLeft = View.Form.Left;
            _settings.Save();
        }

        private void OnFormMouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                View.Form.Location = new Point((View.Form.Location.X - _lastLocation.X) + e.X, (View.Form.Location.Y - _lastLocation.Y) + e.Y);
                View.Form.Update();
            }
        }

        private void OnFormMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastLocation = e.Location;           
        }

        private void StartUps()
        {
            SetButtons(false);
            _controller.StartShipmentApplication<IShipmentApplicationAdapter, IShipmentApplicationHelper>(ApplicationTypes.UpsWorldShip.ToString());
            SetButtons(true);
        }

        private void StartFedEx()
        {
            SetButtons(false);
            _controller.StartShipmentApplication<IShipmentApplicationAdapter, IShipmentApplicationHelper>(ApplicationTypes.FedExShipmentManager.ToString());
            SetButtons(true);
        }        

        private void ShowSettings()
        {
            _controller.Run<SmartShipmentSettingDialogPresenter>();           
        }

        public void ShutdownApplication()
        {
            if (_messagesProvider.Message(InformationResources.MESSAGE_APPLICATION_EXIT) == DialogResult.Yes)
            {
                View.Close();
            }
        }

        public void DockTop()
        {
            if (_settings.GeneralApplicationStartPositionLeft == 0 && _settings.GeneralApplicationStartPositionTop == 0)
            {
                var screenSize = Screen.PrimaryScreen.WorkingArea.Size;
                View.Form.Top = 5;
                View.Form.Left = screenSize.Width/2 - View.Form.Width/2;
            }
            else
            {
                View.Form.Top = _settings.GeneralApplicationStartPositionTop;
                View.Form.Left = _settings.GeneralApplicationStartPositionLeft;
            }
            
        }

        public void ProcessShipment(ApplicationTypes shipmentType, string shipmentId)
        {
            SetButtons(false);
            switch (shipmentType)
            {
                case ApplicationTypes.UpsWorldShip:
                    _controller.StartShipmentApplication<IShipmentApplicationAdapter, IShipmentApplicationHelper>(ApplicationTypes.UpsWorldShip.ToString(), shipmentId);
                    break;
                case ApplicationTypes.FedExShipmentManager:
                    _controller.StartShipmentApplication<IShipmentApplicationAdapter, IShipmentApplicationHelper>(ApplicationTypes.FedExShipmentManager.ToString(), shipmentId);
                    break;
            }           
            SetButtons(true);
        }

        private void SetButtons(bool enabled)
        {
            View.ButtonUps.Enabled = enabled;
            View.ButtonFedEx.Enabled = enabled;
            View.ButtonSettings.Enabled = enabled;
            View.Exit.Enabled = enabled;
            Cursor.Current = !enabled ? Cursors.WaitCursor : Cursor.Current;
        }
    }
}
