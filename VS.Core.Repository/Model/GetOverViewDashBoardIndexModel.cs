namespace VS.Core.Repository.Model
{
    public class GetOverViewDashBoardIndexModel
    {
        public int? SumCall { get; set; }
        public int? SumNoAgree { get; set; }
        public decimal? Perpercent { get; set; }

        public decimal? Timcall { get; set; }
        public decimal? TimeWaiting { get; set; }
        public decimal? TimeTalking { get; set; }

    }


}
