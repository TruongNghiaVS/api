using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface IReportTalkTimeGroupByDay : IGenericRepository<ReportTalkTimeGroupByDay>
    {
        Task<GetAllRecordGroupByLineCodeReponse> ProcessingReportGroupByDay(GetAllRecordGroupByLineCodeRequest entity);
        Task<GetAllRecordGroupByLineCodeReponse> GetAll(GetAllRecordGroupByLineCodeRequest entity);

        Task<GetOverViewDashboardReponse> GetOverViewDashBoard(GetOverViewDashboard entity);

    }

}
