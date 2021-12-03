using CalypsoAPI.Events;
using CalypsoAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalypsoAPI.Interface
{
    public interface ICalypso : IDisposable
    {
        CalypsoConfiguration Configuration { get; set; }
        bool IsRunning { get; }
        List<IService> Services { get; set; }
        CmmState State { get; }

        event EventHandler<CalypsoExceptionEventArgs> CalypsoException;
        event EventHandler MeasurementContinued;
        event EventHandler MeasurementError;
        event EventHandler<MeasurementFinishEventArgs> MeasurementFinished;
        event EventHandler MeasurementPaused;
        event EventHandler<MeasurementStartEventArgs> MeasurementStarted;
        event EventHandler MeasurementStopped;

        Task StartAsync();
        Task StopAsync();
    }
}