using System;
using System.Windows.Forms;
using SmartShipment.Information.Logger;

namespace SmartShipment.Information.InformationTypes
{
    public class Logging : InfoObjectBase
    {
        public Logging(ILogger logger, string message) : base(logger)
        {
            Message = message;
            IsUIMessage = false;
        }

        protected override void Log(ILogger logger)
        {
            logger.Info(Message);
        }

        protected override DialogResult ShowMessage()
        {
            return DialogResult.None;
        }
    }

    public class Fatal : InfoObjectBase
    {
        public Fatal(ILogger logger, Exception e): this(logger, e.ToString()) { }

        public Fatal(ILogger logger, string message) : base(logger)
        {
            Message = message;
            IsUIMessage = false;
        }

        protected override void Log(ILogger logger)
        {
            logger.Fatal(Message);
        }

        protected override DialogResult ShowMessage()
        {
            return DialogResult.None;
        }
    }

}