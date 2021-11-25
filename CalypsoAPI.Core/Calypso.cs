using CalypsoAPI.Core.Events;
using CalypsoAPI.Core.Models;
using System;
using System.IO;

namespace CalypsoAPI.Core
{
    public class Calypso : IDisposable
    {
        private StateManager _stateManager;
        private MessageForm _messageForm;

        public event EventHandler<MeasurementStartEventArgs> MeasurementStarted;
        public event EventHandler<MeasurementFinishEventArgs> MeasurementFinished;
        public event EventHandler MeasurementStopped;
        public event EventHandler MeasurementPaused;
        public event EventHandler MeasurementContinued;
        public event EventHandler MeasurementError;

        public event EventHandler<ApiExceptionEventArgs> ApiException;

        public ApiConfiguration Configuration {get; private set;}
        public CmmState State { get; private set; }

        /// <summary>
        /// Initialize the Calypso Api client. Make sure to configure the API beforehand.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="Exception"/>
        public Calypso Initialize()
        {
            if (Configuration == null)
                throw new Exception("Api is not configured.");

            if (!Directory.Exists(Configuration.CMMObserverFolderPath))
                throw new DirectoryNotFoundException("CMMObserver directory does not exist.");

            _stateManager = new StateManager();
            _messageForm = new MessageForm();
            _messageForm.CmmStateChanged += _messageForm_CMMStateChanged;
            _messageForm.Show();

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="DirectoryNotFoundException"/>
        public Calypso Configure(ApiConfiguration configuration)
        {
            if (!Directory.Exists(configuration.CMMObserverFolderPath))
                throw new DirectoryNotFoundException("CMMObserver directory does not exist.");

            if (configuration.CopyChrFileAfterReading)
                if (configuration.ChrDestinationFolderPath == null || !Directory.Exists(configuration.ChrDestinationFolderPath))
                    throw new DirectoryNotFoundException("Chr directory does not exist.");

            Configuration = configuration;

            return this;
        }

        /// <summary>
        /// Catch WndProc messages send by calypso.
        /// Messages are sent when the state of the CMM changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void _messageForm_CMMStateChanged(object sender, CmmStateChangedEventArgs e)
        {
            try
            {
                var command = await _stateManager.GetCommandFileAsync(Configuration.CMMObserverFolderPath);
                var observer = await _stateManager.GetObserverFileAsync(Configuration.CMMObserverFolderPath);

                var measInfo = new MeasurementInfo();

                switch (e.Status)
                {
                    case Status.Running:
                        if (command.state == "set_cnc_start")
                        {
                            measInfo = new MeasurementInfo()
                            {
                                MeasurementPlanId = observer.planid,
                                DeviceGroup = observer.devicegroup,
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


                        }
                        break;

                    case Status.Finished:

                        break;


                    default:
                        break;
                }


            } 
            catch (Exception ex)
            {
                HandleException(ex);
            }
            
        }

        public void Dispose()
        {
            _messageForm.Close();
            _messageForm.CmmStateChanged -= _messageForm_CMMStateChanged;
            _messageForm?.Dispose();            
        }

        private void HandleException(Exception ex)
        {
            _messageForm.Close();
            _messageForm.Dispose();

            MeasurementInfo = new MeasurementInfo();
            MeasurementPlanInfo = new MeasurementPlanInfo();
            CMMStatus = Status.Exception;

            ApiException?.Invoke(this, new ApiExceptionEventArgs() { Exception = ex });
        }

    }


}
