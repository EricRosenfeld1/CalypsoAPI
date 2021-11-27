namespace CalypsoAPI.Core.Models.State
{
    /// <summary>
    /// Measurement plan details
    /// </summary>
    public class MeasurementPlanInfo
    {
        public string FileName { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string OperatorId { get; set; } = string.Empty;

        public string BaseSystemName { get; set; } = string.Empty;
        public string BaseSystemType { get; set; } = string.Empty;
        public string ClearOldResults { get; set; } = string.Empty;
        public string CompactProtocol { get; set; } = string.Empty;
        public string DisplayPlots { get; set; } = string.Empty;
        public string FeatureListName { get; set; } = string.Empty;
        public string MotionPlanning { get; set; } = string.Empty;
        public string NaviMode { get; set; } = string.Empty;
        public string PdfExport { get; set; } = string.Empty;
        public string PresentationProtocol { get; set; } = string.Empty;
        public string Printer { get; set; } = string.Empty;
        public string PrintPlots { get; set; } = string.Empty;
        public string RunMode { get; set; } = string.Empty;
        public string Speed { get; set; } = string.Empty;

    }
}
