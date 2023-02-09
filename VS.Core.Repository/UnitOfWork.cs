using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Interface;

namespace VS.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public IGroupEmployRepository GroupEmployRe { get; set; }
        public IEmployeeRepository Employees { get; }
        public IUserRepository UserRe { get; }
        public IMasterDataNewRepository MasterNewRe { get; }
        public IMasterDataRepository MasterRe { get; }
        public IGroupReasonRepository GroupRe { get; }
        public ICampagnRepository CampagnRe { get; }
        public IImpactHistoryRepository ImpactRe { get; }

        public IProfileCampagnRepository CampagnProfileRe { get; }
        public IloginReportRepository LoginRe { get; set; }
        public IReportRepository ReportRe { get; }
        public ICallLogRepository CallRe { get; set; }

        public IReportTalkTimeGroupByDay ReportTalkTimeGroupByDay { get; set; }



        public IReportTalkTimeRepository ReportTalkTimeRepository { get; }
        public UnitOfWork(IEmployeeRepository employeeRepository,
             IUserRepository userRepository,
             IMasterDataRepository masterDataRepository,
             IGroupReasonRepository groupRe,
             ICampagnRepository campagnRepository,
             IProfileCampagnRepository campagnProfileRe,
             IImpactHistoryRepository impactHistoryRepository,
             IMasterDataNewRepository masterNewRe,
             IGroupEmployRepository groupEmployRepository,
             IloginReportRepository iloginReportRepository,
              IReportRepository _reportRe,
              ICallLogRepository _callLogRepository,
            IReportTalkTimeRepository _reportTalkTimeRe,
            IReportTalkTimeGroupByDay _reportTalkTimeGroupByDay

            )
        {
            Employees = employeeRepository;
            UserRe = userRepository;
            MasterRe = masterDataRepository;
            GroupRe = groupRe;
            CampagnRe = campagnRepository;
            CampagnProfileRe = campagnProfileRe;
            ImpactRe = impactHistoryRepository;
            MasterNewRe = masterNewRe;
            GroupEmployRe = groupEmployRepository;
            LoginRe = iloginReportRepository;
            ReportRe = _reportRe;
            CallRe = _callLogRepository;
            ReportTalkTimeRepository = _reportTalkTimeRe;
            ReportTalkTimeGroupByDay = _reportTalkTimeGroupByDay;
        }

    }
}