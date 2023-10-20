using System.Collections;

namespace VS.core.Request
{
    public class EmployeeSearchRequest : BaseSearchRequest
    {

        public int? VendorId { get; set; }



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
        public string? CampagnId { get; set; }
        public string GroupStatus { get; set; }

        public int? VendorId { get; set; }
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
        public int? VendorId { get; set; }
        public GroupReasonRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class LineManagementRequest : BaseSearchRequest
    {
        public string Msg { get; set; }

        public int? Userid { get; set; }
        public int? VendorId { get; set; }

        public int? lineIdAdd { get; set; }

        public LineManagementRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class SmsGetallRequest : BaseSearchRequest
    {


        public string Userid { get; set; }
        public int? VendorId { get; set; }

        public int? lineIdAdd { get; set; }

        public SmsGetallRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }


    public class ViewRecordingRequest : BaseSearchRequest
    {

        public string LineCode { get; set; }
        public string UserId { get; set; }

    }

    public class WorkplaceNotedRequest : BaseSearchRequest
    {

        public string LineCode { get; set; }
        public string UserId { get; set; }

    }



    public class GetDashboardQcRequest : BaseSearchRequest
    {
        public string? LineCode { get; set; }

    }

    public class SmsGetallReponse : BaseSearchRepons

    {
        public SmsGetallReponse()
        {
            Total = 0;
        }
    }

    public class ViewRecordingReponse : BaseSearchRepons

    {
        public ViewRecordingReponse()
        {
            Total = 0;
        }
    }


    public class WorkplaceNotedReponse : BaseSearchRepons

    {
        public WorkplaceNotedReponse()
        {
            Total = 0;
        }
    }

    public class GetDashboardQcReponse : BaseSearchRepons
    {
        public GetDashboardQcReponse()
        {
            Total = 0;
        }
    }



    public class LineManagementReponse : BaseSearchRepons

    {
        public LineManagementReponse()
        {
            Total = 0;
        }
    }



    public class DPDRequest : BaseSearchRequest
    {

        public int? VendorId { get; set; }

        public DPDRequest()
        {

        }
    }



    public class PackageRequestALl : BaseSearchRequest
    {

        public int? VendorId { get; set; }
        public PackageRequestALl()
        {

        }
    }


    public class PackageIndexViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Total { get; set; }

        public int Remain { get; set; }

        public bool Active { get; set; }

        public PackageIndexViewModel()
        {
            Active = false;
            Remain = Total = 0;

        }

    }
    public class PackageReponseInfo
    {

        public string? Name { get; set; }
        public int? Status { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }

        public string? IdUser { get; set; }

        public int? VendorId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

    }
    public class DpdReponse : BaseSearchRepons

    {
        public DpdReponse()
        {
            Total = 0;
        }
    }


    public class PackageRequest : BaseSearchRequest
    {
        public int? VendorId { get; set; }

        public PackageRequest()
        {

        }
    }

    public class GetCountBYMinMaxRequest
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
        public string? UserId { get; set; }
        public int? VendorId { get; set; }
        public GetCountBYMinMaxRequest()
        {

        }
    }

    public class GetCountBYMinMaxReponse
    {

        public int Total { get; set; }

        public int Remain { get; set; }
    }



    public class PackageReponse : BaseSearchRepons

    {
        public PackageReponse()
        {
            Total = 0;
        }
    }
    public class GroupReasonReponse : BaseSearchRepons

    {
        public GroupReasonReponse()
        {
            Total = 0;
        }
    }


    public class ReasonReponse : BaseSearchRepons

    {
        public ReasonReponse()
        {
            Total = 0;
        }
    }

    public class CampangnOverviewByIdRequest
    {
        public string? CampaignId { get; set; }

    }

    public class CampagnRequest : BaseSearchRequest
    {
        public string? Msg { get; set; }
        public int? VendorId { get; set; }
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


    public class CampangnOverviewByIdReponse
    {
        public int? NumberHaveNotAssigee { get; set; }

        public int? NumberHasAssigee { get; set; }
        public int? Total { get; set; }

        public int? NumberHasClose { get; set; }

        public int? NumberHasKeep { get; set; }

        public int? NumberProcessing { get; set; }

        public CampangnOverviewByIdReponse()
        {
            NumberHaveNotAssigee = NumberHasAssigee = NumberHasClose = NumberHasKeep = 0;
            NumberProcessing = 0;
        }



    }
    public class OverViewCampangnReponse : BaseSearchRepons

    {
        public OverViewCampangnReponse()
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

        public int? VendorId { get; set; }

        public string? NoAgreement { get; set; }

        public string? PhoneSerach { get; set; }
        public string? LineCode { get; set; }
        public int? Skipp { get; set; }
        public string? ColorCode { get; set; }

        public bool? SkipData { get; set; }



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
        public string? NoAgreement { get; set; }

        public string? VendorId { get; set; }
        public ImpactHistorySerarchRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }

    public class SkipInfoSerarchRequest : BaseSearchRequest
    {

        public string? NoAgreement { get; set; }



        public SkipInfoSerarchRequest()
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

    public class SkipInfoReponse : BaseSearchRepons

    {
        public SkipInfoReponse()
        {
            Total = 0;
        }
    }


    public class GroupEmployeeRequest : BaseSearchRequest
    {
        public int Type { get; set; }
        public int? VendorId { get; set; }
        public GroupEmployeeRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = DateTime.Now.AddMonths(-12);
            this.To = DateTime.Now.AddDays(1).AddMilliseconds(-1);
        }
    }


    public class GroupEmployeeHaveNotInGroupRequest : BaseSearchRequest
    {
        public int GroupId { get; set; }
        public int? VendorId { get; set; }
        public GroupEmployeeHaveNotInGroupRequest()
        {

        }
    }


    public class MemberGroupByIdRequest : BaseSearchRequest
    {
        public int? GroupId { get; set; }
        public int? VendorId { get; set; }
        public MemberGroupByIdRequest()
        {

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



    public class GroupEmployeeHaveNotInGroupRequestReponse : BaseSearchRepons

    {
        public GroupEmployeeHaveNotInGroupRequestReponse()
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

        public int? VendorId { get; set; }
        public int? StatusSearch { get; set; }
        public string? LineCode { get; set; }


        public ReportImpactRequest()
        {
            this.Page = 1;
            this.Limit = 10;
        }
    }


    public class MakeCallRequest
    {
        public string? PhoneNumber { get; set; }

        public string? NoAgree { get; set; }

        public int? ProfileId { get; set; }
        public MakeCallRequest()
        {

        }
    }

    public class SmsRequest
    {
        public string? PhoneNumber { get; set; }

        public string? ContentSms { get; set; }
        public string? NoAgree { get; set; }
        public int? ProfileId { get; set; }
        public SmsRequest()
        {

        }
    }


    public class SmsContentGateway
    {

        public string text { get; set; }

        public int[] port { get; set; }

        public List<ObjectPhone> param { get; set; }
    }

    public class SmsReponse
    {
        public int error_code { get; set; }

        public string sn { get; set; }

        public string sms_in_queue { get; set; }

        public string task_id { get; set; }
    }
    public class ObjectPhone
    {
        public string number { get; set; }
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

        public string? NoAgree { get; set; }

        public int? TimeTalkBegin { get; set; }
        public int? TimeTalkEnd { get; set; }
        public int? TimeFrom1 { get; set; }
        public int? TimeFrom2 { get; set; }

        public int? VendorId { get; set; }
        public ReportCDRequest()
        {
            TimeFrom2 = 64800;
            TimeFrom1 = 25200;
            TimeTalkEnd = 600;
            TimeTalkBegin = 0;

        }
    }

    public class ReportCallRequest : BaseSearchRequest
    {

        public string? LineCode { get; set; }
        public string? Disposition { get; set; }

        public string? PhoneLog { get; set; }

        public string? NoAgree { get; set; }



        public int? VendorId { get; set; }
        public ReportCallRequest()
        {

        }
    }
    public class ReportNoCDRequest : ReportCDRequest
    {
        public string? NoAgree { get; set; }

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
        public DateTime? TimeSelect { get; set; }

        public int? VendorId { get; set; }

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
    public class ReportImpactItemEx
    {

        public string? NoAgreement { get; set; }


        public string? CustomerName { get; set; }
        public DateTime? Dob { get; set; }
        public string? NationalId { get; set; }
        public DateTime? RegisterDay { get; set; }

        public string? CodeProduct { get; set; }

        public string? NameProduct { get; set; }

        public string? PriceProduct { get; set; }

        public string? TotalFines { get; set; }

        public string? TotalMoneyPaid { get; set; }
        public string? Tenure { get; set; }
        public string? EMI { get; set; }

        public DateTime LastPadDay { get; set; }

        public string? NoTenure { get; set; }

        public string? DebitOriginal { get; set; }


        public string? DPD { get; set; }

        public string? MobilePhone { get; set; }

        public string? OtherPhone { get; set; }
        public string? NoteFirstTime { get; set; }

        public string? Road { get; set; }
        public string? SuburbanDir { get; set; }
        public string? Provice { get; set; }

        public string? Road1 { get; set; }
        public string? SuburbanDir1 { get; set; }
        public string? Provice1 { get; set; }

        public string? Road2 { get; set; }
        public string? SuburbanDir2 { get; set; }
        public string? Provice2 { get; set; }

        public string StatusName { get; set; }

        public string status { get; set; }

        public string EmployeeName { get; set; }

        public string? NoteIm { get; set; }
        public string? LastNote { get; set; }


        public DateTime? Promiseday { get; set; }
        public string MoneyPromise { get; set; }
        public DateTime? DaysuggestTime { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }


    public class ReportCDRItem
    {
        public int TotalRecord { get; set; }
        public int Id { get; set; }
        public DateTime? Calldate { get; set; }
        public string? Dst { get; set; }
        public string? Src { get; set; }
        public string? Disposition { get; set; }
        public string? Lastapp { get; set; }
        public string? Billsec { get; set; }
        public string? Duration { get; set; }
        public string? Recordingfile { get; set; }
        public int DurationBill { get; set; }
        public double DurationReal { get; set; }
        public string? NoAgree { get; set; }

        public bool IsCal { get; set; }
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


    public class ReportCallItem
    {
        public int TotalRecord { get; set; }
        public DateTime? CreateAt { get; set; }
        public int Id { get; set; }
        public DateTime? Calldate { get; set; }
        public string? Phone { get; set; }
        public string? Src { get; set; }
        public string? Disposition { get; set; }
        public string? Lastapp { get; set; }
        public string? Billsec { get; set; }
        public string? Duration { get; set; }
        public string? Recordingfile { get; set; }
        public int DurationBill { get; set; }
        public double DurationReal { get; set; }
        public string? NoAgree { get; set; }

        public string? LineCode { get; set; }

        public bool IsCal { get; set; }
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
    public class ReportFLCall
    {
        public int TotalRecord { get; set; }
        public string UserName { get; set; }

        public string LineCode { get; set; }

        public string? Date { get; set; }
        public DateTime? CallAt { get; set; }


    }

    public class ReportCDRItemExport
    {
        public string? Src { get; set; }
        public DateTime? Calldate { get; set; }
        public string? Dst { get; set; }
        private string? Disposition { get; set; }
        private double? DurationReal { get; set; }

        public string DurationRealText
        {

            get
            {
                if (!DurationReal.HasValue)
                {
                    return "00:00:00";
                }
                TimeSpan t = TimeSpan.FromSeconds(DurationReal.Value);
                string s = string.Format("{0}:{1}:{2}", ((int)t.TotalHours), t.Minutes, t.Seconds);
                return s;
            }
        }
        public string? Duration { get; set; }
        private string? Recordingfile { get; set; }
        public string? RecordingfileUrl
        {
            get
            {
                return "http://42.115.94.180:7777/api/file/getaudio10?filePath=" + Recordingfile;
            }
        }
        public string? NoAgree { get; set; }

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
        public DateTime? TimeSelect { get; set; }

        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public DateTime? Linked { get; set; }
    }

    public class HandlelFileRecordingReponse : BaseSearchRepons

    {
        public HandlelFileRecordingReponse()
        {

        }
    }



    public class GetOverViewDashboard : BaseSearchRequest
    {

        public int? VendorId { get; set; }
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







    public class GetAllRecordGroupByLineCodeExportReponse : BaseSearchRepons

    {
        public GetAllRecordGroupByLineCodeExportReponse()
        {
            Total = 0;
        }
    }


    public class GetAllRecordGroupByLineCodeExportRequest : BaseSearchRequest
    {
        public string? LineCode { get; set; }
        public DateTime? TimeSelect { get; set; }

        public int? VendorId { get; set; }

        public GetAllRecordGroupByLineCodeExportRequest()
        {

        }
    }

    public class SmsGetHandleRequest : BaseSearchRequest
    {
        public int Netword { get; set; }



        public SmsGetHandleRequest()
        {
            this.Page = 1;
            this.Limit = 10;

        }
    }



    public class FirstCallLastCallRequest : BaseSearchRequest
    {

        public int? VendorId { get; set; }
        public string? LineCode { get; set; }
        public FirstCallLastCallRequest()
        {



        }
    }

    public class FirstCallLastCallReponse : BaseSearchRepons

    {
        public int TotalRecord { get; set; }
        public FirstCallLastCallReponse()
        {
            Total = 0;
        }
    }
}
