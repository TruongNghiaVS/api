using VS.core.Request;


namespace VS.Core.Repository.baseConfig
{
    public interface IReportRepository
    {
        Task<ImpactDashboardOverviewReponse> GetALlOverView(int user_id = 0);

        Task<ReportImpactReponse> GetAllReportImapact(ReportImpactRequest request);

        Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request);

        Task<ReportCDRReponse> GetAllReportRecordingFile(ReportCDRequest request);

        Task<int> GetReportOverviewAgrree();

        Task<GetOverViewInfoReponse> GetOverViewInfo(GetOverViewInfoRequest request);

        Task<GetOverViewTalkingItemReponse> GetOverViewTimeTalking(GetOverViewInfoRequest request);
    }
}
