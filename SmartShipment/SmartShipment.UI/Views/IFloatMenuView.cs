using System;
using System.Windows.Forms;
using SmartShipment.UI.Common;

namespace SmartShipment.UI.Views
{
    public interface IFloatMenuView : IView
    {             
                
        event Action FormLoad;
        event Action StartUps;
        event Action StartFedEx;
        event Action StartSettings;        
        event Action CloseApplication;

        Button ButtonUps { get; }
        Button ButtonFedEx { get; }
        Button ButtonSettings { get; }
        Button Exit { get; }

        Form Form { get; }

        event Action<object, MouseEventArgs> OnFormMouseUp;
        event Action<object, MouseEventArgs> OnFormMouseDown;
        event Action<object, MouseEventArgs> OnFormMouseMove;
    }
}
