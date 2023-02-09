using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class ReportTalkTimeGroupByDayBussiness : IReportTalkTimeGroupByDayBussiness
    {

        private readonly IUnitOfWork _unitOfWork;
        public ReportTalkTimeGroupByDayBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> AddAsync(ReportTalkTimeGroupByDay entity)
        {
            return _unitOfWork.ReportTalkTimeGroupByDay.AddAsync(entity);
        }
        public Task Delete(ReportTalkTimeGroupByDay entity)
        {
            return _unitOfWork.ReportTalkTimeGroupByDay.Delete(entity);
        }
        public async Task<ReportTalkTimeGroupByDay> GetByIdAsync(string id)
        {
            return await _unitOfWork.ReportTalkTimeGroupByDay.GetByIdAsync(id);
        }
        public Task<int> UpdateAsyn(ReportTalkTimeGroupByDay entity)
        {
            return _unitOfWork.ReportTalkTimeGroupByDay.UpdateAsyn(entity);
        }

        public Task<GetAllRecordGroupByLineCodeReponse> ProcessCalReportGroupByDay(GetAllRecordGroupByLineCodeRequest entity)
        {
            return _unitOfWork.ReportTalkTimeGroupByDay.ProcessingReportGroupByDay(entity);
        }

        public Task<GetAllRecordGroupByLineCodeReponse> GetAll(GetAllRecordGroupByLineCodeRequest entity)
        {
            return _unitOfWork.ReportTalkTimeGroupByDay.GetAll(entity);
        }
        public Task<GetOverViewDashboardReponse> GetOverViewDashBoard(GetOverViewDashboard entity)
        {
            return _unitOfWork.ReportTalkTimeGroupByDay.GetOverViewDashBoard(entity);
        }




    }
}
