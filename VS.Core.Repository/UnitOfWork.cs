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
        public ILineRepository LineRe { get; set; }
        public IReportTalkTimeGroupByDay ReportTalkTimeGroupByDay { get; set; }
        public ISmsMessageRepository SmsRe { get; set; }


        public IReportTalkTimeRepository ReportTalkTimeRepository { get; }

        public IDpdRepository DpdRe { get; set; }

        public IPackageRepository PackageRe { get; }

        public IViewRecordingRepository ViewRe { get; }
        public ISkipInfoRepository SkipRe { get; }

        public IWorkplaceNotedRepository Workplace { get; }

        public IExportFileRepository DailyReport { get; }
        public IStoreRepository StoreRe { get; }
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
            IReportTalkTimeGroupByDay _reportTalkTimeGroupByDay,
            ILineRepository _lineRe,
            ISmsMessageRepository _smsRe,
            IDpdRepository _dpdRe,
            IPackageRepository _PackageRe,
            IViewRecordingRepository _ViewRe,
            ISkipInfoRepository _SkipRe,
            IWorkplaceNotedRepository _workplaceNoted,
             IExportFileRepository _exportFile ,
             IStoreRepository _storeRe

            )
        {
            ViewRe = _ViewRe;
            PackageRe = _PackageRe;
            SmsRe = _smsRe;
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
            LineRe = _lineRe;
            DpdRe = _dpdRe;
            SkipRe = _SkipRe;
            Workplace = _workplaceNoted;
            DailyReport = _exportFile;
            StoreRe = _storeRe;

        }

    }
}