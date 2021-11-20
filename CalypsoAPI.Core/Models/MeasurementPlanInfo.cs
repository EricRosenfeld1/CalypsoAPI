using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core.Models
{
    public class MeasurementPlanInfo
    {
        public string FileName { get; set; } = string.Empty;
        public string BaseSystemName { get; set; } = string.Empty;
        public string BaseSystemType { get; set; } = string.Empty;
        public bool ClearOldResults { get; set; }
        public bool CompactProtocol { get; set; }
        public bool DisplayPlots { get; set; }
        public string FeatureListName { get; set; } = string.Empty;
        public bool MotionPlanning { get; set; }
        public string NaviMode { get; set; } = string.Empty;
        public bool PdfExport { get; set; }
        public bool PresentationProtocol { get; set; }
        public bool Printer { get; set; }
        public bool PrintPlots { get; set; }
        public string RunMode { get; set; } = string.Empty;
        public int Speed { get; set; }
    }
}
