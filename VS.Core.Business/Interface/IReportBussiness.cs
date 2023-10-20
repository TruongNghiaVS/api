using VS.core.Request;

namespace VS.Core.Business.Interface
{
    public interface IReportBussiness
    {

        Task<ImpactDashboardOverviewReponse> ReportImpactDashboardOverview(ReportImpactRequest request);
        Task<int> GetReportOverviewAgrree(GetReportOverviewAgrreeRequest request);
        Task<GetOverViewInfoReponse> GetOverViewInfo(GetOverViewInfoRequest request);

        Task<ReportImpactReponse> GetAllReportImapact(ReportImpactRequest request);
        Task<ReportImpactReponse> ExportImpactData(ReportImpactRequest request);


        //Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request);

        Task<ReportCDRReponse> getAllCall(ReportCallRequest request);
        Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request);
        Task<ReportCDRReponse> GetAllRecordingFile(ReportCDRequest request);
        Task<FirstCallLastCallReponse> getAllFirstLastCall(FirstCallLastCallRequest request);
        Task<ReportCDRReponse> getAllRecordingFileWithNo(ReportNoCDRequest request);

        Task<ReportCDRReponse> ExportRecordingFile(ReportCDRequest request);

        Task<ReportCDRReponse> ExportRecordingFileNo(ReportNoCDRequest request);
        Task<GetOverViewTalkingItemReponse> GetOverViewTimeTalking(GetOverViewInfoRequest request);


        Task<ReportCDRReponse> GetAllReportRecordingFile(ReportCDRequest request);

        Task<int> CalTalkingTime();

    }
}
