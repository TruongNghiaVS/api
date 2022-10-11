namespace VS.Core.Repository.Model
{
    public class CampagnIndexModel : BaseIndexModel
    {
        public string? Code { get; set; }
        public string? CompanyId { get; set; }

        public string? DisplayName { get; set; }

        public bool? Status { get; set; }

        public int? SumCount { get; set; }

        public int? ProcessingCount { get; set; }

        public int? ClosedCount { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? Priority { get; set; }



    }
}
