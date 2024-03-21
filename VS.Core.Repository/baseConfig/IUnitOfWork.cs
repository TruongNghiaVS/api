using VS.Core.Repository.Interface;

namespace VS.Core.Repository.baseConfig
{
    public interface IUnitOfWork
    {
        IGroupEmployRepository GroupEmployRe { get; }
        IEmployeeRepository Employees { get; }
        IUserRepository UserRe { get; }


        IMasterDataRepository MasterRe { get; }

        IMasterDataNewRepository MasterNewRe { get; }
        IGroupReasonRepository GroupRe { get; }
        ICampagnRepository CampagnRe { get; }
        IStoreRepository StoreRe { get; }
        IloginReportRepository LoginRe { get; }
        IProfileCampagnRepository CampagnProfileRe { get; }
        IImpactHistoryRepository ImpactRe { get; }

        IReportRepository ReportRe { get; }
        ICallLogRepository CallRe { get; }
        ISmsMessageRepository SmsRe { get; }

        IReportTalkTimeRepository ReportTalkTimeRepository { get; }


        IReportTalkTimeGroupByDay ReportTalkTimeGroupByDay { get; }

        ILineRepository LineRe { get; }

        IDpdRepository DpdRe { get; }

        IPackageRepository PackageRe { get; }


        IViewRecordingRepository ViewRe { get; }
        IWorkplaceNotedRepository Workplace { get; }
        ISkipInfoRepository SkipRe { get; }
        IExportFileRepository DailyReport { get; }
        IAutoRepository AutoRepository { get; }
        

    }
}
