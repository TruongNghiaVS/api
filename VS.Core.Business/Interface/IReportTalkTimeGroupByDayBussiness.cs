using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface IReportTalkTimeGroupByDayBussiness : IGenericBussine<ReportTalkTimeGroupByDay>
    {
        Task<GetAllRecordGroupByLineCodeReponse> ProcessCalReportGroupByDay(GetAllRecordGroupByLineCodeRequest request);


        Task<GetAllRecordGroupByLineCodeReponse> GetAll(GetAllRecordGroupByLineCodeRequest request);

        Task<GetOverViewDashboardReponse> GetOverViewDashBoard(GetOverViewDashboard request);

    }
}
