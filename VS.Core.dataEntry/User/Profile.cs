namespace VS.Core.dataEntry.User
{
    public class Campagn : BaseEntry
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

        public string? GroupStatus { get; set; }

        public int? VendorId { get; set; }

        public Campagn()
        {
            Priority = 1;
            CompanyId = "-1";
        }

    }


}
