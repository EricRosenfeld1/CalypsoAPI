using CalypsoAPI.Core;
using CalypsoAPI.Core.Events;
using CalypsoAPI.Core.Interface;
using CalypsoAPI.Core.Models.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalypsoAPI.Test
{
    public class CalypsoMock : ICalypso
    {
        public CalypsoConfiguration Configuration { get; set; }

        public bool IsRunning { get; set; }

        public List<IService> Services { get; set; } = new List<IService>();

        public CmmState State { get; set; } = new CmmState();

        public event EventHandler<CalypsoExceptionEventArgs> CalypsoException;
        public event EventHandler MeasurementContinued;
        public event EventHandler MeasurementError;
        public event EventHandler<MeasurementFinishEventArgs> MeasurementFinished;
        public event EventHandler MeasurementPaused;
        public event EventHandler<MeasurementStartEventArgs> MeasurementStarted;
        public event EventHandler MeasurementStopped;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task StartAsync()
        {
            foreach (var service in Services)
                await service.StartAsync();

            var start = await CalypsoFileHelper.GetStartFileAsync(Configuration.CMMObserverFolderPath);
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

            State.Status = Status.Running;
            State.LatestMeasurementResults = await CalypsoFileHelper.GetMeasurementResultAsync($"{Configuration.CMMObserverFolderPath}\\test_chr.txt");

            IsRunning = true;
        }

        public Task StopAsync()
        {
            IsRunning = false;
            return Task.CompletedTask;
        }
    }
}
