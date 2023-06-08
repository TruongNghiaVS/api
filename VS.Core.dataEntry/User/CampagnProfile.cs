namespace VS.Core.dataEntry.User
{
    public class CampagnProfile : BaseEntry
    {
        public bool? Skipp { get; set; }
        public int Reasonstatus { get; set; }
        public int Status { get; set; }
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

        public string? Assignee { get; set; }

        public float TotalFines { get; set; }
        public float TotalMoneyPaid { get; set; }

        public int Tenure { get; set; }
        public int NoTenure { get; set; }

        public float TotalPaid { get; set; }

        public float LastPaid { get; set; }

        public DateTime? LastPadDay { get; set; }

        public string NameProduct { get; set; }

        public string? CodeProduct { get; set; }

        public string PriceProduct { get; set; }

        public string NoteFirstTime { get; set; }

        public string NoteRel { get; set; }
        public string? SkipContent { get; set; }

        public int? VendorId { get; set; }

        public string? ColorCode { get; set; }
        public bool? DeleteRecord { get; set; }
        public CampagnProfile()
        {

        }

    }


}
