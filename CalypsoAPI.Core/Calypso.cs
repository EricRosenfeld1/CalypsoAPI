using CalypsoAPI.Core.Events;
using CalypsoAPI.Core.Models.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CalypsoAPI.Core
{
    public class Calypso : IDisposable
    {
        private MessageForm _messageForm = new MessageForm();

        public event EventHandler<MeasurementStartEventArgs> MeasurementStarted;
        public event EventHandler<MeasurementFinishEventArgs> MeasurementFinished;
        public event EventHandler MeasurementStopped;
        public event EventHandler MeasurementPaused;
        public event EventHandler MeasurementContinued;
        public event EventHandler MeasurementError;

        public event EventHandler<CalypsoExceptionEventArgs> CalypsoException;

        /// <summary>
        /// Is calypso api running
        /// </summary>
        public bool IsRunning { get; private set; }

        public CalypsoConfiguration Configuration { get; set;}

        public List<IService> Services { get; set; } = new List<IService>();

        /// <summary>
        /// Information about state of the measuring machine, measurement plan and results
        /// </summary>
        public CmmState State { get; private set; } = new CmmState();

        /// <summary>
        /// Start the calypso api after verifying the configuration
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">Some of the paths in the configuration are missing.</exception>
        /// <exception cref="Exception">If configuration is null</exception>
        public async Task StartAsync()
        {
            if (Configuration == null)
                throw new Exception("No configuration found!");

            if (!Directory.Exists(Configuration.CMMObserverFolderPath))
                throw new DirectoryNotFoundException("Observer directory does not exist!");

            if (Configuration.CopyChrFileAfterReading)
                if (!Directory.Exists(Configuration.ChrDestinationFolderPath))
                    throw new DirectoryNotFoundException("Chr destination folder does not exist!");

            IsRunning = true;

            _messageForm.CmmStateChanged += OnCmmStateChanged;
            _messageForm.Show();

            foreach(var service in Services)
            {
                try
                {
                    await service.StartAsync();
                } 
                catch (Exception ex)
                {
                    CalypsoException?.Invoke(this, new CalypsoExceptionEventArgs() { Exception = ex });
                }
            }
        }

        /// <summary>
        /// Stop the calypso api
        /// </summary>
        public async Task StopAsync()
        {
            _messageForm.CmmStateChanged -= OnCmmStateChanged;

            foreach (var service in Services)
            {
                try
                {
                    await service.StopAsync();
                }
                catch (Exception ex)
                {
                    CalypsoException?.Invoke(this, new CalypsoExceptionEventArgs() { Exception = ex });
                }
            }

            IsRunning = false;
        }


        public void Dispose()
        {
            IsRunning = false;
            _messageForm.CmmStateChanged -= OnCmmStateChanged;
            _messageForm.Close();
        }

        /// <summary>
        /// Catch WndProc messages send by calypso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnCmmStateChanged(object sender, CmmStateChangedEventArgs e)
        {
            try
            {
                State.Status = e.Status;

                var command = await CalypsoFileHelper.GetCommandFileAsync(Configuration.CMMObserverFolderPath);
                var observer = await CalypsoFileHelper.GetObserverFileAsync(Configuration.CMMObserverFolderPath);

                switch (e.Status)
                {
                    case Status.Running:
                        if (command.state == "set_cnc_start")
                        { 
                            var start = await CalypsoFileHelper.GetStartFileAsync(command.planPath);
                            State.MeasurementPlan = new MeasurementPlanInfo()
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
                                Speed = start.speed,
                                Date = start.date,
                                Time = start.time,
                                OperatorId = start.operid,
                                PartNumber = start.partnbinc
                            };
                            MeasurementStarted?.Invoke(this, new MeasurementStartEventArgs() { MeasurementPlan = State.MeasurementPlan });
                        } 
                        else if(command.state == "set_cnc_cont")
                            MeasurementContinued?.Invoke(this, EventArgs.Empty);                   
                        break;

                    case Status.Finished:
                        if(State.MeasurementPlan != null)
                        {
                            var info = new MeasurementInfo()
                            {
                                ChrPath = command.chrPath,
                                HdrPath = command.hdrPath,
                                FetPath = command.fetPath,
                                ToleranceState = command.toleranceState
                            };
                            MeasurementFinished?.Invoke(this, new MeasurementFinishEventArgs()
                            {
                                MeasurementInfo = info,
                                MeasurementPlan = State.MeasurementPlan,
                                MeasurementResult = await CalypsoFileHelper.GetMeasurementResultAsync(command.chrPath)
                            });
                        }                   
                        break;

                    case Status.Stopped:
                        MeasurementStopped?.Invoke(sender, EventArgs.Empty);
                        break;

                    case Status.Paused:
                        MeasurementPaused?.Invoke(this, EventArgs.Empty);
                        break;

                    case Status.Exception:
                        MeasurementError?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// Handle exceptions and raise the exception event
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            Dispose();
            CalypsoException?.Invoke(this, new CalypsoExceptionEventArgs() { Exception = ex });
        }
    }


}
