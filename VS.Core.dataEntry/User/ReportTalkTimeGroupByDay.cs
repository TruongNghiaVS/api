namespace VS.Core.dataEntry.User
{
    public class ReportTalkTimeGroupByDay : BaseEntry
    {
        public string? LineCode { get; set; }

        public int? SumCall { get; set; }

        public int? SumNoAgree { get; set; }

        public double? PerPercent { get; set; }

        public int? SumAn { get; set; }
        public int? VendorId { get; set; }
        public int? SumNoBussy { get; set; }
        public int? SumNOCancel { get; set; }
        public int? SumNOAswer { get; set; }
        public int? SumNOChanel { get; set; }
        public int? SumNOServe { get; set; }
        public decimal? TimeWaiting { get; set; }
        public decimal? Timcall { get; set; }

        public decimal? TimeTalking { get; set; }
        public int? YearR { get; set; }
        public int? MonthR { get; set; }
        public int? DayR { get; set; }
        public int? SumNoFail { get; set; }


        public ReportTalkTimeGroupByDay()
        {

        }

    }


}
