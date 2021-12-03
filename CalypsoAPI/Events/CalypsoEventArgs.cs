using CalypsoAPI.Models;
using System;

namespace CalypsoAPI.Events
{
    public class CmmStateChangedEventArgs : EventArgs
    {
        public Status Status { get; set; }
    }

    public class MeasurementInfoEventArgs : EventArgs
    {
        public MeasurementInfo MeasurementInfo { get; set; }
    }

    public class MeasurementPlanInfoEventArgs : EventArgs
    {
        public MeasurementPlanInfo MeasurementPlanInfo { get; set; }
    }

    public class CalypsoExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }

    public class MeasurementStartEventArgs : EventArgs
    {
        public MeasurementPlanInfo MeasurementPlan { get; set; }
    }

    public class MeasurementFinishEventArgs : EventArgs
    {
        public MeasurementPlanInfo MeasurementPlan { get; set; }
        public MeasurementInfo MeasurementInfo { get; set; }
        public MeasurementResult MeasurementResult { get; set; }
    }
}
