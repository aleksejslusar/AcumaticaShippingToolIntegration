using System.Threading.Tasks;
using System.Windows.Forms;
using SmartShipment.Information.Logger;

namespace SmartShipment.Information.InformationTypes
{
    public abstract class InfoObjectBase
    {
        private readonly ILogger _logger;

        protected InfoObjectBase(ILogger logger)
        {
            _logger = logger;
            IsLog = true;
            IsUIMessage = true;
        }

        public string Message { get; protected set; }
        public MessageSource Source { get; protected set; }
        public bool IsLog { get; protected set; }
        public bool IsUIMessage { get; protected set; }
                
        protected abstract void Log(ILogger logger);
        protected abstract DialogResult ShowMessage();

        public void HandleInfo()
        {
            if (IsLog)
            {
                Task.Factory.StartNew(() => Log(_logger));
            }

            if (IsUIMessage)
            {
                ShowMessage();
            }
        }
    }


    public enum MessageSource
    {
        Application = 0,
        UpsApplicatonAdapter = 1,
        FedexApplicatonAdapter = 2,
        ApplicatonUI = 3,
        ApplicatonSettings = 4,
        NetworkSoap = 5,
        NetworkRest = 6,
    }
}