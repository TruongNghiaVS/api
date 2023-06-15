namespace VS.Core.Repository.Model
{
    public class ImpactHistoryIndexModel : BaseIndexModel
    {
        public string? ShortDescription { get; set; }
        public string? StatusIm { get; set; }

        public string? NoteIm { get; set; }

        public DateTime? Promiseday { get; set; }

        public string? MoneyPromise { get; set; }

        public DateTime? DaysuggestTime { get; set; }

        public int? StatusFollow { get; set; }

        public int? Relationship { get; set; }

        public int? ProfileId { get; set; }

        public int? Priority { get; set; }

        public string? StatusName { get; set; }

        public string? ColorCode { get; set; }
        public DateTime? CreateAt { get; set; }


    }



    public class ImpactHistoryv2IndexModel : BaseIndexModel
    {
        public DateTime? CreateTime { get; set; }
        public string? Username { get; set; }

        public string? NoAgreement { get; set; }

        public string? PlaceCode { get; set; }

        public string? WayContact { get; set; }

        public string? NoteCode { get; set; }

        public string? StatusName { get; set; }

        public DateTime? Promiseday { get; set; }

        public string? MoneyPromise { get; set; }

        public string? NoteIm { get; set; }



    }

}
