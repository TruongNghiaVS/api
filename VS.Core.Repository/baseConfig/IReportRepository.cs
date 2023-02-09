using VS.core.Request;


namespace VS.Core.Repository.baseConfig
{
    public interface IReportRepository
    {
        Task<ImpactDashboardOverviewReponse> GetALlOverView(ReportImpactRequest request);

        Task<ReportImpactReponse> GetAllReportImapact(ReportImpactRequest request);

        Task<ReportCDRReponse> GetAllRecordingFile(ReportCDRequest request);
        Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request);

        Task<ReportCDRReponse> GetAllReportRecordingFile(ReportCDRequest request);

        Task<int> GetReportOverviewAgrree(GetReportOverviewAgrreeRequest request);

        Task<GetOverViewInfoReponse> GetOverViewInfo(GetOverViewInfoRequest request);

        Task<GetOverViewTalkingItemReponse> GetOverViewTimeTalking(GetOverViewInfoRequest request);
    }
}
