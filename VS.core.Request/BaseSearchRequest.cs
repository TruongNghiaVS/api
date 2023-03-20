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

        private DateTime? FromTimeAss { get; set; }

        public DateTime? From
        {

            get
            {
                return FromTimeAss;
            }
            set
            {
                if (value.HasValue)
                {
                    FromTimeAss = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 0, 0, 0);

                }
                else
                {
                    FromTimeAss = null;
                }

            }
        }


        private DateTime? ToTimeAss { get; set; }
        public DateTime? To
        {
            get
            {
                return ToTimeAss;
            }
            set
            {
                if (value.HasValue)
                {
                    ToTimeAss = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 23, 59, 59);

                }
                else
                {
                    ToTimeAss = null;
                }

            }
        }

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
