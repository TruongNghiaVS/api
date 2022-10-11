namespace VS.core.API.model
{
    public class BaseInputSearch
    {
        public string Token { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

        public string? OrderBy { get; set; }

        public string? Status { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public BaseInputSearch()
        {
            Limit = 10;
            Page = 1;
        }
    }
}
