namespace VS.core.API.model
{
    public class CampagnAdd
    {
        public string? Code { get; set; }
        public string? CompanyId { get; set; }
        public string? DisplayName { get; set; }
        public bool? Status { get; set; }
        public int? SumCount { get; set; }
        public int? ProcessingCount { get; set; }
        public int? ClosedCount { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Priority { get; set; }
        public string? ShortDes { get; set; }
        public CampagnAdd()
        {
            Priority = 1;

        }


    }
    public class CampagnUpdate
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? CompanyId { get; set; }

        public string? DisplayName { get; set; }

        public bool? Status { get; set; }

        public int? SumCount { get; set; }

        public int? ProcessingCount { get; set; }

        public int? ClosedCount { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? Priority { get; set; }

        public string? ShortDes { get; set; }

        public CampagnUpdate()
        {
            Priority = 1;
        }
    }

}
