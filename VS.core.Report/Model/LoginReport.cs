using VS.Core.dataEntry;

namespace VS.core.Report.Model
{
    public class LoginReport : BaseEntry
    {
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Action { get; set; }
        public string? Note { get; set; }
        public DateTime? BusinessTime { get; set; }

        public LoginReport()
        {
            BusinessTime = DateTime.Now;
        }

    }
}
