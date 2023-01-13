using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace VS.Core.Repository.baseConfig
{
    public interface IReportTalkTimeRepository : IGenericRepository<ReportTalkTime>
    {
        Task<ReportTalkTimeReponse> GetALl(ReportTalkTimeRequest request);

        Task<IEnumerable<ReportQuerryTaltimeIndex>> HandlelFileRecording(HandlelFileRecordingRequest request);

        Task<ReportQuerryRecordingFileIndex> GetInfomationRecording(string likiedId);
    }




}
