using NLog;
using NLog.Config;
using NLog.Targets;

namespace SmartShipment.Information.Logger
{
    public class SmartShipmentLogger : ILogger
    {
        private NLog.Logger _logger;
        public SmartShipmentLogger()
        {
            Init();
        }

        private void Init()
        {            
            var config = new LoggingConfiguration();

            //Base log, warnings and errors
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);         
            fileTarget.FileName = "${specialfolder:folder=ApplicationData}/Sprinterra/Acumatica Shipping Tool Integration/Logs/smartShipmentLog.txt";
            fileTarget.Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} ${message}";
            var rule = new LoggingRule("*", LogLevel.Debug, LogLevel.Error, fileTarget);
            config.LoggingRules.Add(rule);


            //Base log, warnings and errors
            var fileFatalTarget = new FileTarget();
            config.AddTarget("file", fileFatalTarget);
            fileFatalTarget.FileName = "${specialfolder:folder=ApplicationData}/Sprinterra/Acumatica Shipping Tool Integration/Logs/applicationErrors.txt";
            fileFatalTarget.Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} ${message}";
            
            var ruleFatal = new LoggingRule("*", LogLevel.Fatal, fileFatalTarget);
            config.LoggingRules.Add(ruleFatal);

            LogManager.Configuration = config;
            
            _logger = LogManager.GetLogger("SmartShipment Logger");            
        }


        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }
    }
}