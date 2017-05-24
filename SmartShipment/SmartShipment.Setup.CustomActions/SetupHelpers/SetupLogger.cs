using NLog;
using NLog.Config;
using NLog.Targets;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public interface ISetupLogger
    {
        void Info(string message);
        void Error(string message);
    }

    public class SetupLogger : ISetupLogger
    {
        private Logger _logger;

        public SetupLogger()
        {
            Init();
        }

        private void Init()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = "${specialfolder:folder=ApplicationData}/Sprinterra/Acumatica Shipping Tool Integration/Logs/install.txt";
            fileTarget.Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} ${message}";

            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);


            LogManager.Configuration = config;

            _logger = LogManager.GetLogger("Setup Logger");
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}