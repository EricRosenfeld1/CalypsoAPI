using CalypsoAPI.Core.Events;
using CalypsoAPI.Core.Models;
using System;
using System.IO;

namespace CalypsoAPI.Core
{
    public class Calypso
    {
        private readonly ApiConfiguration _config;
        private StateManager _stateManager;
        private MessageForm _messageForm;

        public event EventHandler<CmmStateChangedEventArgs> CmmStateChanged;
        public event EventHandler<MeasurementInfoEventArgs> MeasurementInfoChanged;
        public event EventHandler<MeasurementPlanInfoEventArgs> MeasurementPlanInfoChanged;


        /// <summary>
        /// Information of the current measurement plan.
        /// </summary>
        /// <remarks>Is <see langword="null"/> if CMM Observer file does not exist</remarks>
        public MeasurementPlanInfo MeasurementPlanInfo { get; private set; }

        /// <summary>
        /// Information of the current measurement.
        /// </summary>
        /// <remarks>Is <see langword="null"/> if CMM Observer file does not exist</remarks>
        public MeasurementInfo MeasurementInfo { get; private set; }

        /// <summary>
        /// Current state of the CMM.
        /// </summary>
        public Status CMMStatus { get; private set; }

        /// <summary>
        /// Calpyso API Client.
        /// </summary>
        /// <param name="configuration"></param>
        public Calypso(ApiConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Initialize the Calypso Api client.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public void Initialize()
        {
            if (!Directory.Exists(_config.CMMObserverFolderPath))
                throw new DirectoryNotFoundException("Observer directory does not exist.");

            _stateManager = new StateManager();
            _messageForm = new MessageForm();
            _messageForm.CmmStateChanged += _messageForm_CMMStateChanged;
            _messageForm.Show();
        }

        /// <summary>
        /// Catch WndProc messages send by calypso.
        /// Messages are sent when the state of the CMM changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void _messageForm_CMMStateChanged(object sender, CmmStateChangedEventArgs e)
        {
            var command = await _stateManager.GetCommandFileAsync(_config.CMMObserverFolderPath);
            var observer = await _stateManager.GetObserverFileAsync(_config.CMMObserverFolderPath);

            var measInfo = new MeasurementInfo();

            switch (e.Status)
            {
                case Status.Running:
                    if (command.state == "set_cnc_start")
                    {
                        measInfo = new MeasurementInfo()
                        {
                            MeasurementPlanId = observer.planid,
                            ControllerType = observer.controllertyp,
                            DeviceGroup = observer.devicegroup,
                            FirmwareRevision = observer.firmWareRevision,
                            OperatorId = observer.operid,
                            PartNumber = observer.partnbinc,
                            MeasurementPlanFileName = command.planPath,
                        };

                        var start = await _stateManager.GetStartFileAsync(command.planPath);
                        var planInfo = new MeasurementPlanInfo()
                        {
                            BaseSystemName = start.baseSystemRealName,
                            BaseSystemType = start.baseSystemType,
                            ClearOldResults = start.clearOldRes,
                            CompactProtocol = start.compactProtocol,
                            DisplayPlots = start.displayPlots,
                            FeatureListName = start.featureListName,
                            FileName = start.nameOfFile,
                            MotionPlanning = start.motionPlanning,
                            NaviMode = start.naviMode,
                            PdfExport = start.pdfExport,
                            PresentationProtocol = start.presProtocol,
                            Printer = start.printer,
                            PrintPlots = start.printPlots,
                            RunMode = start.runMode,
                            Speed = start.speed
                        };

                        MeasurementInfo = measInfo;
                        MeasurementInfoChanged?.Invoke(this, new MeasurementInfoEventArgs() { MeasurementInfo = measInfo });

                        MeasurementPlanInfo = planInfo;
                        MeasurementPlanInfoChanged?.Invoke(this, new MeasurementPlanInfoEventArgs() { MeasurementPlanInfo = planInfo });
                    }
                    break;

                case Status.Finished:
                    measInfo = new MeasurementInfo()
                    {
                        MeasurementPlanId = observer.planid,
                        ControllerType = observer.controllertyp,
                        DeviceGroup = observer.devicegroup,
                        FirmwareRevision = observer.firmWareRevision,
                        OperatorId = observer.operid,
                        PartNumber = observer.partnbinc,
                        ChrFilePath = command.chrPath,
                        HdrFilePath = command.hdrPath,
                        FetFilePath = command.fetPath
                    };

                    MeasurementInfo = measInfo;
                    MeasurementInfoChanged?.Invoke(this, new MeasurementInfoEventArgs() { MeasurementInfo = measInfo });
                    break;

                case Status.Paused:
                case Status.Stopped:
                case Status.Exception:
                default:
                    break;
            }

            CMMStatus = e.Status;
            CmmStateChanged?.Invoke(this, new CmmStateChangedEventArgs() { Status = e.Status });
        }
    }


}
