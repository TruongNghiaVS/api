namespace VS.Core.Repository.Model
{
    public class CampagnProileExportIndexModel
    {


        public CampagnProileExportIndexModel()
        {

        }
        public string STT { get; set; }

        public string NoAgreement { get; set; }

        public string CustomerName { get; set; }
        public string MobilePhone { get; set; }

        public DateTime DayOfBirth { get; set; }

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
        public string AssigneeName { get; set; }
        public string ReasonstatusText
        {
            get; set;
        }
        public DateTime? UpdateAt { get; set; }
     
        


    }
}
