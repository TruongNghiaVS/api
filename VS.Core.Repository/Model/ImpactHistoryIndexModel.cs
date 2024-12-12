namespace VS.Core.Repository.Model
{
    public class ImpactHistoryIndexModel : BaseIndexModel
    {
        public string? ShortDescription { get; set; }
        public string? StatusIm { get; set; }

        public string? NoteIm { get; set; }

        public DateTime? Promiseday { get; set; }

        public string? MoneyPromise { get; set; }

        public DateTime? DaysuggestTime { get; set; }

        public int? StatusFollow { get; set; }

        public int? Relationship { get; set; }

        public int? ProfileId { get; set; }

        public int? Priority { get; set; }

        public string? StatusName { get; set; }

        public string? ColorCode { get; set; }
        public DateTime? CreateAt { get; set; }

        public string? StatusCode { get; set; }


    }



    public class ImpactHistoryv2IndexModel
    {
        public DateTime? CreateTime { get; set; }
        public string? Username { get; set; }

        public string? NoAgreement { get; set; }

        public string? PlaceCode { get; set; }

        public string? WayContact { get; set; }

        public string? NoteCode { get; set; }

        public string? StatusName { get; set; }

        public DateTime? Promiseday { get; set; }

        public string? MoneyPromise { get; set; }

        public string? Question
        {
            get
            {
                return "";
            }
        }

        public string? NoteIm { get; set; }





    }

    public class ImpactHistoryv2IndexModelVP
    {
        public DateTime? ActivityDate { get; set; }
        public string? Account_Number { get; set; }
        public string? Invoice_Number { get; set; }
        public string? Agent_Name { get; set; }
        public string? Activity_Type { get; set; }
        public string? Phone_Number { get; set; }
        public string? Call_Disposition { get; set; }
        public string? Call_Outcome { get; set; }
        public string? NoteCode { get; set; }
        public string? Noted { get; set; }
        public DateTime? Promiseday { get; set; }
        public string? MoneyPromise { get; set; }

    }

    public class MiraeCallReportIndexModel
    {

        public string? Username { get; set; }
        public string? Teamlead { get; set; }
        public string? Agrreement_id { get; set; }
        public string? Action_code { get; set; }
        public DateTime? Primise_date { get; set; }
        public string? Promise_amt { get; set; }
        public string? Remark { get; set; }
        public DateTime? Calldate { get; set; }
        public string? ContactPerson { get; set; }


    }

}
