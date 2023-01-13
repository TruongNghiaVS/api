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

        IloginReportRepository LoginRe { get; }
        IProfileCampagnRepository CampagnProfileRe { get; }
        IImpactHistoryRepository ImpactRe { get; }

        IReportRepository ReportRe { get; }
        ICallLogRepository CallRe { get; }

        IReportTalkTimeRepository ReportTalkTimeRepository { get; }

    }
}
