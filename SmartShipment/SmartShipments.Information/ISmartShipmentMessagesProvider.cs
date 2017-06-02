using System;
using System.Windows.Forms;

namespace SmartShipment.Information
{
    public interface ISmartShipmentMessagesProvider
    {
        void Error(Exception exception);
        void Warn(string message);
        void Info(string message);
        void Log(string message);
        void Fatal(Exception exception);
        DialogResult Message(string message);
    }
}