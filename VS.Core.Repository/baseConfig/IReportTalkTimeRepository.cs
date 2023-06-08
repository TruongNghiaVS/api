using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace VS.Core.Repository.baseConfig
{
    public interface IReportTalkTimeRepository : IGenericRepository<ReportTalkTime>
    {
        Task<ReportTalkTimeReponse> GetALl(ReportTalkTimeRequest request);
        Task<List<ReportTalkTimeIndexModel>> GetAllDeleted();
        Task<int> DeleteAll();
        Task<int> UpdateFileDeleted(string filePath);
        Task<int> DeleteAllRangeFromTo(DateTime from, DateTime to);
        Task<DateTime?> GetMaxLinked(string type);

        Task<IEnumerable<ReportQuerryTaltimeIndex>> HandlelFileRecording(HandlelFileRecordingRequest request);

        Task<IEnumerable<ReportQuerryTaltimeIndex>> HandlelFileRecordingServe2(HandlelFileRecordingRequest request);
        Task<IEnumerable<ReportQuerryTaltimeIndex>> HandlelFileRecordingServe3(HandlelFileRecordingRequest request);
        Task<IEnumerable<ReportQuerryTaltimeIndex>> HandlelFileRecordingServe4(HandlelFileRecordingRequest request);
        Task<ReportQuerryRecordingFileIndex> GetInfomationRecording(string likiedId);
    }




}
