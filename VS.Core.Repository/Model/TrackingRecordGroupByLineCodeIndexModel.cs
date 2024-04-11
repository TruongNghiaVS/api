namespace VS.Core.Repository.Model
{
    public class TrackingRecordGroupByLineCodeIndexModel : BaseIndexModel
    {
        public int? SumCall { get; set; }

        public string LineCode { get; set; }

        public int? SumNoAgree { get; set; }

        public int? SumAn { get; set; }

        public int? SumNoBussy { get; set; }

        public int? SumNOCancel { get; set; }

        public int? SumNOAswer { get; set; }

        public int? SumNOChanel { get; set; }

        public int? SumNOServe { get; set; }

        public decimal? TimeWaiting { get; set; }

        public decimal? TimCall { get; set; }

        public int? YearR { get; set; }

        public int? MonthR { get; set; }

        public int? DayR { get; set; }

        public int Total { get; set; }

        public int SumNoFail { get; set; }

        public decimal? PerPercent { get; set; }

        public decimal? TimeTalking { get; set; }

        public string? UserName { get; set; }

        public string? ManagerUserName { get; set; }
        public DateTime? LastCall { get; set; }

        public DateTime? EndOfCall { get; set; }
        public DateTime? LastCallcrm { get; set; }


        public bool? IsCalling { get; set; }

        public string? DurationRealTime { get; set; }

        public TrackingRecordGroupByLineCodeIndexModel()
        {
            IsCalling = false;
            DurationRealTime = "00:00:00";
        }


    }

}
