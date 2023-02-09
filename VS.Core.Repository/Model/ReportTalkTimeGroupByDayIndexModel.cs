namespace VS.Core.Repository.Model
{
    public class ReportTalkTimeGroupByDayIndexModel : BaseIndexModel
    {

        public string? LineCode { get; set; }

        public int? SumCall { get; set; }

        public int? SumNoAgree { get; set; }

        public double? PerPercent { get; set; }

        public int? SumAn { get; set; }

        public int? SumNoBussy { get; set; }
        public int? SumNOCancel { get; set; }

        public int? SumNOAswer { get; set; }

        public int? SumNOChanel { get; set; }

        public int? SumNOServe { get; set; }

        public double? TimeWaiting { get; set; }

        public double? Timcall { get; set; }

        public int? YearR { get; set; }

        public int? MonthR { get; set; }

        public int? DayR { get; set; }



    }
}
