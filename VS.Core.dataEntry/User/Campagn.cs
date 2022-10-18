namespace VS.Core.dataEntry.User
{
    public class Profile : BaseEntry
    {
        public string CustomerName { get; set; }
        public string NoAgreement { get; set; }
        public string NationalId { get; set; }
        public string MobilePhone { get; set; }
        public string? Phone1 { get; set; }
        public string? HouseNumber { get; set; }

        public string? OfficeNumber { get; set; }

        public string? OtherPhone { get; set; }

        public string? Email { get; set; }

        public DateTime? DayOfBirth { get; set; }


        // addressInfo

        public string? Road { get; set; }
        public string? SuburbanDir { get; set; }
        public string? Provice { get; set; }

        public string? Road1 { get; set; }
        public string? SuburbanDir1 { get; set; }
        public string? Provice1 { get; set; }

        public string? Road2 { get; set; }
        public string? SuburbanDir2 { get; set; }
        public string? Provice2 { get; set; }


        public string? StatusPayMent { get; set; }

        public string DPD { get; set; }

        // infomation about finance
        public DateTime? RegisterDay { get; set; }

        public float? DebitOriginal { get; set; }

        public float? AmountLoan { get; set; }

        public float EMI { get; set; }

        public int CampaignId { get; set; }

        public string Assignee { get; set; }

        public Profile()
        {

        }

    }


}
