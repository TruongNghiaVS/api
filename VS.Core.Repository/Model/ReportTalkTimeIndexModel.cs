namespace VS.Core.Repository.Model
{
    public class ReportTalkTimeIndexModel : BaseIndexModel
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

        public ReportTalkTimeIndexModel()
        {

        }


    }
}
