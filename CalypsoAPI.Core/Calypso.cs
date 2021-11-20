using System;
using System.Windows.Forms;
using CalypsoAPI.Core.Events;
using CalypsoAPI.Core.Models;

namespace CalypsoAPI.Core
{
    public class Calypso
    {
        private readonly ApiConfiguration _config;
        private StateManager _stateManager;
        private MessageForm _messageForm;

        public event EventHandler<CMMStateChangedEventArgs> CMMStateChanged;
        public event EventHandler<MeasurementInfoEventargs> MeasurementInfoChanged;

        public MeasurementPlanInfo MeasurementPlanInfo { get; private set; }
        public MeasurementInfo MeasurementInfo { get; private set; }
        public Status CMMStatus { get; private set; }

        public Calypso(ApiConfiguration configuration)
        {
            _config = configuration;
        }

        public void Initialize()
        {
            _stateManager = new StateManager();
            _messageForm = new MessageForm();
            _messageForm.Show();
            _messageForm.CMMStateChanged += _messageForm_CMMStateChanged;
        }

        private async void _messageForm_CMMStateChanged(object sender, CMMStateChangedEventArgs e)
        {
            CMMStatus = e.Status;
            CMMStateChanged?.Invoke(this, e);

            var command = await _stateManager.GetCommandFileAsync(_config.CMMObserverFolderPath);
            var observer = await _stateManager.GetObserverFileAsync(_config.CMMObserverFolderPath);
            var info = new MeasurementInfo()
            {
                MeasurementPlanId = observer.planid,
                ControllerType = observer.controllertyp,
                DeviceGroup = observer.devicegroup,
                FirmwareRevision = observer.firmWareRevision,
                OperatorId = observer.operid,
                PartNumber = observer.partnbinc,
                MeasurementPlanFileName = command.planPath,
                ChrFilePath = command.chrPath,
                HdrFilePath = command.hdrPath,
                FetFilePath = command.fetPath                             
            };

            MeasurementInfo = info;
            MeasurementInfoChanged?.Invoke(this, new MeasurementInfoEventargs() { MeasurementInfo = info });
        }
    }


}
