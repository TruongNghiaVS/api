namespace VS.Core.Repository.Model
{
    public class LoginReportIndexModel : BaseIndexModel
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? ActionUser { get; set; }
        public string? Note { get; set; }
        public DateTime? BusinessTime { get; set; }


    }

    public class LoginReportMiraeIndexModel
    {
        public string? UserName { get; set; }
        public DateTime? TimeBusiness { get; set; }

        public DateTime? CheckIn { get; set; }



        public DateTime? Checkout { get; set; }

        public double Duration
        {
            get
            {
                if (Checkout.HasValue && CheckIn.HasValue)
                {
                    return Math.Round((Checkout - CheckIn).Value.TotalHours, 2);
                }
                return 0;
            }
        }

        public string GetDayText
        {
            get
            {
                return GetDayOfWeek(CheckIn.Value);
            }
        }
        private string GetDayOfWeek(DateTime dow)
        {
            if (dow.DayOfWeek == DayOfWeek.Monday)
            {
                return "MONDAY";
            }
            if (dow.DayOfWeek == DayOfWeek.Tuesday)
            {
                return "TUESDAY";
            }
            if (dow.DayOfWeek == DayOfWeek.Wednesday)
            {
                return "WEDNESDAY";
            }
            if (dow.DayOfWeek == DayOfWeek.Friday)
            {
                return "Friday".ToUpper();
            }
            if (dow.DayOfWeek == DayOfWeek.Saturday)
            {
                return "SATURDAY";
            }

            if (dow.DayOfWeek == DayOfWeek.Sunday)
            {
                return "SUNDAY";
            }
            return "";

        }


        public int Typedata { get; set; }

        public LoginReportMiraeIndexModel()
        {

        }


    }
}
