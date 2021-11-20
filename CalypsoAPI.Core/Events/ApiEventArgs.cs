using CalypsoAPI.Core.Models;
using System;

namespace CalypsoAPI.Core.Events
{
    public class CMMStateChangedEventArgs : EventArgs
    {
        public Status Status { get; set; }
    }

    public class MeasurementInfoEventargs
    {
        public MeasurementInfo MeasurementInfo { get; set; }
    }
}
