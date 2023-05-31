namespace VS.core.API.model
{
    public class ImpactHistoryAdd
    {
        public string? Code { get; set; }

        public string? ShortDescription { get; set; }
        public string? StatusIm { get; set; }

        public string? NoteIm { get; set; }
        public string? NoteCode { get; set; }

        public DateTime? Promiseday { get; set; }

        public string? MoneyPromise { get; set; }

        public DateTime? DaysuggestTime { get; set; }

        public int? StatusFollow { get; set; }

        public int? Relationship { get; set; }

        public int? ProfileId { get; set; }

        public int? Priority { get; set; }

        public int? Assignee { get; set; }

        public string ColorCode { get; set; }
    }
    public class ImpactHistoryUpdate
    {
        public string Id { get; set; }
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

        public ImpactHistoryUpdate()
        {
            Priority = 1;
        }
    }

}
