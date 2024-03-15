namespace VS.Core.Repository.Model
{
    public class CampagnProileExportIndexModel
    {


        public string Id { get; set; }
        public CampagnProileExportIndexModel()
        {

        }
        public string STT { get; set; }

        public string NoAgreement { get; set; }

        public string CustomerName { get; set; }
        public string MobilePhone { get; set; }

        public string HouseNumber { get; set; }

        public string Phone1 { get; set; }

        public string OtherPhone { get; set; }
        public DateTime DayOfBirth { get; set; }

        public  string OfficeNumber { get; set; }

        public string NationalId { get; set; }

        public string NameProduct { get; set; }
        public string CodeProduct { get; set; }
        public decimal AmountLoan { get; set; }
        public DateTime? RegisterDay { get; set; }


        public int Tenure { get; set; }
        public int NoTenure { get; set; }

        public float EMI { get; set; }

        public float TotalFines { get; set; }

        public DateTime? LastPadDay { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalMoneyPaid { get; set; }

        public decimal DebitOriginal { get; set; }

        public int? DPD { get; set; }


        public string Road { get; set; }

        public string SuburbanDir { get; set; }

        public string Provice { get; set; }

        public string? Road1 { get; set; }
        public string? SuburbanDir1 { get; set; }
        public string? Provice1 { get; set; }
        public string? NoteFirstTime { get; set; }

        public string? NoteRel { get; set; }

        public string? AssignedId { get; set; }
        public string AssigneeName { get; set; }
        public string ReasonstatusText
        {
            get; set;
        }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public string? NoteCode { get; set; }
        public string? PlaceCode { get; set; }
        public string? WayContact { get; set; }
        public string? Code { get; set; }
        public string? ColorCode { get; set; }

        public string? Assignee { get; set; }

        public string? UserName { get; set; }


    }
}
