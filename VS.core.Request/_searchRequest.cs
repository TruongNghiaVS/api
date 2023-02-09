using System.Collections;

namespace VS.core.Request
{
    public class EmployeeSearchRequest : BaseSearchRequest
    {




        public EmployeeSearchRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }

    public class EmployeeSearchReponse : BaseSearchRepons
    {

        public EmployeeSearchReponse()
        {
            Total = 0;
        }


    }


    public class MaterDataRequest : BaseSearchRequest
    {
        public string Msg { get; set; }
        public string GroupStatus { get; set; }
        public MaterDataRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class MasterDataReponse : BaseSearchRepons

    {
        public MasterDataReponse()
        {
            Total = 0;
        }
    }



    public class GroupReasonRequest : BaseSearchRequest
    {
        public string Msg { get; set; }

        public GroupReasonRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class GroupReasonReponse : BaseSearchRepons

    {
        public GroupReasonReponse()
        {
            Total = 0;
        }
    }


    public class CampagnRequest : BaseSearchRequest
    {
        public string? Msg { get; set; }

        public string? CampaignId { get; set; }
        public CampagnRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class CampagnRequestReponse : BaseSearchRepons

    {
        public CampagnRequestReponse()
        {
            Total = 0;
        }
    }




    public class GetAllProfileByCampang : BaseSearchRequest
    {
        public string? Id { get; set; }

        public int? Status { get; set; }
        public string? TypegetData { get; set; }

        public int? DpdMax { get; set; }
        public int? DpdMin { get; set; }

        public string? NoAgreement { get; set; }


    }


    public class GetAllProfileByCampangReponse : BaseSearchRepons

    {
        public GetAllProfileByCampangReponse()
        {
            Total = 0;
        }
    }

    public partial class CampagnAsiggeeByCampagnIdReponse : BaseSearchRepons
    {


        public int? NumberTotal { get; set; }
        public int? NumberHaveNotAssigee { get; set; }

        public int? NumberHasAssigee { get; set; }

        public int? NumberHasClose { get; set; }

        public GetOverviewCampaignModelById Model { get; set; }
        public CampagnAsiggeeByCampagnIdReponse()
        {
            NumberTotal = 0;
            NumberHaveNotAssigee = 0;
            NumberHasAssigee = 0;
            NumberHasClose = 0;

        }

    }



    public class ImpactHistorySerarchRequest : BaseSearchRequest
    {
        public string? Msg { get; set; }

        public string? ProfileId { get; set; }
        public ImpactHistorySerarchRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }

    public class ImpactHistoryReponse : BaseSearchRepons

    {
        public ImpactHistoryReponse()
        {
            Total = 0;
        }
    }



    public class GroupEmployeeRequest : BaseSearchRequest
    {
        public int Type { get; set; }
        public GroupEmployeeRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = DateTime.Now.AddMonths(-12);
            this.To = DateTime.Now.AddDays(1).AddMilliseconds(-1);
        }
    }

    public class MaterDataNewRequest : BaseSearchRequest
    {
        public int Type { get; set; }
        public MaterDataNewRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class MasterDataNewReponse : BaseSearchRepons

    {
        public MasterDataNewReponse()
        {
            Total = 0;
        }
    }
    public class GroupEmployeeReponse : BaseSearchRepons

    {
        public GroupEmployeeReponse()
        {
            Total = 0;
        }
    }

    public class MasterDataInfoReponse : BaseSearchRepons

    {

        public IEnumerable ListData { get; set; }

        public IEnumerable ListDataReason { get; set; }
        public MasterDataInfoReponse()
        {
            Total = 0;
        }
    }


    public class CampaignProfile_caseRequest
    {
        public bool? ResetCase { get; set; }

        public bool? Skipp { get; set; }

        public int? Id { get; set; }


    }

    public class GetOverviewCampaignModelByIdReponse
    {

        public GetOverviewCampaignModelById model { get; set; }


    }

    public class GetOverviewCampaignModelById

    {
        public int NumberHaveNotAssigee { get; set; }
        public int NumberHasAssigee { get; set; }
        public int NumberHasClose { get; set; }
        public int NumberHasKeep { get; set; }
        public int Total { get; set; }
    }

    public class LoginReportSerarchRequest : BaseSearchRequest
    {
        public string? Msg { get; set; }

        public string? ProfileId { get; set; }

        public string? UserName { get; set; }
        public LoginReportSerarchRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }

    public class LoginReportReponse : BaseSearchRepons

    {
        public LoginReportReponse()
        {
            Total = 0;
        }
    }



    public class ImpactDashboardOverviewReponse
    {
        public List<ImpactDashboardOverviewReponseItem>? Data { get; set; }

    }

    public class ImpactDashboardOverviewReponseItem
    {
        public string ReasonCode { get; set; }

        public string? ReasonName { get; set; }
        public int Sum { get; set; }

        public float Percent { get; set; }


    }
    public class ReportImpactRequest : BaseSearchRequest
    {
        public string? UserId { get; set; }

        public string? LineCode { get; set; }
        public ReportImpactRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }


    public class MakeCallRequest
    {
        public string? PhoneNumber { get; set; }

        public string? NoAgree { get; set; }

        public MakeCallRequest()
        {

        }
    }



    public class ReportCDRReponse : BaseSearchRepons

    {
        public ReportCDRReponse()
        {
            Total = 0;
        }
    }

    public class ReportCDRequest : BaseSearchRequest
    {

        public string? LineCode { get; set; }
        public string? Disposition { get; set; }

        public string? PhoneLog { get; set; }
        public ReportCDRequest()
        {

        }
    }


    public class GetAllRecordGroupByLineCodeReponse : BaseSearchRepons

    {
        public GetAllRecordGroupByLineCodeReponse()
        {
            Total = 0;
        }
    }


    public class GetAllRecordGroupByLineCodeRequest : BaseSearchRequest
    {
        public string? LineCode { get; set; }
        public GetAllRecordGroupByLineCodeRequest()
        {

        }
    }





    public class ReportImpactReponse : BaseSearchRepons

    {

        public ReportImpactReponse()
        {
            Total = 0;
        }
    }


    public class ReportImpactItem
    {

        public int TotalRecord { get; set; }

        public string? CustomerName { get; set; }
        public string? NoAgreement { get; set; }
        public string? Phone1 { get; set; }
        public string? OfficeNumber { get; set; }
        public string? OtherPhone { get; set; }
        public string? HouseNumber { get; set; }
        public string? ReasonName { get; set; }
        public string? CampaignName { get; set; }
        public string? UpdateName { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }



    public class ReportCDRItem
    {
        public int TotalRecord { get; set; }
        public string? Calldate { get; set; }
        public string? Dst { get; set; }
        public string? Src { get; set; }
        public string? Disposition { get; set; }
        public string? Lastapp { get; set; }
        public string? Billsec { get; set; }
        public string? Duration { get; set; }
        public string? Recordingfile { get; set; }
        public int DurationBill { get; set; }
        public double DurationReal { get; set; }
        public bool IsShow
        {
            get
            {
                if (Lastapp == "Dial" && Disposition == "ANSWERED")
                {
                    return true;
                }
                return false;
            }
        }


    }


    public class DashboardOverviewRequest : BaseSearchRequest
    {
        public string LineCode { get; set; }
    }


    public class DashboardOverviewReponse : BaseSearchRequest
    {
        public int? SumCallOut { get; set; }
        public int? SumNoAgree { get; set; }
        public int? PercentConnection { get; set; }
        public int? SumTalktime { get; set; }
    }


    public class DashboardDetailRequest : BaseSearchRequest
    {
        public string LineCode { get; set; }
    }


    public class DashboardDetailReponse
    {

        public int SumTimeCall { get; set; }
        public int SumNoAgree { get; set; }

        public double PercentConnection { get; set; }

        public int TalkTime { get; set; }

        public DashboardDetailReponse()
        {
            SumTimeCall = SumNoAgree = TalkTime = 0;
            PercentConnection = 0;
        }

    }


    public class DashboardDetailItem
    {
        public string Author { get; set; }

        public int? Sum { get; set; }

        public int? SumAgree { get; set; }

        public double? PercentConnection { get; set; }
        public int? SumAnswered { get; set; }
        public int? SumTimeCall { get; set; }
        public int? SumTimeWaiting { get; set; }
        public int? SumTimeTalking { get; set; }
        public int? SumCallNoAswer { get; set; }
        public int? SumCallCancel { get; set; }
        public int? SumCallBusy { get; set; }
        public int? SumCallChanelError { get; set; }
        public int? SumNotCall { get; set; }
        public int? SumCallErrorServer { get; set; }
    }

    public class ReportOverviewAgrree
    {
        public int? Total { get; set; }

    }

    public class ReportNoAgreeRequest
    {
        public int Total { get; set; }


    }



    public class GetOverViewInfoReponse

    {

        public List<GetOverViewInfoReponseItem> Data { get; set; }


        public GetOverViewInfoReponse()
        {

        }
    }

    public class GetOverViewInfoReponseItem
    {

        public int Total { get; set; }

        public string? Type { get; set; }
    }


    public class GetOverViewTalkingItem
    {
        public int Duration { get; set; }

        public int Billsec { get; set; }

        public string Disposition { get; set; }
    }

    public class GetOverViewTalkingItemReponse

    {
        public List<GetOverViewTalkingItem> Data { get; set; }
        public GetOverViewTalkingItemReponse()
        {

        }
    }

    public class GetOverViewInfoRequest : BaseSearchRepons

    {

        public string? LineCode { get; set; }
        public GetOverViewInfoRequest()
        {
            Total = 0;
        }
    }


    public class GetReportOverviewAgrreeRequest
    {

        public string? LineCode { get; set; }

        public string? UserId { get; set; }

    }


    public class CallLogReponse : BaseSearchRepons

    {
        public CallLogReponse()
        {
            Total = 0;
        }
    }



    public class CallLogSerarchRequest : BaseSearchRequest
    {
        public string? Msg { get; set; }

        public string? ProfileId { get; set; }
        public CallLogSerarchRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }
    public class ReportTalkTimeGroupByDaySerarchRequest : BaseSearchRequest
    {

        public ReportTalkTimeGroupByDaySerarchRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }
    public class ReportTalkTimeRequest : BaseSearchRequest
    {

    }

    public class ReportTalkTimeReponse : BaseSearchRepons

    {
        public ReportTalkTimeReponse()
        {
            Total = 0;
        }
    }




    public class HandlelFileRecordingRequest : BaseSearchRequest
    {

    }

    public class HandlelFileRecordingReponse : BaseSearchRepons

    {
        public HandlelFileRecordingReponse()
        {

        }
    }



    public class GetOverViewDashboard : BaseSearchRequest
    {

        public string? LineCode { get; set; }

        public string? UserName { get; set; }


    }


    public class GetOverViewDashboardReponse : BaseSearchRepons

    {

        public GetOverViewDashboardReponse()
        {
            Total = 0;
        }
    }



}
