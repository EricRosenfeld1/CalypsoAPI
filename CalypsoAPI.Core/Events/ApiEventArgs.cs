using CalypsoAPI.Core.Models;
using System;

namespace CalypsoAPI.Core.Events
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
}
