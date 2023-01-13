using VS.core.Request;

namespace VS.Core.Business.Interface
{
    public interface IReportBussiness
    {

        Task<ImpactDashboardOverviewReponse> ReportImpactDashboardOverview();


        Task<int> GetReportOverviewAgrree();
        Task<GetOverViewInfoReponse> GetOverViewInfo(GetOverViewInfoRequest request);

        Task<ReportImpactReponse> GetAllReportImapact(ReportImpactRequest request);
        //Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request);
        Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request);


        Task<GetOverViewTalkingItemReponse> GetOverViewTimeTalking(GetOverViewInfoRequest request);


        Task<ReportCDRReponse> GetAllReportRecordingFile(ReportCDRequest request);

        Task<int> CalTalkingTime();

    }
}
