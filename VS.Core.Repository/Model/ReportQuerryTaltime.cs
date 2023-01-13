namespace VS.Core.Repository.Model
{
    public class ReportQuerryTaltimeIndex : BaseIndexModel
    {
        public string? LineCode { get; set; }
        public string? NoAgree { get; set; }

        public string? PhoneLog { get; set; }

        public string? FileRecording { get; set; }

        public int? Duration { get; set; }

        public int? CompanyId { get; set; }

        public int? CampangnId { get; set; }

        public DateTime? CallDate { get; set; }

        public DateTime? EventTime { get; set; }

        public string Linkedid { get; set; }
    }


    public class ReportQuerryRecordingFileIndex : BaseIndexModel
    {
        public string File { get; set; }
        public int? Duration { get; set; }

        public int updated { get; set; }


    }
}
