namespace VS.Core.dataEntry.User
{
    public class SkipInfo : BaseEntry
    {
        public string? NationalId { get; set; }
        public string? NoAgree { get; set; }
        public string? TypeCustomer { get; set; }
        public string? Name { get; set; }
        public string? Relation { get; set; }

        public DateTime? Dob { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Position { get; set; }

        public string? SalaryDe { get; set; }

        public string? CompanyName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string? Cmnd { get; set; }

        public string? TimeWork { get; set; }

        public string? WorkPlace { get; set; }

        public SkipInfo()
        {

        }

    }


}
