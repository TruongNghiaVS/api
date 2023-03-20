namespace VS.Core.Repository.Model
{
    public class GetAllRecordGroupByLineCodeIndexModel : BaseIndexModel
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
    }




    public class GetAllRecordGroupByLineCodeExportIndexModel

    {

        private string convertToHHMMSS(decimal? number)
        {

            if (!number.HasValue || number == 0)
            {
                return "00:00:00";
            }

            double numberRe = (double)number;
            var timeSpan = TimeSpan.FromSeconds(numberRe);
            int hh = timeSpan.Hours;
            int mm = timeSpan.Minutes;
            int ss = timeSpan.Seconds;
            return hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
        }

        public GetAllRecordGroupByLineCodeExportIndexModel()
        {

        }
        public string FullName { get; set; }
        public string LineCode { get; set; }
        public int? SumNoAgree { get; set; }
        public string DateString
        {
            get
            {
                return this.YearR + "/" + this.MonthR + "/" + this.DayR;
            }
        }

        public int? SumCall
        {
            get
            {
                return this.SumAn + this.SumNOAswer + this.SumNOCancel + this.SumNoBussy + this.SumNoFail;
            }
        }

        public decimal? PerPercent { get; set; }

        private decimal? TimCall { get; set; }
        private decimal? TimeWaiting { get; set; }
        private decimal? TimeTalking { get; set; }
        public string TimeCallText
        {
            get
            {
                return convertToHHMMSS(TimCall);

            }
        }

        public string TimeWaitingText
        {
            get
            {

                return convertToHHMMSS(this.TimeWaiting);

            }
        }

        public string TimeTalkingText
        {
            get
            {

                return convertToHHMMSS(this.TimeTalking);

            }
        }
        public int? SumAn { get; set; }
        public int? SumNOAswer { get; set; }
        public int? SumNOCancel { get; set; }
        public int? SumNoBussy { get; set; }
        public int? SumNOChanel { get; set; }
        public int SumNoFail { get; set; }
        public int? SumNOServe { get; set; }

        private int? YearR { get; set; }

        private int? MonthR { get; set; }

        private int? DayR { get; set; }
        public DateTime? CreateAt { get; set; }




    }

}
