using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core.Models
{
    public class MeasurementInfo
    {
        public string MeasurementPlanId { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string OperatorId { get; set; } = string.Empty;
        public string ControllerType { get; set; } = string.Empty;
        public string DeviceGroup { get; set; } = string.Empty;
        public string FirmwareRevision { get; set; } = string.Empty;

        public string ChrFilePath { get; set; } = string.Empty;
        public string FetFilePath { get; set; } = string.Empty;
        public string HdrFilePath { get; set; }

        public string MeasurementPlanFileName { get; set; } = string.Empty;
    }
}
