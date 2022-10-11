using System.Collections;

namespace VS.core.Request
{
    public class BaseSearchRequest
    {
        public string? Token { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public string? OrderBy { get; set; }
        public string? Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string? UserId { get; set; }
        public BaseSearchRequest()
        {
            Page = 1;

            Limit = 10;
        }
    }


    public class BaseSearchRepons
    {
        public int Total { get; set; }
        public IEnumerable? Data { get; set; }
    }
}
