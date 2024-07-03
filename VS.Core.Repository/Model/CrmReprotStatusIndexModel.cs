namespace VS.Core.Repository.Model
{
    public class CrmReprotStatusIndexModel
    {


        public string Id { get; set; }
        public CrmReprotStatusIndexModel()
        {

        }

        public string Total { get; set; }
        public int Total2 { get; set; }
        public string FullName { get; set; }
        public string ReasonCode { get; set; }
        public string ManagerName { get; set; }

        public string ManagerFullName { get; set; }

        public int CreatedBy { get; set; }

    }



    public class CrmReportStatusDeatailIndexModel
    {


        public string Id { get; set; }

        public string Total { get; set; }
        public int TotalGrand { get; set; }
        public string FullName { get; set; }
        public string ReasonCode { get; set; }
        public string ManagerName { get; set; }
        public string ManagerFullName { get; set; }

        public int CreatedBy { get; set; }

        public int BPTP { get; set; }

        public int CALLBACK { get; set; }
        public int CONFIRM { get; set; }
        public int DIE { get; set; }
        public int DIF { get; set; }

        public int LM { get; set; }
        public int LMN { get; set; }
        public int NCON { get; set; }
        public int OTHER { get; set; }
        public int PTP { get; set; }

        public int RFTP { get; set; }

        public int RONA { get; set; }
    }


    public class CrmReportStatusDeataiVPBanklIndexModel
    {
        public string Id { get; set; }
        public string Total { get; set; }
        public int TotalGrand { get; set; }
        public string FullName { get; set; }
        public string ReasonCode { get; set; }
        public string ManagerName { get; set; }
        public string ManagerFullName { get; set; }
        public int CreatedBy { get; set; }
        public int InvalidAddress { get; set; }
        public int VoiceMessageOperator { get; set; }
        public int UnknownContact { get; set; }
        public int UC { get; set; }
        public int SystemHangUp { get; set; }
        public int NotReached { get; set; }
        public int NotInService { get; set; }
        public int NoAnswer { get; set; }
        public int InvalidNumber { get; set; }
        public int Drop { get; set; }
        public int Busy { get; set; }
        public int WrongPartyContact { get; set; }
        public int ThirdPartyContact { get; set; }
        public int RTPFinancialDifficulty { get; set; }
        public int RTPDC { get; set; }
        public int RTPBKY { get; set; }

        public int PPReminder { get; set; }

        public int PPPending { get; set; }

        public int PPApproved { get; set; }

        public int ClaimNC { get; set; }

        public int ClaimFP { get; set; }

        public int ClaimDCD { get; set; }

        public int ClaimBKY { get; set; }

        public int Callback { get; set; }
    }

    public class CrmReportTalktimeIndexModel
    {


        public string Id { get; set; }
        public CrmReportTalktimeIndexModel()
        {

        }

        public double Total { get; set; }

        public string FullName { get; set; }
        public string ReasonCode { get; set; }
        public string ManagerName { get; set; }

        public int CreatedBy { get; set; }

    }


}
